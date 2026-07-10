const fs = require("fs");
const path = require("path");

const root = __dirname;
const outDir = path.join(root, "dist");
const clientDir = path.join(outDir, "client");
const clientItems = [
  "index.html",
  "styles.css",
  "sw.js",
  "manifest.webmanifest",
  "src",
  "data",
  "assets"
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

fs.rmSync(outDir, { recursive: true, force: true, maxRetries: 5, retryDelay: 120 });
fs.mkdirSync(clientDir, { recursive: true });

for (const item of clientItems) {
  const source = path.join(root, item);
  if (fs.existsSync(source)) {
    copyRecursive(source, path.join(clientDir, item));
  }
}

copyRecursive(path.join(root, ".openai"), path.join(outDir, ".openai"));

const serverDir = path.join(outDir, "server");
fs.mkdirSync(serverDir, { recursive: true });
fs.writeFileSync(
  path.join(serverDir, "index.js"),
  `export default {
  async fetch(request, env) {
    const url = new URL(request.url);
    const assetRequest = new Request(url, request);
    const response = await env.ASSETS.fetch(assetRequest);
    if (response.status !== 404) return response;

    url.pathname = "/index.html";
    return env.ASSETS.fetch(new Request(url, request));
  }
};
`,
  "utf8"
);

console.log(`Built NETA JI static PWA to ${outDir}`);
