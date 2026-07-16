import assert from "node:assert/strict";
import fs from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";
import { findEnclosedCells } from "../src/capture-logic.js";
import { evaluateMandate, getWinRules } from "../src/game-rules.js";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");

function makeGrid(cols = 10, rows = 10) {
  return {
    cols,
    rows,
    regionMask: new Uint8Array(cols * rows),
    owner: new Uint8Array(cols * rows)
  };
}

function fillMask(grid, x0, y0, x1, y1) {
  for (let y = y0; y <= y1; y += 1) {
    for (let x = x0; x <= x1; x += 1) grid.regionMask[y * grid.cols + x] = 1;
  }
}

function setOwner(grid, ownerId, points) {
  for (const [x, y] of points) grid.owner[y * grid.cols + x] = ownerId;
}

function rectangleBorder(x0, y0, x1, y1) {
  const points = [];
  for (let x = x0; x <= x1; x += 1) points.push([x, y0], [x, y1]);
  for (let y = y0 + 1; y < y1; y += 1) points.push([x0, y], [x1, y]);
  return points;
}

const closed = makeGrid();
fillMask(closed, 1, 1, 8, 8);
setOwner(closed, 1, rectangleBorder(3, 3, 6, 6));
const smallCapture = findEnclosedCells({ ...closed, ownerId: 1 });
assert.deepEqual(smallCapture.sort((a, b) => a - b), [44, 45, 54, 55]);
assert.ok(smallCapture.length < closed.regionMask.reduce((sum, value) => sum + value, 0));

const open = makeGrid();
fillMask(open, 1, 1, 8, 8);
const openBorder = rectangleBorder(3, 3, 6, 6).filter(([x, y]) => !(x === 6 && y === 4));
setOwner(open, 1, openBorder);
assert.equal(findEnclosedCells({ ...open, ownerId: 1 }).length, 0);

const irregular = makeGrid(12, 9);
fillMask(irregular, 2, 1, 9, 7);
for (let y = 1; y <= 3; y += 1) irregular.regionMask[y * irregular.cols + 9] = 0;
setOwner(irregular, 1, rectangleBorder(4, 3, 7, 6));
assert.equal(findEnclosedCells({ ...irregular, ownerId: 1 }).length, 4);

assert.deepEqual(getWinRules(true), {
  minWinSeconds: 16,
  influenceTarget: 55,
  scoreTarget: 59,
  timeoutInfluence: 42,
  timeoutScore: 54
});
assert.equal(evaluateMandate({ demoMode: true, roundElapsed: 30, influence: 12, score: 18, timeLeft: 50 }), null);
assert.equal(evaluateMandate({ demoMode: true, roundElapsed: 15.9, influence: 80, score: 80, timeLeft: 50 }), null);
assert.deepEqual(
  evaluateMandate({ demoMode: true, roundElapsed: 16, influence: 55, score: 59, timeLeft: 50 }),
  { won: true, reason: "District mandate reached." }
);
assert.deepEqual(
  evaluateMandate({ demoMode: false, roundElapsed: 40, influence: 47, score: 70, timeLeft: 0 }),
  { won: false, reason: "The campaign clock ended." }
);
assert.deepEqual(
  evaluateMandate({ demoMode: false, roundElapsed: 40, influence: 48, score: 58, timeLeft: 0 }),
  { won: true, reason: "The campaign clock ended." }
);

const gameData = JSON.parse(fs.readFileSync(path.join(root, "data", "game-data.json"), "utf8"));
const supportedEffects = new Set([
  "speedUp",
  "speedDown",
  "raid",
  "teaBreak",
  "claimBurst",
  "claimLine",
  "neutral",
  "supportSwing",
  "fundsUp",
  "memeWave",
  "dholBoost",
  "posterRain"
]);
assert.ok(gameData.randomEvents.length >= 10, "Expected a varied event pool.");
for (const event of gameData.randomEvents) {
  assert.ok(event.title && event.copy, "Every event needs safe display copy.");
  assert.ok(supportedEffects.has(event.effect), `Unsupported event effect: ${event.effect}`);
}

const mapData = JSON.parse(fs.readFileSync(path.join(root, "data", "india-map-shapes.json"), "utf8"));
assert.equal(mapData.regionCount, 36);
assert.match(mapData.projection, /^normalized-(geoboundaries|cartoon)-india-/);
assert.ok(mapData.license && mapData.attribution, "Map data must carry license attribution.");

const serviceWorker = fs.readFileSync(path.join(root, "sw.js"), "utf8");
assert.match(serviceWorker, /src\/capture-logic\.js/);
assert.match(serviceWorker, /src\/game-rules\.js/);
assert.match(serviceWorker, /data\/india-map-shapes\.json/);
assert.match(serviceWorker, /neta-ji-v\d+/);

const manifest = JSON.parse(fs.readFileSync(path.join(root, "manifest.webmanifest"), "utf8"));
assert.equal(manifest.display, "standalone");
assert.equal(manifest.orientation, "any");
assert.ok(manifest.icons.some((icon) => icon.sizes === "192x192" && icon.type === "image/png"));
assert.ok(manifest.icons.some((icon) => icon.sizes === "512x512" && icon.purpose === "maskable"));

console.log(JSON.stringify({
  captureTests: 3,
  mandateRuleTests: 5,
  smallCaptureCells: smallCapture.length,
  eventsChecked: gameData.randomEvents.length,
  mapRegions: mapData.regionCount,
  pwaManifest: "ok",
  status: "ok"
}, null, 2));
