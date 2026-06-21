const COLS = 64;
const ROWS = 42;
const ROUND_SECONDS = 90;

const fallbackData = {
  opponentParties: [
    { name: "Chai Biscuit Front", color: "#2f7de1", symbol: "cup" },
    { name: "WiFi Vikas Dal", color: "#13a99a", symbol: "wheel" },
    { name: "Homework Mukti Morcha", color: "#f4c542", symbol: "kite" }
  ],
  randomEvents: [
    { title: "Mega rally speaker boost", effect: "speedUp", copy: "Your rally van got louder. Speed up for a bit." },
    { title: "Poster printer jam", effect: "speedDown", copy: "The printer needs a thappad. Speed drops for a bit." },
    { title: "Tea stall debate won", effect: "claimBurst", copy: "Local voters liked the joke. Nearby influence grows." },
    { title: "Sticker wave", effect: "claimLine", copy: "Volunteers pasted stickers across the route." },
    { title: "Rain delay", effect: "neutral", copy: "Everyone waited under the same tent. No damage, only drama." }
  ],
  blockedTerms: [
    "bjp",
    "bharatiya janata party",
    "congress",
    "inc",
    "aap",
    "aam aadmi party",
    "modi",
    "rahul",
    "gandhi",
    "kejriwal",
    "yogi",
    "amit shah",
    "shivsena",
    "rss",
    "vhp",
    "hindu",
    "muslim",
    "sikh",
    "christian",
    "dalit",
    "brahmin",
    "rajput",
    "yadav",
    "terror",
    "violence",
    "kill",
    "nazi"
  ]
};

const canvas = document.querySelector("#gameCanvas");
const ctx = canvas.getContext("2d");
const setupModal = document.querySelector("#setupModal");
const resultModal = document.querySelector("#resultModal");
const partyNameInput = document.querySelector("#partyNameInput");
const sloganInput = document.querySelector("#sloganInput");
const colorInput = document.querySelector("#colorInput");
const symbolInput = document.querySelector("#symbolInput");
const nameError = document.querySelector("#nameError");
const startBtn = document.querySelector("#startBtn");
const restartBtn = document.querySelector("#restartBtn");
const shareBtn = document.querySelector("#shareBtn");
const boostBtn = document.querySelector("#boostBtn");
const influenceStat = document.querySelector("#influenceStat");
const timeStat = document.querySelector("#timeStat");
const eventStat = document.querySelector("#eventStat");
const toast = document.querySelector("#toast");
const feedList = document.querySelector("#feedList");
const partySwatch = document.querySelector("#partySwatch");
const partyNamePreview = document.querySelector("#partyNamePreview");
const partySloganPreview = document.querySelector("#partySloganPreview");
const resultHeadline = document.querySelector("#resultHeadline");
const resultCopy = document.querySelector("#resultCopy");

const state = {
  data: fallbackData,
  owner: new Uint8Array(COLS * ROWS),
  trail: new Uint8Array(COLS * ROWS),
  mode: "setup",
  party: {
    name: "Momo Lovers Party",
    slogan: "Vote for extra chutney",
    color: "#f05d23",
    symbol: "star"
  },
  player: null,
  opponents: [],
  keys: new Set(),
  lastTime: 0,
  timeLeft: ROUND_SECONDS,
  eventClock: 12,
  boostClock: 0,
  speedMul: 1,
  toastClock: 0,
  influence: 0,
  cellSize: 12,
  offsetX: 0,
  offsetY: 0,
  shareText: ""
};

async function loadGameData() {
  try {
    const response = await fetch("data/game-data.json", { cache: "no-store" });
    if (!response.ok) throw new Error("Data file unavailable");
    state.data = await response.json();
  } catch {
    state.data = fallbackData;
  }
}

function index(x, y) {
  return y * COLS + x;
}

function clamp(value, min, max) {
  return Math.max(min, Math.min(max, value));
}

