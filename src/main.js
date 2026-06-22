const COLS = 64;
const ROWS = 42;
const ROUND_SECONDS = 90;
const PROGRESS_KEY = "netaJiCampaignProgressV1";
const MAP_ASSET_URL = "assets/india-map.jpg";

const REGIONS = [
  { id: "andhra-pradesh", name: "Andhra Pradesh", type: "State" },
  { id: "arunachal-pradesh", name: "Arunachal Pradesh", type: "State" },
  { id: "assam", name: "Assam", type: "State" },
  { id: "bihar", name: "Bihar", type: "State" },
  { id: "chhattisgarh", name: "Chhattisgarh", type: "State" },
  { id: "goa", name: "Goa", type: "State" },
  { id: "gujarat", name: "Gujarat", type: "State" },
  { id: "haryana", name: "Haryana", type: "State" },
  { id: "himachal-pradesh", name: "Himachal Pradesh", type: "State" },
  { id: "jharkhand", name: "Jharkhand", type: "State" },
  { id: "karnataka", name: "Karnataka", type: "State" },
  { id: "kerala", name: "Kerala", type: "State" },
  { id: "madhya-pradesh", name: "Madhya Pradesh", type: "State" },
  { id: "maharashtra", name: "Maharashtra", type: "State" },
  { id: "manipur", name: "Manipur", type: "State" },
  { id: "meghalaya", name: "Meghalaya", type: "State" },
  { id: "mizoram", name: "Mizoram", type: "State" },
  { id: "nagaland", name: "Nagaland", type: "State" },
  { id: "odisha", name: "Odisha", type: "State" },
  { id: "punjab", name: "Punjab", type: "State" },
  { id: "rajasthan", name: "Rajasthan", type: "State" },
  { id: "sikkim", name: "Sikkim", type: "State" },
  { id: "tamil-nadu", name: "Tamil Nadu", type: "State" },
  { id: "telangana", name: "Telangana", type: "State" },
  { id: "tripura", name: "Tripura", type: "State" },
  { id: "uttar-pradesh", name: "Uttar Pradesh", type: "State" },
  { id: "uttarakhand", name: "Uttarakhand", type: "State" },
  { id: "west-bengal", name: "West Bengal", type: "State" },
  { id: "andaman-nicobar", name: "Andaman and Nicobar Islands", type: "UT" },
  { id: "chandigarh", name: "Chandigarh", type: "UT" },
  { id: "dadra-daman-diu", name: "Dadra and Nagar Haveli and Daman and Diu", type: "UT" },
  { id: "delhi", name: "Delhi", type: "UT" },
  { id: "jammu-kashmir", name: "Jammu and Kashmir", type: "UT" },
  { id: "ladakh", name: "Ladakh", type: "UT" },
  { id: "lakshadweep", name: "Lakshadweep", type: "UT" },
  { id: "puducherry", name: "Puducherry", type: "UT" }
];

const REGION_POSITIONS = {
  "andhra-pradesh": [0.54, 0.68],
  "arunachal-pradesh": [0.89, 0.25],
  assam: [0.83, 0.34],
  bihar: [0.64, 0.39],
  chhattisgarh: [0.5, 0.52],
  goa: [0.34, 0.72],
  gujarat: [0.25, 0.49],
  haryana: [0.4, 0.32],
  "himachal-pradesh": [0.43, 0.23],
  jharkhand: [0.58, 0.48],
  karnataka: [0.42, 0.7],
  kerala: [0.43, 0.86],
  "madhya-pradesh": [0.42, 0.48],
  maharashtra: [0.38, 0.59],
  manipur: [0.86, 0.43],
  meghalaya: [0.78, 0.38],
  mizoram: [0.85, 0.5],
  nagaland: [0.89, 0.39],
  odisha: [0.58, 0.57],
  punjab: [0.36, 0.28],
  rajasthan: [0.3, 0.38],
  sikkim: [0.7, 0.32],
  "tamil-nadu": [0.5, 0.82],
  telangana: [0.5, 0.62],
  tripura: [0.81, 0.47],
  "uttar-pradesh": [0.49, 0.36],
  uttarakhand: [0.48, 0.28],
  "west-bengal": [0.68, 0.47],
  "andaman-nicobar": [0.83, 0.85],
  chandigarh: [0.39, 0.29],
  "dadra-daman-diu": [0.29, 0.55],
  delhi: [0.43, 0.34],
  "jammu-kashmir": [0.35, 0.18],
  ladakh: [0.48, 0.16],
  lakshadweep: [0.25, 0.82],
  puducherry: [0.55, 0.82]
};

const mapImage = new Image();
mapImage.src = MAP_ASSET_URL;

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
    "shiv sena",
    "tmc",
    "trinamool",
    "samajwadi",
    "bahujan",
    "bsp",
    "dmk",
    "aiadmk",
    "ncp",
    "jdu",
    "rjd",
    "brs",
    "trs",
    "tdp",
    "ysr",
    "janata dal",
    "rss",
    "vhp",
    "nitish",
    "mamata",
    "stalin",
    "akhilesh",
    "mayawati",
    "uddhav",
    "shinde",
    "pawar",
    "lalu",
    "naidu",
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
    "nazi",
    "bomb",
    "gun",
    "riot",
    "attack",
    "hate",
    "abuse",
    "slur",
    "porn",
    "rape"
  ]
};

