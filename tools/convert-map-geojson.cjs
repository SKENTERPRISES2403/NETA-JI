const fs = require("fs");
const path = require("path");

const inputPath = process.argv[2];
const outputPath = process.argv[3] || path.resolve(__dirname, "..", "data", "india-map-shapes.json");

if (!inputPath) {
  console.error("Usage: node tools/convert-map-geojson.cjs <geojson> [output-json]");
  process.exit(1);
}

const ISO_TO_ID = {
  "IN-AP": "andhra-pradesh",
  "IN-AR": "arunachal-pradesh",
  "IN-AS": "assam",
  "IN-BR": "bihar",
  "IN-CT": "chhattisgarh",
  "IN-GA": "goa",
  "IN-GJ": "gujarat",
  "IN-HR": "haryana",
  "IN-HP": "himachal-pradesh",
  "IN-JH": "jharkhand",
  "IN-KA": "karnataka",
  "IN-KL": "kerala",
  "IN-MP": "madhya-pradesh",
  "IN-MH": "maharashtra",
  "IN-MN": "manipur",
  "IN-ML": "meghalaya",
  "IN-MZ": "mizoram",
  "IN-NL": "nagaland",
  "IN-OR": "odisha",
  "IN-PB": "punjab",
  "IN-RJ": "rajasthan",
  "IN-SK": "sikkim",
  "IN-TN": "tamil-nadu",
  "IN-TG": "telangana",
  "IN-TR": "tripura",
  "IN-UP": "uttar-pradesh",
  "IN-UT": "uttarakhand",
  "IN-WB": "west-bengal",
  "IN-AN": "andaman-nicobar",
  "IN-CH": "chandigarh",
  "IN-DH": "dadra-daman-diu",
  "IN-DL": "delhi",
  "IN-JK": "jammu-kashmir",
  "IN-LA": "ladakh",
  "IN-LD": "lakshadweep",
  "IN-PY": "puducherry"
};

const geojson = JSON.parse(fs.readFileSync(path.resolve(inputPath), "utf8"));
const regionRings = new Map();
const allPoints = [];

function outerRings(geometry) {
  if (geometry.type === "Polygon") return [geometry.coordinates[0]];
  if (geometry.type === "MultiPolygon") return geometry.coordinates.map((polygon) => polygon[0]);
  throw new Error(`Unsupported geometry type: ${geometry.type}`);
}

for (const feature of geojson.features || []) {
  const iso = feature.properties?.shapeISO;
  const id = ISO_TO_ID[iso];
  if (!id) throw new Error(`Unknown or missing shapeISO: ${iso}`);
  const rings = outerRings(feature.geometry).filter((ring) => ring.length >= 4);
  regionRings.set(id, rings);
  for (const ring of rings) allPoints.push(...ring);
}

const expectedIds = Object.values(ISO_TO_ID);
const missing = expectedIds.filter((id) => !regionRings.has(id));
if (missing.length || regionRings.size !== expectedIds.length) {
  throw new Error(`Expected 36 regions. Missing: ${missing.join(", ") || "none"}`);
}

const xs = allPoints.map((point) => point[0]);
const ys = allPoints.map((point) => point[1]);
const bounds = {
  minX: Math.min(...xs),
  minY: Math.min(...ys),
  maxX: Math.max(...xs),
  maxY: Math.max(...ys)
};
const padding = 0.018;
const scale = 1 - padding * 2;
const round = (value) => Number(value.toFixed(5));

function normalizeRing(ring) {
  const normalized = ring.map(([x, y]) => [
    round(padding + ((x - bounds.minX) / (bounds.maxX - bounds.minX)) * scale),
    round(padding + ((bounds.maxY - y) / (bounds.maxY - bounds.minY)) * scale)
  ]);
  const deduped = normalized.filter((point, index) => {
    const previous = normalized[index - 1];
    return !previous || point[0] !== previous[0] || point[1] !== previous[1];
  });
  if (deduped.length > 3) {
    const first = deduped[0];
    const last = deduped[deduped.length - 1];
    if (first[0] === last[0] && first[1] === last[1]) deduped.pop();
  }
  return deduped;
}

const shapes = {};
for (const id of expectedIds) {
  shapes[id] = regionRings.get(id).map(normalizeRing).filter((ring) => ring.length >= 3);
}

const output = {
  schemaVersion: 2,
  projection: "normalized-geoboundaries-india-adm1-v1",
  regionCount: expectedIds.length,
  sourceName: "geoBoundaries India ADM1",
  sourceRevision: "geoBoundaries release commit 9469f09; simplified for mobile rendering",
  sourceUrl: "https://www.geoboundaries.org/api/current/gbOpen/IND/ADM1/",
  sourceData: "DataMeet India community, Election Commission of India",
  sourceNote: "Gameplay-simplified state/UT geometry. Not intended for navigation, legal boundary decisions, or survey use.",
  officialReference: "https://surveyofindia.gov.in/pages/outline-maps-of-india",
  license: "Creative Commons Attribution 2.5 India (CC BY 2.5 IN)",
  licenseUrl: "https://creativecommons.org/licenses/by/2.5/in/",
  attribution: "geoBoundaries; DataMeet India community; Election Commission of India",
  coordinateBounds: Object.fromEntries(Object.entries(bounds).map(([key, value]) => [key, round(value)])),
  shapes
};

fs.mkdirSync(path.dirname(path.resolve(outputPath)), { recursive: true });
fs.writeFileSync(path.resolve(outputPath), JSON.stringify(output, null, 2) + "\n", "utf8");
console.log(`Wrote ${expectedIds.length} regions to ${path.resolve(outputPath)}`);