function normalizeName(value) {
  return value
    .toLowerCase()
    .replace(/[^a-z0-9 ]+/g, " ")
    .replace(/\s+/g, " ")
    .trim();
}

function validatePartyName(name) {
  const normalized = normalizeName(name);
  if (normalized.length < 3) {
    return "Party name thoda bada rakho.";
  }
  if (!/^[a-z0-9 ]+$/.test(normalized)) {
    return "Abhi prototype me English letters, numbers, aur spaces use karo.";
  }
  const blocked = state.data.blockedTerms || fallbackData.blockedTerms;
  const matched = blocked.find((term) => normalized.includes(normalizeName(term)));
  if (matched) {
    return "Fictional comedy name rakho. Real politics ya sensitive terms blocked hain.";
  }
  return "";
}

function setPartyPreview() {
  partySwatch.style.background = state.party.color;
  partyNamePreview.textContent = state.party.name;
  partySloganPreview.textContent = state.party.slogan;
}

function addFeed(text) {
  const item = document.createElement("li");
  item.textContent = text;
  feedList.prepend(item);
  while (feedList.children.length > 8) {
    feedList.lastElementChild.remove();
  }
}

function showToast(message) {
  toast.textContent = message;
  toast.classList.add("is-visible");
  state.toastClock = 2.4;
}

function resizeCanvas() {
  const rect = canvas.getBoundingClientRect();
  const dpr = window.devicePixelRatio || 1;
  canvas.width = Math.max(1, Math.floor(rect.width * dpr));
  canvas.height = Math.max(1, Math.floor(rect.height * dpr));
  ctx.setTransform(dpr, 0, 0, dpr, 0, 0);
  state.cellSize = Math.min(rect.width / COLS, rect.height / ROWS);
  state.offsetX = (rect.width - state.cellSize * COLS) / 2;
  state.offsetY = (rect.height - state.cellSize * ROWS) / 2;
}

function claimRect(ownerId, cx, cy, w, h) {
  const x0 = clamp(Math.floor(cx - w / 2), 1, COLS - 2);
  const x1 = clamp(Math.floor(cx + w / 2), 1, COLS - 2);
  const y0 = clamp(Math.floor(cy - h / 2), 1, ROWS - 2);
  const y1 = clamp(Math.floor(cy + h / 2), 1, ROWS - 2);
  for (let y = y0; y <= y1; y += 1) {
    for (let x = x0; x <= x1; x += 1) {
      state.owner[index(x, y)] = ownerId;
      state.trail[index(x, y)] = 0;
    }
  }
}

function claimDisk(ownerId, cx, cy, radius) {
  const r2 = radius * radius;
  for (let y = Math.max(1, Math.floor(cy - radius)); y <= Math.min(ROWS - 2, Math.ceil(cy + radius)); y += 1) {
    for (let x = Math.max(1, Math.floor(cx - radius)); x <= Math.min(COLS - 2, Math.ceil(cx + radius)); x += 1) {
      const dx = x - cx;
      const dy = y - cy;
      if (dx * dx + dy * dy <= r2) {
        state.owner[index(x, y)] = ownerId;
        state.trail[index(x, y)] = 0;
      }
    }
  }
}

function createAgent(ownerId, name, color, symbol, x, y) {
  return {
    ownerId,
    name,
    color,
    symbol,
    x,
    y,
    dirX: ownerId % 2 === 0 ? 1 : -1,
    dirY: 0,
    speed: 4.1,
    turnClock: 0.8 + Math.random() * 0.9,
    trailCells: []
  };
}