const canvas = document.querySelector("#gameCanvas");
const ctx = canvas.getContext("2d");
const setupModal = document.querySelector("#setupModal");
const regionModal = document.querySelector("#regionModal");
const resultModal = document.querySelector("#resultModal");
const confirmPanel = document.querySelector("#confirmPanel");
const confirmTitle = document.querySelector("#confirmTitle");
const confirmCopy = document.querySelector("#confirmCopy");
const partyNameInput = document.querySelector("#partyNameInput");
const sloganInput = document.querySelector("#sloganInput");
const colorInput = document.querySelector("#colorInput");
const symbolInput = document.querySelector("#symbolInput");
const nameError = document.querySelector("#nameError");
const startBtn = document.querySelector("#startBtn");
const restartBtn = document.querySelector("#restartBtn");
const nextRegionBtn = document.querySelector("#nextRegionBtn");
const shareBtn = document.querySelector("#shareBtn");
const boostBtn = document.querySelector("#boostBtn");
const openMapBtn = document.querySelector("#openMapBtn");
const confirmRegionBtn = document.querySelector("#confirmRegionBtn");
const cancelRegionBtn = document.querySelector("#cancelRegionBtn");
const influenceStat = document.querySelector("#influenceStat");
const timeStat = document.querySelector("#timeStat");
const eventStat = document.querySelector("#eventStat");
const regionStat = document.querySelector("#regionStat");
const toast = document.querySelector("#toast");
const feedList = document.querySelector("#feedList");
const partySwatch = document.querySelector("#partySwatch");
const partyNamePreview = document.querySelector("#partyNamePreview");
const partySloganPreview = document.querySelector("#partySloganPreview");
const resultHeadline = document.querySelector("#resultHeadline");
const resultCopy = document.querySelector("#resultCopy");
const activeRegionCopy = document.querySelector("#activeRegionCopy");
const miniRegionGrid = document.querySelector("#miniRegionGrid");
const regionGrid = document.querySelector("#regionGrid");
const regionModalCopy = document.querySelector("#regionModalCopy");

