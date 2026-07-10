const fs = require("fs");
const path = require("path");

const root = __dirname;
const outDir = path.join(root, "dist");
const copyItems = [
  "index.html",
  "styles.css",
  "sw.js",
  "manifest.webmanifest",
  "src",
  "data",
  "assets",
  ".openai"
];

function copyRecursive(source, target) {
  const stats = fs.statSync(source);
  if (stats.isDirectory()) {
    fs.mkdirSync(target, { recursive: true });
    for (const item of fs.readdirSync(source)) {
      copyRecursive(path.join(source, item), path.join(target, item));
    }
    return;
  }
  fs.mkdirSync(path.dirname(target), { recursive: true });
  fs.copyFileSync(source, target);
}

fs.rmSync(outDir, { recursive: true, force: true });
fs.mkdirSync(outDir, { recursive: true });

for (const item of copyItems) {
  const source = path.join(root, item);
  if (fs.existsSync(source)) {
    copyRecursive(source, path.join(outDir, item));
  }
}

console.log(`Built NETA JI static PWA to ${outDir}`);