function resetGame() {
  state.owner.fill(0);
  state.trail.fill(0);
  state.player = createAgent(1, state.party.name, state.party.color, state.party.symbol, 31, 21);
  state.player.speed = 6.4;
  state.opponents = [];
  state.timeLeft = ROUND_SECONDS;
  state.eventClock = 8;
  state.boostClock = 0;
  state.speedMul = 1;
  state.influence = 0;
  feedList.innerHTML = "";

  claimRect(1, 31, 21, 7, 7);
  const spots = [
    [11, 10],
    [52, 11],
    [51, 32]
  ];
  state.data.opponentParties.slice(0, 3).forEach((party, i) => {
    const [x, y] = spots[i];
    const agent = createAgent(i + 2, party.name, party.color, party.symbol, x, y);
    state.opponents.push(agent);
    claimRect(agent.ownerId, x, y, 6, 6);
  });

  addFeed("District arena opened. Close a campaign loop to win influence.");
  addFeed("Use WASD, arrow keys, touch, or the control pad.");
  updateStats();
}

function startGame() {
  const name = partyNameInput.value.trim();
  const error = validatePartyName(name);
  nameError.textContent = error;
  if (error) return;

  state.party = {
    name,
    slogan: sloganInput.value.trim() || "Sabka meme, sabka game",
    color: colorInput.value,
    symbol: symbolInput.value
  };
  setPartyPreview();
  resetGame();
  setupModal.classList.remove("is-open");
  resultModal.classList.remove("is-open");
  state.mode = "playing";
  showToast(`${state.party.name} starts the campaign.`);
}

function setDirection(agent, dir) {
  if (!agent) return;
  if (dir === "up") {
    agent.dirX = 0;
    agent.dirY = -1;
  }
  if (dir === "down") {
    agent.dirX = 0;
    agent.dirY = 1;
  }
  if (dir === "left") {
    agent.dirX = -1;
    agent.dirY = 0;
  }
  if (dir === "right") {
    agent.dirX = 1;
    agent.dirY = 0;
  }
}

function applyKeyboardDirection() {
  if (state.keys.has("ArrowUp") || state.keys.has("w")) setDirection(state.player, "up");
  if (state.keys.has("ArrowDown") || state.keys.has("s")) setDirection(state.player, "down");
  if (state.keys.has("ArrowLeft") || state.keys.has("a")) setDirection(state.player, "left");
  if (state.keys.has("ArrowRight") || state.keys.has("d")) setDirection(state.player, "right");
}

function cellFromAgent(agent) {
  return {
    x: clamp(Math.floor(agent.x), 0, COLS - 1),
    y: clamp(Math.floor(agent.y), 0, ROWS - 1)
  };
}

function recordTrail(agent, x, y) {
  const idx = index(x, y);
  if (state.owner[idx] === agent.ownerId) {
    if (agent.trailCells.length > 2) closeLoop(agent);
    return;
  }
  if (state.trail[idx] === agent.ownerId) {
    if (agent.trailCells[agent.trailCells.length - 1] === idx) return;
    if (agent.ownerId === 1) finishRound(false, "Your rally route folded into itself. The poster team is confused.");
    return;
  }
  if (state.trail[idx] && state.trail[idx] !== agent.ownerId) {
    const cutOwner = state.trail[idx];
    clearTrail(cutOwner);
    if (cutOwner === 1) {
      finishRound(false, "Opposition cut your campaign route.");
    }
    return;
  }
  state.trail[idx] = agent.ownerId;
  agent.trailCells.push(idx);
}

function clearTrail(ownerId) {
  for (let i = 0; i < state.trail.length; i += 1) {
    if (state.trail[i] === ownerId) state.trail[i] = 0;
  }
  const agent = ownerId === 1 ? state.player : state.opponents.find((item) => item.ownerId === ownerId);
  if (agent) agent.trailCells = [];
}