const state = {
  data: fallbackData,
  owner: new Uint8Array(COLS * ROWS),
  trail: new Uint8Array(COLS * ROWS),
  regionMask: new Uint8Array(COLS * ROWS),
  activePolygon: [],
  mode: "setup",
  party: {
    name: "Momo Lovers Party",
    slogan: "Vote for extra chutney",
    color: "#f05d23",
    symbol: "star"
  },
  player: null,
  opponents: [],
  supporters: [],
  conversionBursts: [],
  supportersConverted: 0,
  campaign: {
    activeRegionId: null,
    pendingRegionId: null,
    completed: {},
    lastWonRegionId: null
  },
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
  mapRect: { x: 0, y: 0, width: 0, height: 0 },
  pointer: { x: 0, y: 0, active: false },
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

function validateSafeText(value, label, minLength) {
  const normalized = normalizeName(value);
  if (normalized.length < minLength) {
    return `${label} thoda bada rakho.`;
  }
  if (!/^[a-z0-9 ]+$/.test(normalized)) {
    return "Abhi prototype me English letters, numbers, aur spaces use karo.";
  }
  if (/(.)\1{4,}/.test(normalized.replace(/\s+/g, ""))) {
    return `${label} me repeated spam letters kam rakho.`;
  }
  const blocked = state.data.blockedTerms || fallbackData.blockedTerms;
  const matched = blocked.find((term) => normalized.includes(normalizeName(term)));
  if (matched) {
    return "Fictional comedy text rakho. Real politics, hate, abuse, ya sensitive terms blocked hain.";
  }
  return "";
}

function validatePartyName(name) {
  return validateSafeText(name, "Party name", 3);
}

function validateSlogan(slogan) {
  if (!slogan.trim()) return "";
  return validateSafeText(slogan, "Slogan", 4);
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

function getActiveRegion() {
  return REGIONS.find((region) => region.id === state.campaign.activeRegionId) || null;
}

function loadCampaignProgress() {
  try {
    const raw = localStorage.getItem(PROGRESS_KEY);
    if (!raw) return;
    const parsed = JSON.parse(raw);
    state.campaign.activeRegionId = parsed.activeRegionId || null;
    state.campaign.completed = parsed.completed && typeof parsed.completed === "object" ? parsed.completed : {};
    state.campaign.lastWonRegionId = parsed.lastWonRegionId || null;
  } catch {
    state.campaign.activeRegionId = null;
    state.campaign.completed = {};
    state.campaign.lastWonRegionId = null;
  }
}

function saveCampaignProgress() {
  const payload = {
    activeRegionId: state.campaign.activeRegionId,
    completed: state.campaign.completed,
    lastWonRegionId: state.campaign.lastWonRegionId
  };
  localStorage.setItem(PROGRESS_KEY, JSON.stringify(payload));
}

function renderRegionHub() {
  const activeRegion = getActiveRegion();
  regionStat.textContent = activeRegion ? activeRegion.name : "Choose State";
  activeRegionCopy.textContent = activeRegion
    ? `${activeRegion.name} is active. Won regions stay in ${state.party.name} color.`
    : "Create a party, then choose a state or UT.";
  miniRegionGrid.innerHTML = "";
  regionGrid.innerHTML = "";
  miniRegionGrid.style.setProperty("--region-color", state.party.color);
  regionGrid.style.setProperty("--region-color", state.party.color);

  for (const region of REGIONS) {
    const won = Boolean(state.campaign.completed[region.id]);
    const active = state.campaign.activeRegionId === region.id;

    const mini = document.createElement("button");
    mini.type = "button";
    mini.className = `mini-region-cell${won ? " is-won" : ""}${active ? " is-active" : ""}`;
    mini.title = `${region.name}${won ? " won" : ""}`;
    mini.setAttribute("aria-label", mini.title);
    mini.addEventListener("click", () => showRegionPrompt(region.id));
    miniRegionGrid.append(mini);

    const button = document.createElement("button");
    button.type = "button";
    button.className = `region-btn${won ? " is-won" : ""}${active ? " is-active" : ""}`;
    button.innerHTML = `${region.name}<small>${won ? "Won mandate" : active ? "Active campaign" : region.type}</small>`;
    button.addEventListener("click", () => showRegionPrompt(region.id));
    regionGrid.append(button);
  }
}

function openRegionModal() {
  setupModal.classList.remove("is-open");
  resultModal.classList.remove("is-open");
  regionModal.classList.remove("is-open");
  confirmPanel.hidden = true;
  state.campaign.pendingRegionId = null;
  renderRegionHub();
  state.mode = "map";
  showToast("Touch any black flag on the India map.");
}

function showRegionPrompt(regionId) {
  const region = REGIONS.find((item) => item.id === regionId);
  if (!region) return;
  state.campaign.pendingRegionId = region.id;
  confirmTitle.textContent = `${region.name} election?`;
  confirmCopy.textContent = state.campaign.completed[region.id]
    ? "This mandate is already won. OK to replay this region."
    : "OK dabao, phir sirf is region ka bada outline arena khulega.";
  confirmPanel.hidden = false;
  state.mode = "confirm";
}

function selectRegion(regionId) {
  const region = REGIONS.find((item) => item.id === regionId);
  if (!region) return;
  const previous = getActiveRegion();
  state.campaign.activeRegionId = region.id;
  saveCampaignProgress();
  renderRegionHub();
  regionModal.classList.remove("is-open");
  resetGame();
  resultModal.classList.remove("is-open");
  setupModal.classList.remove("is-open");
  state.mode = "playing";
  const yatraCopy = previous && previous.id !== region.id ? `Paidal yatra moved from ${previous.name} to ${region.name}.` : `${region.name} campaign opened.`;
  addFeed(yatraCopy);
  showToast(yatraCopy);
}

function confirmPendingRegion() {
  if (!state.campaign.pendingRegionId) return;
  const regionId = state.campaign.pendingRegionId;
  state.campaign.pendingRegionId = null;
  confirmPanel.hidden = true;
  selectRegion(regionId);
}

function markActiveRegionWon() {
  const activeRegion = getActiveRegion();
  if (!activeRegion) return null;
  state.campaign.completed[activeRegion.id] = {
    wonAt: Date.now(),
    influence: state.influence,
    party: state.party.name
  };
  state.campaign.lastWonRegionId = activeRegion.id;
  saveCampaignProgress();
  renderRegionHub();
  return activeRegion;
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

function hashText(value) {
  let hash = 2166136261;
  for (let i = 0; i < value.length; i += 1) {
    hash ^= value.charCodeAt(i);
    hash = Math.imul(hash, 16777619);
  }
  return hash >>> 0;
}

function seededNoise(seed, indexValue) {
  let value = seed + indexValue * 1013904223;
  value ^= value << 13;
  value ^= value >>> 17;
  value ^= value << 5;
  return ((value >>> 0) % 10000) / 10000;
}

function generateRegionPolygon(region) {
  const seed = hashText(region?.id || "district");
  const points = [];
  const count = region?.type === "UT" ? 15 : 20;
  const centerX = COLS / 2;
  const centerY = ROWS / 2;
  const radiusX = region?.type === "UT" ? 17.5 : 22.5;
  const radiusY = region?.type === "UT" ? 14.5 : 16.8;
  for (let i = 0; i < count; i += 1) {
    const t = i / count;
    const angle = Math.PI * 2 * t - Math.PI / 2;
    const wobble = 0.76 + seededNoise(seed, i) * 0.34;
    const skew = Math.sin(angle * 2 + seed * 0.00001) * 0.12;
    points.push({
      x: centerX + Math.cos(angle) * radiusX * wobble + skew * radiusX,
      y: centerY + Math.sin(angle) * radiusY * (0.84 + seededNoise(seed, i + 33) * 0.28)
    });
  }
  return points;
}

function pointInPolygon(x, y, points) {
  if (!points.length) return true;
  let inside = false;
  for (let i = 0, j = points.length - 1; i < points.length; j = i, i += 1) {
    const xi = points[i].x;
    const yi = points[i].y;
    const xj = points[j].x;
    const yj = points[j].y;
    const intersect = yi > y !== yj > y && x < ((xj - xi) * (y - yi)) / (yj - yi || 1) + xi;
    if (intersect) inside = !inside;
  }
  return inside;
}

function isMaskedCell(x, y) {
  if (!state.activePolygon.length) return true;
  if (x < 0 || x >= COLS || y < 0 || y >= ROWS) return false;
  return state.regionMask[index(Math.floor(x), Math.floor(y))] === 1;
}

function generateRegionMask(region) {
  state.regionMask.fill(0);
  state.activePolygon = generateRegionPolygon(region);
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      if (pointInPolygon(x + 0.5, y + 0.5, state.activePolygon)) {
        state.regionMask[index(x, y)] = 1;
      }
    }
  }
}

function findMaskedSpawn(preferredX, preferredY) {
  if (isMaskedCell(preferredX, preferredY)) return { x: preferredX, y: preferredY };
  for (let r = 1; r < 22; r += 1) {
    for (let y = Math.max(1, Math.floor(preferredY - r)); y <= Math.min(ROWS - 2, Math.ceil(preferredY + r)); y += 1) {
      for (let x = Math.max(1, Math.floor(preferredX - r)); x <= Math.min(COLS - 2, Math.ceil(preferredX + r)); x += 1) {
        if (isMaskedCell(x, y)) return { x, y };
      }
    }
  }
  return { x: COLS / 2, y: ROWS / 2 };
}

function claimRect(ownerId, cx, cy, w, h) {
  const x0 = clamp(Math.floor(cx - w / 2), 1, COLS - 2);
  const x1 = clamp(Math.floor(cx + w / 2), 1, COLS - 2);
  const y0 = clamp(Math.floor(cy - h / 2), 1, ROWS - 2);
  const y1 = clamp(Math.floor(cy + h / 2), 1, ROWS - 2);
  for (let y = y0; y <= y1; y += 1) {
    for (let x = x0; x <= x1; x += 1) {
      if (!isMaskedCell(x, y)) continue;
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
      if (dx * dx + dy * dy <= r2 && isMaskedCell(x, y)) {
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
    trailCells: [],
    trailPoints: []
  };
}

function createSupporter(source, color) {
  const angle = Math.random() * Math.PI * 2;
  const distance = 0.3 + Math.random() * 1.4;
  return {
    x: clamp(source.x + Math.cos(angle) * distance, 1, COLS - 1.01),
    y: clamp(source.y + Math.sin(angle) * distance, 1, ROWS - 1.01),
    color,
    phase: Math.random() * Math.PI * 2,
    driftX: (Math.random() - 0.5) * 2.6,
    driftY: (Math.random() - 0.5) * 2.6,
    spread: 0.7 + Math.random() * 1.9,
    followSpeed: 4.4 + Math.random() * 2.2,
    size: 0.36 + Math.random() * 0.18
  };
}

function addConversionBurst(x, y, color) {
  for (let i = 0; i < 18; i += 1) {
    const angle = Math.random() * Math.PI * 2;
    const speed = 2 + Math.random() * 5;
    state.conversionBursts.push({
      x,
      y,
      color,
      vx: Math.cos(angle) * speed,
      vy: Math.sin(angle) * speed,
      life: 0.45 + Math.random() * 0.45,
      maxLife: 0.9,
      size: 0.22 + Math.random() * 0.28
    });
  }
}

function convertOpponent(ownerId, cutX, cutY) {
  const opponentIndex = state.opponents.findIndex((agent) => agent.ownerId === ownerId);
  if (opponentIndex === -1) return false;

  const [opponent] = state.opponents.splice(opponentIndex, 1);
  let convertedCells = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (state.owner[i] === ownerId) {
      state.owner[i] = 1;
      convertedCells += 1;
    }
    if (state.trail[i] === ownerId) {
      state.trail[i] = 0;
      convertedCells += 1;
    }
  }

  const joinPoint = {
    x: typeof cutX === "number" ? cutX : opponent.x,
    y: typeof cutY === "number" ? cutY : opponent.y
  };
  const joined = clamp(5 + Math.floor(convertedCells / 18), 5, 16);
  for (let i = 0; i < joined; i += 1) {
    state.supporters.push(createSupporter(joinPoint, opponent.color));
  }
  while (state.supporters.length > 42) state.supporters.shift();
  state.supportersConverted += joined;
  addConversionBurst(joinPoint.x, joinPoint.y, opponent.color);
  addFeed(`${opponent.name} joined your rally. +${joined} supporters.`);
  showToast(`${opponent.name} converted. Supporters joined.`);
  if (state.opponents.length === 0) {
    addFeed("All local rivals joined your rally. Keep winning booths.");
  }
  updateStats();
  return true;
}

function resetGame() {
  const activeRegion = getActiveRegion();
  generateRegionMask(activeRegion);
  state.owner.fill(0);
  state.trail.fill(0);
  const playerSpawn = findMaskedSpawn(31, 21);
  state.player = createAgent(1, state.party.name, state.party.color, state.party.symbol, playerSpawn.x, playerSpawn.y);
  state.player.speed = 6.4;
  state.player.dirX = 0;
  state.player.dirY = 0;
  state.opponents = [];
  state.supporters = [];
  state.conversionBursts = [];
  state.supportersConverted = 0;
  state.keys.clear();
  state.timeLeft = ROUND_SECONDS;
  state.eventClock = 8;
  state.boostClock = 0;
  state.speedMul = 1;
  state.influence = 0;
  feedList.innerHTML = "";

  claimDisk(1, state.player.x, state.player.y, 3.8);
  const spots = [
    [11, 10],
    [52, 11],
    [51, 32]
  ];
  state.data.opponentParties.slice(0, 3).forEach((party, i) => {
    const [sx, sy] = spots[i];
    const { x, y } = findMaskedSpawn(sx, sy);
    const agent = createAgent(i + 2, party.name, party.color, party.symbol, x, y);
    state.opponents.push(agent);
    claimDisk(agent.ownerId, x, y, 3.2);
  });

  addFeed(`${activeRegion ? activeRegion.name : "District"} arena opened. Close a campaign loop to win influence.`);
  addFeed("Use WASD, arrow keys, touch, or the control pad.");
  updateStats();
}

function startGame() {
  const name = partyNameInput.value.trim();
  const slogan = sloganInput.value.trim();
  const error = validatePartyName(name) || validateSlogan(slogan);
  nameError.textContent = error;
  if (error) return;

  state.party = {
    name,
    slogan: slogan || "Sabka meme, sabka game",
    color: colorInput.value,
    symbol: symbolInput.value
  };
  setPartyPreview();
  renderRegionHub();
  openRegionModal();
  showToast(`${state.party.name} is ready. Choose a state or UT.`);
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
    // Self-crossing is allowed so players can draw messier, more natural routes.
    return;
  }
  if (state.trail[idx] && state.trail[idx] !== agent.ownerId) {
    const cutOwner = state.trail[idx];
    if (agent.ownerId === 1 && cutOwner > 1) {
      convertOpponent(cutOwner, x, y);
      return;
    }
    clearTrail(cutOwner);
    if (cutOwner === 1) {
      finishRound(false, "Opposition cut your campaign route.");
    }
    return;
  }
  state.trail[idx] = agent.ownerId;
  agent.trailCells.push(idx);
  agent.trailPoints.push({ x: agent.x, y: agent.y });
  if (agent.trailPoints.length > 180) agent.trailPoints.shift();
}

function clearTrail(ownerId) {
  for (let i = 0; i < state.trail.length; i += 1) {
    if (state.trail[i] === ownerId) state.trail[i] = 0;
  }
  const agent = ownerId === 1 ? state.player : state.opponents.find((item) => item.ownerId === ownerId);
  if (agent) {
    agent.trailCells = [];
    agent.trailPoints = [];
  }
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
    if (!isMaskedCell(x, y) || visited[idx] || state.owner[idx] === agent.ownerId) return;
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
    if (state.regionMask[i] && !visited[i] && state.owner[i] !== agent.ownerId) {
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
  agent.trailPoints = [];
}

function updateAgent(agent, dt) {
  const speed = agent.ownerId === 1 ? agent.speed * state.speedMul : agent.speed;
  const nextX = clamp(agent.x + agent.dirX * speed * dt, 1, COLS - 1.01);
  const nextY = clamp(agent.y + agent.dirY * speed * dt, 1, ROWS - 1.01);
  if (isMaskedCell(nextX, nextY)) {
    agent.x = nextX;
    agent.y = nextY;
  } else if (agent.ownerId !== 1) {
    agent.dirX = -agent.dirX || (Math.random() > 0.5 ? 1 : -1);
    agent.dirY = -agent.dirY;
    agent.turnClock = 0;
  }
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
    convertOpponent(state.trail[playerIdx], playerCell.x, playerCell.y);
  }

  for (const agent of [...state.opponents]) {
    const distanceToPlayer = Math.hypot(agent.x - state.player.x, agent.y - state.player.y);
    if (distanceToPlayer < 1.15) {
      convertOpponent(agent.ownerId, agent.x, agent.y);
      continue;
    }
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
    state.player.trailPoints = [];
  }
}

function updateSupporters(dt) {
  if (!state.player || state.supporters.length === 0) return;
  const sideX = -state.player.dirY;
  const sideY = state.player.dirX;
  const backX = -state.player.dirX;
  const backY = -state.player.dirY;

  state.supporters.forEach((supporter, i) => {
    supporter.phase += dt * (1.5 + supporter.size * 2);
    const looseBack = 1.1 + supporter.spread + Math.sin(supporter.phase) * 0.38;
    const looseSide = supporter.driftX + Math.cos(supporter.phase * 0.9) * (0.72 + supporter.spread * 0.16);
    const targetX = state.player.x + backX * looseBack + sideX * looseSide + Math.sin(supporter.phase * 1.7) * 0.22;
    const targetY = state.player.y + backY * looseBack + sideY * looseSide + supporter.driftY * 0.35 + Math.cos(supporter.phase * 1.3) * 0.22;
    const dx = targetX - supporter.x;
    const dy = targetY - supporter.y;
    const distance = Math.hypot(dx, dy) || 1;
    const step = Math.min(distance, supporter.followSpeed * dt);
    supporter.x = clamp(supporter.x + (dx / distance) * step, 1, COLS - 1.01);
    supporter.y = clamp(supporter.y + (dy / distance) * step, 1, ROWS - 1.01);
  });

  for (let i = 0; i < state.supporters.length; i += 1) {
    for (let j = i + 1; j < state.supporters.length; j += 1) {
      const a = state.supporters[i];
      const b = state.supporters[j];
      const dx = b.x - a.x;
      const dy = b.y - a.y;
      const distance = Math.hypot(dx, dy) || 1;
      if (distance < 0.72) {
        const push = (0.72 - distance) * 0.22;
        const nx = dx / distance;
        const ny = dy / distance;
        a.x = clamp(a.x - nx * push, 1, COLS - 1.01);
        a.y = clamp(a.y - ny * push, 1, ROWS - 1.01);
        b.x = clamp(b.x + nx * push, 1, COLS - 1.01);
        b.y = clamp(b.y + ny * push, 1, ROWS - 1.01);
      }
    }
  }
}

function updateConversionBursts(dt) {
  if (state.conversionBursts.length === 0) return;
  for (const particle of state.conversionBursts) {
    particle.life -= dt;
    particle.x += particle.vx * dt;
    particle.y += particle.vy * dt;
    particle.vx *= 0.96;
    particle.vy *= 0.96;
  }
  state.conversionBursts = state.conversionBursts.filter((particle) => particle.life > 0);
}

function updateStats() {
  let playerCells = 0;
  let playableCells = 0;
  for (let i = 0; i < state.owner.length; i += 1) {
    if (!state.regionMask[i]) continue;
    playableCells += 1;
    if (state.owner[i] === 1) playerCells += 1;
  }
  state.influence = Math.round((playerCells / Math.max(1, playableCells)) * 100);
  const activeRegion = getActiveRegion();
  regionStat.textContent = activeRegion ? activeRegion.name : "Choose State";
  influenceStat.textContent = `${state.influence}%`;
  timeStat.textContent = `${Math.ceil(state.timeLeft)}s`;
}

function finishRound(won, reason) {
  if (state.mode !== "playing") return;
  state.mode = "result";
  clearTrail(1);
  const victory = won || state.influence >= 45;
  const activeRegion = getActiveRegion();
  const wonRegion = victory ? markActiveRegionWon() : null;
  const regionName = (wonRegion || activeRegion)?.name || "district";
  const supporterLine = state.supportersConverted
    ? ` ${state.supportersConverted} converted supporters joined the yatra.`
    : " The poster team is already posing for the reel.";
  const headline = victory
    ? `${state.party.name} wins ${regionName} mandate`
    : `${state.party.name} gets a ${regionName} recount`;
  const copy = victory
    ? `${state.influence}% influence in ${regionName}.${supporterLine}`
    : `${reason} Final influence: ${state.influence}% in ${regionName}. New strategy meeting starts now.`;
  state.shareText = `NETA JI: ${state.party.name} scored ${state.influence}% influence in ${regionName}. ${state.party.slogan}`;
  resultHeadline.textContent = headline;
  resultCopy.textContent = copy;
  nextRegionBtn.hidden = !victory;
  if (victory) {
    addFeed(`${regionName} mandate won. Red flag raised on the India map.`);
    openRegionModal();
    showToast(`${regionName} won. Choose the next black flag.`);
    return;
  }
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
  updateSupporters(dt);
  updateConversionBursts(dt);
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

function shadeHex(hex, amount) {
  const value = hex.replace("#", "");
  if (value.length !== 6) return hex;
  const next = [0, 2, 4]
    .map((start) => clamp(parseInt(value.slice(start, start + 2), 16) + amount, 0, 255).toString(16).padStart(2, "0"))
    .join("");
  return "#" + next;
}

function getRegionMapPoint(region, rect = state.mapRect) {
  const position = REGION_POSITIONS[region.id] || [0.5, 0.5];
  return {
    x: rect.x + position[0] * rect.width,
    y: rect.y + position[1] * rect.height
  };
}

function drawFlag(x, y, color, scale = 1) {
  ctx.save();
  ctx.lineWidth = 2 * scale;
  ctx.strokeStyle = "#151515";
  ctx.fillStyle = "#151515";
  ctx.beginPath();
  ctx.moveTo(x, y - 12 * scale);
  ctx.lineTo(x, y + 15 * scale);
  ctx.stroke();
  ctx.fillStyle = color;
  ctx.beginPath();
  ctx.moveTo(x + 1 * scale, y - 12 * scale);
  ctx.lineTo(x + 20 * scale, y - 8 * scale);
  ctx.lineTo(x + 2 * scale, y - 2 * scale);
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.fillStyle = "#fffdf7";
  ctx.beginPath();
  ctx.arc(x, y + 16 * scale, 4.5 * scale, 0, Math.PI * 2);
  ctx.fill();
  ctx.stroke();
  ctx.restore();
}

function drawWonPatch(region, rect) {
  const point = getRegionMapPoint(region, rect);
  const seed = hashText(region.id);
  const radius = region.type === "UT" ? 15 : 24;
  ctx.save();
  ctx.globalAlpha = 0.72;
  ctx.fillStyle = state.party.color;
  ctx.strokeStyle = "rgba(21, 21, 21, 0.55)";
  ctx.lineWidth = 2;
  ctx.beginPath();
  for (let i = 0; i < 12; i += 1) {
    const angle = (Math.PI * 2 * i) / 12;
    const wobble = 0.75 + seededNoise(seed, i) * 0.5;
    const x = point.x + Math.cos(angle) * radius * wobble;
    const y = point.y + Math.sin(angle) * radius * 0.72 * wobble;
    if (i === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  }
  ctx.closePath();
  ctx.fill();
  ctx.stroke();
  ctx.restore();
}

function drawMapHome(width, height) {
  ctx.fillStyle = "#d8edf7";
  ctx.fillRect(0, 0, width, height);
  const imgW = mapImage.naturalWidth || 235;
  const imgH = mapImage.naturalHeight || 244;
  const scale = Math.min((width * 0.95) / imgW, (height * 0.9) / imgH);
  const mapW = imgW * scale;
  const mapH = imgH * scale;
  const x = (width - mapW) / 2;
  const y = Math.max(8, (height - mapH) / 2 - 4);
  state.mapRect = { x, y, width: mapW, height: mapH };

  ctx.save();
  ctx.fillStyle = "#fffdf7";
  ctx.fillRect(x - 8, y - 8, mapW + 16, mapH + 16);
  if (mapImage.complete && mapImage.naturalWidth) {
    ctx.filter = "contrast(1.18) saturate(1.06)";
    ctx.drawImage(mapImage, x, y, mapW, mapH);
    ctx.filter = "none";
  } else {
    ctx.fillStyle = "#f7f4ec";
    ctx.fillRect(x, y, mapW, mapH);
    ctx.fillStyle = "#151515";
    ctx.font = "900 18px ui-sans-serif";
    ctx.textAlign = "center";
    ctx.fillText("India Map", x + mapW / 2, y + mapH / 2);
  }
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = 4;
  ctx.strokeRect(x - 3, y - 3, mapW + 6, mapH + 6);

  for (const region of REGIONS) {
    if (state.campaign.completed[region.id]) drawWonPatch(region, state.mapRect);
  }

  for (const region of REGIONS) {
    const point = getRegionMapPoint(region);
    const won = Boolean(state.campaign.completed[region.id]);
    const pending = state.campaign.pendingRegionId === region.id;
    if (pending) {
      ctx.fillStyle = "rgba(244, 197, 66, 0.5)";
      ctx.beginPath();
      ctx.arc(point.x + 8, point.y + 3, 18, 0, Math.PI * 2);
      ctx.fill();
    }
    drawFlag(point.x, point.y, won ? "#d92d20" : "#151515", region.type === "UT" ? 0.65 : 0.82);
  }

  ctx.fillStyle = "rgba(21, 21, 21, 0.88)";
  ctx.fillRect(14, height - 62, Math.min(width - 28, 560), 44);
  ctx.fillStyle = "#fffdf7";
  ctx.font = "900 14px ui-sans-serif";
  ctx.textAlign = "left";
  ctx.fillText("Touch a black flag to start election. Red flags are won.", 28, height - 35);
  ctx.restore();
}

function drawRegionArena() {
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  ctx.fillStyle = "rgba(21, 21, 21, 0.12)";
  for (let y = 0; y < ROWS; y += 1) {
    for (let x = 0; x < COLS; x += 1) {
      if (!state.regionMask[index(x, y)]) {
        ctx.fillRect(x * s, y * s, s + 0.5, s + 0.5);
      }
    }
  }
  ctx.strokeStyle = "#151515";
  ctx.lineWidth = Math.max(3, s * 0.32);
  ctx.lineJoin = "round";
  ctx.beginPath();
  state.activePolygon.forEach((point, i) => {
    const x = point.x * s;
    const y = point.y * s;
    if (i === 0) ctx.moveTo(x, y);
    else ctx.lineTo(x, y);
  });
  ctx.closePath();
  ctx.stroke();

  const activeRegion = getActiveRegion();
  if (activeRegion) {
    ctx.fillStyle = "rgba(21, 21, 21, 0.84)";
    ctx.font = `900 ${Math.max(12, s * 1.05)}px ui-sans-serif`;
    ctx.textAlign = "center";
    ctx.fillText(activeRegion.name, (COLS / 2) * s, 4.4 * s);
  }
  ctx.restore();
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
      if (!state.regionMask[idx]) continue;
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
      const baseColor = ownerColor(ownerId);
      const isPlayerTrail = ownerId === 1;
      ctx.fillStyle = isPlayerTrail ? shadeHex(baseColor, -55) : baseColor;
      ctx.globalAlpha = isPlayerTrail ? 1 : 0.8;
      ctx.fillRect(x * s + s * 0.05, y * s + s * 0.05, s * 0.9, s * 0.9);
      if (isPlayerTrail) {
        ctx.strokeStyle = "rgba(21, 21, 21, 0.72)";
        ctx.lineWidth = Math.max(1, s * 0.12);
        ctx.strokeRect(x * s + s * 0.05, y * s + s * 0.05, s * 0.9, s * 0.9);
      }
    }
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawTrailPaths() {
  const agents = [state.player, ...state.opponents].filter(Boolean);
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  ctx.lineCap = "round";
  ctx.lineJoin = "round";
  for (const agent of agents) {
    if (!agent.trailPoints || agent.trailPoints.length < 2) continue;
    const isPlayerTrail = agent.ownerId === 1;
    const color = isPlayerTrail ? shadeHex(ownerColor(agent.ownerId), -62) : ownerColor(agent.ownerId);
    ctx.globalAlpha = isPlayerTrail ? 0.96 : 0.72;
    ctx.strokeStyle = color;
    ctx.lineWidth = isPlayerTrail ? s * 0.86 : s * 0.64;
    ctx.beginPath();
    agent.trailPoints.forEach((point, i) => {
      const x = (point.x + 0.5) * s;
      const y = (point.y + 0.5) * s;
      if (i === 0) ctx.moveTo(x, y);
      else ctx.lineTo(x, y);
    });
    ctx.stroke();
    if (isPlayerTrail) {
      ctx.globalAlpha = 0.9;
      ctx.strokeStyle = "rgba(21, 21, 21, 0.62)";
      ctx.lineWidth = Math.max(1, s * 0.12);
      ctx.stroke();
    }
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawSupporters() {
  if (state.supporters.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (const supporter of state.supporters) {
    const cx = (supporter.x + 0.5) * s;
    const cy = (supporter.y + 0.5) * s;
    ctx.globalAlpha = 0.96;
    ctx.fillStyle = supporter.color;
    ctx.strokeStyle = "rgba(21, 21, 21, 0.82)";
    ctx.lineWidth = Math.max(1, s * 0.1);
    ctx.beginPath();
    ctx.arc(cx, cy, s * supporter.size, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
    ctx.fillStyle = "#fffdf7";
    ctx.beginPath();
    ctx.arc(cx, cy - s * supporter.size * 0.16, s * supporter.size * 0.34, 0, Math.PI * 2);
    ctx.fill();
  }
  ctx.restore();
  ctx.globalAlpha = 1;
}

function drawConversionBursts() {
  if (state.conversionBursts.length === 0) return;
  ctx.save();
  ctx.translate(state.offsetX, state.offsetY);
  const s = state.cellSize;
  for (const particle of state.conversionBursts) {
    const alpha = clamp(particle.life / particle.maxLife, 0, 1);
    ctx.globalAlpha = alpha;
    ctx.fillStyle = particle.color;
    ctx.strokeStyle = "rgba(21, 21, 21, 0.72)";
    ctx.lineWidth = Math.max(1, s * 0.08);
    ctx.beginPath();
    ctx.arc((particle.x + 0.5) * s, (particle.y + 0.5) * s, s * particle.size, 0, Math.PI * 2);
    ctx.fill();
    ctx.stroke();
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
  if (document.body.dataset.mode !== state.mode) {
    document.body.dataset.mode = state.mode;
  }
  if (state.mode === "map" || state.mode === "confirm") {
    drawMapHome(rect.width, rect.height);
    return;
  }
  drawGridBackground(rect.width, rect.height);
  drawRegionArena();
  drawTerritory();
  drawTrails();
  drawTrailPaths();
  drawLeaderStatue();
  drawConversionBursts();
  for (const opponent of state.opponents) drawAgent(opponent, false);
  drawSupporters();
  if (state.player) drawAgent(state.player, true);
}

function loop(timestamp) {
  const dt = Math.min(0.05, (timestamp - state.lastTime) / 1000 || 0);
  state.lastTime = timestamp;
  update(dt);
  draw();
  requestAnimationFrame(loop);
}

function canvasPointFromEvent(event) {
  const rect = canvas.getBoundingClientRect();
  return {
    x: event.clientX - rect.left,
    y: event.clientY - rect.top
  };
}

function pickRegionFromMap(point) {
  let best = null;
  let bestDistance = Infinity;
  for (const region of REGIONS) {
    const flag = getRegionMapPoint(region);
    const distance = Math.hypot(point.x - flag.x, point.y - flag.y);
    const threshold = region.type === "UT" ? 24 : 30;
    if (distance < threshold && distance < bestDistance) {
      best = region;
      bestDistance = distance;
    }
  }
  return best;
}

function handleMapPointer(event) {
  const region = pickRegionFromMap(canvasPointFromEvent(event));
  if (region) {
    showRegionPrompt(region.id);
  } else {
    confirmPanel.hidden = true;
    state.campaign.pendingRegionId = null;
  }
}

function pointerToDirection(event) {
  if (state.mode === "map" || state.mode === "confirm") {
    handleMapPointer(event);
    return;
  }
  if (!state.player || state.mode !== "playing") return;
  const point = canvasPointFromEvent(event);
  const px = (point.x - state.offsetX) / state.cellSize;
  const py = (point.y - state.offsetY) / state.cellSize;
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
  canvas.addEventListener("pointerdown", (event) => {
    const point = canvasPointFromEvent(event);
    state.pointer = { x: point.x, y: point.y, active: true };
    pointerToDirection(event);
  });
  canvas.addEventListener("pointermove", (event) => {
    if (!state.pointer.active || state.mode !== "playing") return;
    const point = canvasPointFromEvent(event);
    const dx = point.x - state.pointer.x;
    const dy = point.y - state.pointer.y;
    if (Math.hypot(dx, dy) > 12) {
      if (Math.abs(dx) > Math.abs(dy)) setDirection(state.player, dx > 0 ? "right" : "left");
      else setDirection(state.player, dy > 0 ? "down" : "up");
      state.pointer = { x: point.x, y: point.y, active: true };
    }
  });
  canvas.addEventListener("pointerup", () => {
    state.pointer.active = false;
  });
  canvas.addEventListener("pointercancel", () => {
    state.pointer.active = false;
  });
  document.querySelectorAll(".dir-btn").forEach((button) => {
    button.addEventListener("click", () => setDirection(state.player, button.dataset.dir));
  });
  boostBtn.addEventListener("click", useRallyBoost);
  startBtn.addEventListener("click", startGame);
  restartBtn.addEventListener("click", () => {
    resultModal.classList.remove("is-open");
    if (getActiveRegion()) {
      selectRegion(state.campaign.activeRegionId);
    } else {
      setupModal.classList.add("is-open");
      state.mode = "setup";
    }
  });
  nextRegionBtn.addEventListener("click", openRegionModal);
  openMapBtn.addEventListener("click", openRegionModal);
  confirmRegionBtn.addEventListener("click", confirmPendingRegion);
  cancelRegionBtn.addEventListener("click", () => {
    state.campaign.pendingRegionId = null;
    confirmPanel.hidden = true;
    state.mode = "map";
  });
  shareBtn.addEventListener("click", shareResult);
  partyNameInput.addEventListener("input", () => {
    nameError.textContent = validatePartyName(partyNameInput.value.trim()) || validateSlogan(sloganInput.value.trim());
  });
  sloganInput.addEventListener("input", () => {
    nameError.textContent = validatePartyName(partyNameInput.value.trim()) || validateSlogan(sloganInput.value.trim());
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
  loadCampaignProgress();
  setPartyPreview();
  renderRegionHub();
  bindEvents();
  resizeCanvas();
  resetGame();
  requestAnimationFrame(loop);
}

init();
