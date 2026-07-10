const fs = require("fs");
const path = require("path");
const vm = require("vm");

const root = path.resolve(__dirname, "..");
const source = fs.readFileSync(path.join(root, "src", "main.js"), "utf8");

function extractConst(name, nextName) {
  const pattern = new RegExp(`const ${name} = ([\\s\\S]*?);\\n\\nconst ${nextName}`);
  const match = source.match(pattern);
  if (!match) throw new Error(`Could not extract ${name}`);
  return vm.runInNewContext(`(${match[1]})`, Object.create(null), { timeout: 1000 });
}

const regions = extractConst("REGIONS", "REGION_POSITIONS");
const shapes = extractConst("REGION_MAP_SHAPES", "fallbackData");

function polygonArea(points) {
  let area = 0;
  for (let i = 0; i < points.length; i += 1) {
    const current = points[i];
    const next = points[(i + 1) % points.length];
    area += current[0] * next[1] - next[0] * current[1];
  }
  return Math.abs(area / 2);
}

function polygonCentroid(points) {
  let areaTwice = 0;
  let cx = 0;
  let cy = 0;
  for (let i = 0; i < points.length; i += 1) {
    const current = points[i];
    const next = points[(i + 1) % points.length];
    const cross = current[0] * next[1] - next[0] * current[1];
    areaTwice += cross;
    cx += (current[0] + next[0]) * cross;
    cy += (current[1] + next[1]) * cross;
  }
  if (Math.abs(areaTwice) < 0.0001) {
    return points.reduce((sum, point) => ({ x: sum.x + point[0] / points.length, y: sum.y + point[1] / points.length }), { x: 0, y: 0 });
  }
  return {
    x: cx / (3 * areaTwice),
    y: cy / (3 * areaTwice)
  };
}

function pointInPolygon(point, polygon) {
  let inside = false;
  for (let i = 0, j = polygon.length - 1; i < polygon.length; j = i, i += 1) {
    const xi = polygon[i][0];
    const yi = polygon[i][1];
    const xj = polygon[j][0];
    const yj = polygon[j][1];
    if ((yi > point.y) !== (yj > point.y) && point.x < ((xj - xi) * (point.y - yi)) / (yj - yi) + xi) {
      inside = !inside;
    }
  }
  return inside;
}

function largestPolygon(polygons) {
  return polygons.reduce((best, polygon) => (polygonArea(polygon) > polygonArea(best) ? polygon : best), polygons[0]);
}

function selectAt(point) {
  const hits = [];
  for (const region of regions) {
    const polygons = shapes[region.id] || [];
    const containing = polygons.filter((polygon) => pointInPolygon(point, polygon));
    if (containing.length) {
      hits.push({
        id: region.id,
        area: Math.min(...containing.map(polygonArea))
      });
    }
  }
  hits.sort((a, b) => a.area - b.area);
  return hits[0]?.id || null;
}

const missing = regions.filter((region) => !shapes[region.id]).map((region) => region.id);
const extra = Object.keys(shapes).filter((id) => !regions.some((region) => region.id === id));
const outOfRange = [];
const badPolygons = [];
const centroidMismatches = [];
const perRegion = [];

for (const region of regions) {
  const polygons = shapes[region.id] || [];
  polygons.forEach((polygon, polygonIndex) => {
    if (polygon.length < 3) badPolygons.push(`${region.id}[${polygonIndex}]`);
    polygon.forEach(([x, y], pointIndex) => {
      if (x < 0 || x > 1 || y < 0 || y > 1) outOfRange.push(`${region.id}[${polygonIndex}][${pointIndex}]`);
    });
  });

  if (!polygons.length) continue;
  const largest = largestPolygon(polygons);
  const centroid = polygonCentroid(largest);
  const selected = selectAt(centroid);
  if (selected !== region.id) {
    centroidMismatches.push({ id: region.id, selected });
  }
  perRegion.push({
    id: region.id,
    polygons: polygons.length,
    area: Number(polygons.reduce((sum, polygon) => sum + polygonArea(polygon), 0).toFixed(5)),
    centroid: { x: Number(centroid.x.toFixed(4)), y: Number(centroid.y.toFixed(4)) },
    centroidSelects: selected
  });
}

const report = {
  checkedAt: new Date().toISOString(),
  regionCount: regions.length,
  shapeCount: Object.keys(shapes).length,
  missing,
  extra,
  outOfRange,
  badPolygons,
  centroidMismatches,
  perRegion
};

fs.mkdirSync(path.join(root, "reports"), { recursive: true });
fs.writeFileSync(path.join(root, "reports", "map-qa-report.json"), JSON.stringify(report, null, 2));
console.log(JSON.stringify(report, null, 2));

if (missing.length || extra.length || outOfRange.length || badPolygons.length || centroidMismatches.length) {
  process.exitCode = 1;
}