function closeLoop(agent) {
  for (const idx of agent.trailCells) {
    state.owner[idx] = agent.ownerId;
    state.trail[idx] = 0;
  }

  const visited = new Uint8Array(COLS * ROWS);
  const queue = [];
  const push = (x, y) => {
    const idx = index(x, y);
    if (visited[idx] || state.owner[idx] === agent.ownerId) return;
    visited[idx] = 1;
    queue.push([x, y]);
  };

  for (let x = 0; x < COLS; x += 1) {
    push(x, 0);
    push(x, ROWS - 1);
  }
  for (let y = 0; y < ROWS; y += 1) {
    push(0, y);
    push(COLS - 1, y);
  }

  for (let i = 0; i < queue.length; i += 1) {
    const [x, y] = queue[i];
    if (x > 0) push(x - 1, y);
    if (x < COLS - 1) push(x + 1, y);
    if (y > 0) push(x, y - 1);
    if (y < ROWS - 1) push(x, y + 1);
  }

  let gained = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (!visited[i] && state.owner[i] !== agent.ownerId) {
      state.owner[i] = agent.ownerId;
      state.trail[i] = 0;
      gained += 1;
    }
  }

  if (agent.ownerId === 1 && agent.trailCells.length + gained > 5) {
    addFeed(`${state.party.name} won ${agent.trailCells.length + gained} new booths.`);
    showToast("Campaign loop closed. Influence gained.");
  }
  agent.trailCells = [];
}

function updateAgent(agent, dt) {
  const speed = agent.ownerId === 1 ? agent.speed * state.speedMul : agent.speed;
  agent.x = clamp(agent.x + agent.dirX * speed * dt, 1, COLS - 1.01);
  agent.y = clamp(agent.y + agent.dirY * speed * dt, 1, ROWS - 1.01);
  const { x, y } = cellFromAgent(agent);
  recordTrail(agent, x, y);
}

function updateOpponents(dt) {
  for (const agent of state.opponents) {
    agent.turnClock -= dt;
    const { x, y } = cellFromAgent(agent);
    const nearWall = x <= 2 || x >= COLS - 3 || y <= 2 || y >= ROWS - 3;
    if (agent.turnClock <= 0 || nearWall || Math.random() < 0.01) {
      const dirs = [
        ["up", 0, -1],
        ["down", 0, 1],
        ["left", -1, 0],
        ["right", 1, 0]
      ];
      const choice = dirs[Math.floor(Math.random() * dirs.length)];
      agent.dirX = choice[1];
      agent.dirY = choice[2];
      agent.turnClock = 0.45 + Math.random() * 1.2;
    }
    updateAgent(agent, dt);
  }
}

function handleTrailCuts() {
  if (!state.player) return;
  const playerCell = cellFromAgent(state.player);
  const playerIdx = index(playerCell.x, playerCell.y);
  if (state.trail[playerIdx] > 1) {
    const ownerId = state.trail[playerIdx];
    clearTrail(ownerId);
    addFeed("You cut an opponent rally route. Their posters came down.");
    showToast("Opponent route cut.");
  }

  for (const agent of state.opponents) {
    const cell = cellFromAgent(agent);
    const idx = index(cell.x, cell.y);
    if (state.trail[idx] === 1) {
      finishRound(false, `${agent.name} cut your rally route.`);
      return;
    }
  }
}

function triggerEvent() {
  const event = state.data.randomEvents[Math.floor(Math.random() * state.data.randomEvents.length)];
  eventStat.textContent = event.title;
  addFeed(event.title);
  showToast(event.copy);

  if (event.effect === "speedUp") {
    state.speedMul = 1.28;
    state.boostClock = 5;
  } else if (event.effect === "speedDown") {
    state.speedMul = 0.82;
    state.boostClock = 5;
  } else if (event.effect === "claimBurst") {
    claimDisk(1, state.player.x, state.player.y, 3.8);
  } else if (event.effect === "claimLine") {
    for (const idx of state.player.trailCells.slice(-16)) {
      state.owner[idx] = 1;
      state.trail[idx] = 0;
    }
    state.player.trailCells = [];
  }
}

function updateStats() {
  let playerCells = 0;
  for (const cell of state.owner) {
    if (cell === 1) playerCells += 1;
  }
  state.influence = Math.round((playerCells / state.owner.length) * 100);
  influenceStat.textContent = `${state.influence}%`;
  timeStat.textContent = `${Math.ceil(state.timeLeft)}s`;
}

function finishRound(won, reason) {
  if (state.mode !== "playing") return;
  state.mode = "result";
  clearTrail(1);
  const victory = won || state.influence >= 45;
  const headline = victory
    ? `${state.party.name} wins the district mandate`
    : `${state.party.name} gets a comedy recount`;
  const copy = victory
    ? `${state.influence}% influence, one namaste leader, and enough posters for a viral reel.`
    : `${reason} Final influence: ${state.influence}%. New strategy meeting starts now.`;
  state.shareText = `NETA JI: ${state.party.name} scored ${state.influence}% influence. ${state.party.slogan}`;
  resultHeadline.textContent = headline;
  resultCopy.textContent = copy;
  resultModal.classList.add("is-open");
}

function update(dt) {
  if (state.mode !== "playing") return;
  applyKeyboardDirection();
  state.timeLeft -= dt;
  state.eventClock -= dt;
  state.toastClock -= dt;
  if (state.toastClock <= 0) toast.classList.remove("is-visible");

  if (state.boostClock > 0) {
    state.boostClock -= dt;
    if (state.boostClock <= 0) state.speedMul = 1;
  }

  updateAgent(state.player, dt);
  updateOpponents(dt);
  handleTrailCuts();

  if (state.eventClock <= 0) {
    triggerEvent();
    state.eventClock = 12 + Math.random() * 8;
  }

  updateStats();
  if (state.influence >= 55) {
    finishRound(true, "District mandate reached.");
  } else if (state.timeLeft <= 0) {
    finishRound(state.influence >= 35, "The campaign clock ended.");
  }
}

function ownerColor(ownerId) {
  if (ownerId === 1) return state.party.color;
  const opponent = state.opponents.find((agent) => agent.ownerId === ownerId);
  return opponent ? opponent.color : "#cbc2b3";
}

function drawGridBackground(width, height) {
  ctx.fillStyle = "#ebe4d4";
  ctx.fillRect(0, 0, width, height);

  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  ctx.strokeStyle = "rgba(21, 21, 21, 0.06)";
  ctx.lineWidth = 1;
  for (let x = 0; x <= COLS; x += 4) {
    ctx.beginPath();
    ctx.moveTo(x * state.cellSize, 0);
    ctx.lineTo(x * state.cellSize, ROWS * state.cellSize);
    ctx.stroke();
  }
  for (let y = 0; y <= ROWS; y += 4) {
    ctx.beginPath();
    ctx.moveTo(0, y * state.cellSize);
    ctx.lineTo(COLS * state.cellSize, y * state.cellSize);
    ctx.stroke();
  }
  ctx.restore();
}

function drawSymbol(symbol, cx, cy, size, color) {
  ctx.save();
  ctx.translate(cx, cy);
  ctx.strokeStyle = "#151515";
  ctx.fillStyle = color;
  ctx.lineWidth = Math.max(1.5, size * 0.12);
  if (symbol === "star") {
    ctx.beginPath();
    for (let i = 0; i < 10; i += 1) {
      const angle = -Math.PI / 2 + (i * Math.PI) / 5;
      const radius = i % 2 === 0 ? size * 0.48 : size * 0.2;
      const x = Math.cos(angle) * radius;
      const y = Math.sin(angle) * radius;
      if (i === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    }
    ctx.closePath();
    ctx.fill();
    ctx.stroke();
  } else if (symbol === "kite") {
    ctx.beginPath();
    ctx.moveTo(0, -size * 0.5);
    ctx.lineTo(size * 0.42, 0);
    ctx.lineTo(0, size * 0.52);
    ctx.lineTo(-size * 0.42, 0);
    ctx.closePath();
    ctx.fill();
    ctx.stroke();
  } else if (symbol === "mic") {
    ctx.fillRect(-size * 0.18, -size * 0.45, size * 0.36, size * 0.52);
    ctx.strokeRect(-size * 0.18, -size * 0.45, size * 0.36, size * 0.52);
    ctx.beginPath();
    ctx.moveTo(0, size * 0.08);
    ctx.lineTo(0, size * 0.46);
    ctx.moveTo(-size * 0.24, size * 0.46);
    ctx.lineTo(size * 0.24, size * 0.46);
    ctx.stroke();
  } else if (symbol === "cup") {
    ctx.fillRect(-size * 0.34, -size * 0.24, size * 0.56, size * 0.46);
    ctx.strokeRect(-size * 0.34, -size * 0.24, size * 0.56, size * 0.46);
    ctx.beginPath();
    ctx.arc(size * 0.22, 0, size * 0.2, -Math.PI / 2, Math.PI / 2);
    ctx.stroke();
  } else {
    ctx.beginPath();
    ctx.arc(0, 0, size * 0.44, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
    for (let i = 0; i < 8; i += 1) {
      const angle = (i * Math.PI) / 4;
      ctx.beginPath();
      ctx.moveTo(0, 0);
      ctx.lineTo(Math.cos(angle) * size * 0.44, Math.sin(angle) * size * 0.44);
      ctx.stroke();
    }
  }
  ctx.restore();
}

function drawTerritory() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      const idx = index(x, y);
      const ownerId = state.owner[idx];
      if (!ownerId) continue;
      ctx.fillStyle = ownerColor(ownerId);
      ctx.globalAlpha = ownerId === 1 ? 0.82 : 0.62;
      ctx.fillRect(x * s, y * s, s + 0.5, s + 0.5);

      if (ownerId === 1 && x % 8 === 2 && y % 6 === 2) {
        ctx.globalAlpha = 1;
        ctx.fillStyle = "#fffdf7";
        ctx.fillRect(x * s + s * 0.1, y * s + s * 0.18, s * 2.4, s * 1.2);
        ctx.strokeStyle = "#151515";
        ctx.lineWidth = 1;
        ctx.strokeRect(x * s + s * 0.1, y * s + s * 0.18, s * 2.4, s * 1.2);
        ctx.fillStyle = "#151515";
        ctx.font = `${Math.max(7, s * 0.58)}px ui-sans-serif`;
        ctx.fillText("NETA JI", x * s + s * 0.22, y * s + s * 0.98);
      }
    }
  }
  ctx.globalAlpha = 1;
  ctx.restore();
}

function drawTrails() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      const ownerId = state.trail[index(x, y)];
      if (!ownerId) continue;
      ctx.fillStyle = ownerColor(ownerId);
      ctx.globalAlpha = ownerId === 1 ? 1 : 0.8;
      ctx.fillRect(x * s + s * 0.12, y * s + s * 0.12, s * 0.76, s * 0.76);
    }
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawAgent(agent, isPlayer) {
  const s = state.cellSize;
  const cx = state.offsetX + (agent.x + 0.5) * s;
  const cy = state.offsetY + (agent.y + 0.5) * s;
  ctx.save();
  ctx.fillStyle = agent.color;
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(2, s * 0.18);
  ctx.beginPath();
  ctx.arc(cx, cy, s * (isPlayer ? 0.9 : 0.74), 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  drawSymbol(agent.symbol, cx, cy, s * (isPlayer ? 1.15 : 0.9), "#fffdf7");
  if (isPlayer) {
    ctx.fillStyle = "#151515";
    ctx.font = `900 ${Math.max(10, s * 0.8)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.fillText("JI", cx, cy + s * 1.7);
  }
  ctx.restore();
}

function drawLeaderStatue() {
  const s = state.cellSize;
  const x = state.offsetX + 31 * s;
  const y = state.offsetY + 19 * s;
  ctx.save();
  ctx.translate(x, y);
  ctx.fillStyle = "#f0b37e";
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(1.5, s * 0.12);
  ctx.beginPath();
  ctx.arc(0, 0, s * 0.9, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#fffdf7";
  ctx.fillRect(-s * 0.8, s * 0.75, s * 1.6, s * 1.6);
  ctx.strokeRect(-s * 0.8, s * 0.75, s * 1.6, s * 1.6);
  ctx.fillStyle = "#f0b37e";
  ctx.save();
  ctx.rotate(-0.46);
  ctx.fillRect(-s * 0.55, s * 0.88, s * 0.44, s * 1.15);
  ctx.strokeRect(-s * 0.55, s * 0.88, s * 0.44, s * 1.15);
  ctx.restore();
  ctx.save();
  ctx.rotate(0.46);
  ctx.fillRect(s * 0.12, s * 0.88, s * 0.44, s * 1.15);
  ctx.strokeRect(s * 0.12, s * 0.88, s * 0.44, s * 1.15);
  ctx.restore();
  ctx.restore();
}

function draw() {
  const rect = canvas.getBoundingClientRect();
  drawGridBackground(rect.width, rect.height);
  drawTerritory();
  drawTrails();
  drawLeaderStatue();
  for (const opponent of state.opponents) drawAgent(opponent, false);
  if (state.player) drawAgent(state.player, true);
}

function loop(timestamp) {
  const dt = Math.min(0.05, (timestamp - state.lastTime) / 1000 || 0);
  state.lastTime = timestamp;
  update(dt);
  draw();
  requestAnimationFrame(loop);
}

function pointerToDirection(event) {
  if (!state.player || state.mode !== "playing") return;
  const rect = canvas.getBoundingClientRect();
  const px = (event.clientX - rect.left - state.offsetX) / state.cellSize;
  const py = (event.clientY - rect.top - state.offsetY) / state.cellSize;
  const dx = px - state.player.x;
  const dy = py - state.player.y;
  if (Math.abs(dx) > Math.abs(dy)) {
    setDirection(state.player, dx > 0 ? "right" : "left");
  } else {
    setDirection(state.player, dy > 0 ? "down" : "up");
  }
}

function bindEvents() {
  window.addEventListener("resize", resizeCanvas);
  window.addEventListener("keydown", (event) => {
    state.keys.add(event.key);
    if (["ArrowUp", "ArrowDown", "ArrowLeft", "ArrowRight", " "].includes(event.key)) {
      event.preventDefault();
    }
    if (event.key === " ") useRallyBoost();
  });
  window.addEventListener("keyup", (event) => state.keys.delete(event.key));
  canvas.addEventListener("pointerdown", pointerToDirection);
  canvas.addEventListener("pointermove", (event) => {
    if (event.buttons) pointerToDirection(event);
  });
  document.querySelectorAll(".dir-btn").forEach((button) => {
    button.addEventListener("click", () => setDirection(state.player, button.dataset.dir));
  });
  boostBtn.addEventListener("click", useRallyBoost);
  startBtn.addEventListener("click", startGame);
  restartBtn.addEventListener("click", () => {
    resultModal.classList.remove("is-open");
    setupModal.classList.add("is-open");
    state.mode = "setup";
  });
  shareBtn.addEventListener("click", shareResult);
  partyNameInput.addEventListener("input", () => {
    nameError.textContent = validatePartyName(partyNameInput.value.trim());
  });
}

function useRallyBoost() {
  if (state.mode !== "playing" || state.boostClock > 0) return;
  state.speedMul = 1.42;
  state.boostClock = 2.2;
  showToast("Rally sprint activated.");
}

async function shareResult() {
  const text = state.shareText || `NETA JI: ${state.party.name} is ready for a comedy mandate.`;
  try {
    if (navigator.share) {
      await navigator.share({ title: "NETA JI", text });
    } else if (navigator.clipboard) {
      await navigator.clipboard.writeText(text);
      showToast("Share text copied.");
    }
  } catch {
    showToast("Share cancelled.");
  }
}

async function init() {
  await loadGameData();
  setPartyPreview();
  bindEvents();
  resizeCanvas();
  resetGame();
  requestAnimationFrame(loop);
}

init();
