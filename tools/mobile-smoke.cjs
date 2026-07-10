const fs = require("fs");
const http = require("http");
const https = require("https");
const os = require("os");
const path = require("path");
const { spawn, spawnSync } = require("child_process");

const root = path.resolve(__dirname, "..");
const targetArg = process.argv[2] || "";
const isPublicTarget = /^https?:\/\//.test(targetArg);
const port = Number(process.env.PORT || 5177);
const localPath = targetArg && !isPublicTarget ? targetArg : "/?demo=1&smoke=local";
const baseUrl = isPublicTarget ? targetArg : `http://127.0.0.1:${port}${localPath}`;
const outDir = path.join(root, "dist", "smoke");

function findChrome() {
  const candidates = [
    process.env.CHROME_PATH,
    "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
    "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe",
    "C:\\Program Files\\Microsoft\\Edge\\Application\\msedge.exe",
    "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
    "/usr/bin/google-chrome",
    "/usr/bin/chromium",
    "/usr/bin/chromium-browser"
  ].filter(Boolean);
  return candidates.find((candidate) => fs.existsSync(candidate));
}

function requestText(url) {
  const client = url.startsWith("https:") ? https : http;
  return new Promise((resolve, reject) => {
    const req = client.get(url, (res) => {
      const chunks = [];
      res.on("data", (chunk) => chunks.push(chunk));
      res.on("end", () => {
        const body = Buffer.concat(chunks).toString("utf8");
        if (res.statusCode < 200 || res.statusCode >= 400) {
          reject(new Error(`${url} returned ${res.statusCode}`));
          return;
        }
        resolve(body);
      });
    });
    req.setTimeout(15000, () => {
      req.destroy(new Error(`${url} timed out`));
    });
    req.on("error", reject);
  });
}

async function waitFor(url) {
  const started = Date.now();
  let lastError;
  while (Date.now() - started < 20000) {
    try {
      await requestText(url);
      return;
    } catch (error) {
      lastError = error;
      await new Promise((resolve) => setTimeout(resolve, 500));
    }
  }
  throw lastError || new Error(`${url} did not become ready`);
}

function makeUrl(route) {
  return new URL(route, baseUrl).toString();
}

function assertContains(name, text, expected) {
  if (!text.includes(expected)) {
    throw new Error(`${name} is missing ${expected}`);
  }
}

async function main() {
  fs.mkdirSync(outDir, { recursive: true });
  let server;
  if (!isPublicTarget) {
    server = spawn(process.execPath, ["dev-server.cjs", String(port), "127.0.0.1"], {
      cwd: root,
      stdio: "ignore",
      windowsHide: true
    });
  }

  try {
    await waitFor(baseUrl);
    const [page, manifest, serviceWorker, mainJs] = await Promise.all([
      requestText(baseUrl),
      requestText(makeUrl("/manifest.webmanifest")),
      requestText(makeUrl("/sw.js")),
      requestText(makeUrl("/src/main.js"))
    ]);

    assertContains("index.html", page, "NETA JI");
    assertContains("manifest", manifest, "icon-512.png");
    assertContains("service worker", serviceWorker, "CACHE_NAME");
    assertContains("main.js", mainJs, "drawMemeWaveScene");
    assertContains("main.js", mainJs, "isOnboardingVisible");

    const chrome = findChrome();
    if (!chrome) throw new Error("Chrome or Edge was not found. Set CHROME_PATH to run screenshots.");

    const shots = [
      { name: "mobile", size: "390,844" },
      { name: "desktop", size: "1280,720" }
    ];
    const screenshots = [];
    for (const shot of shots) {
      const profile = path.join(os.tmpdir(), `neta-ji-${shot.name}-${Date.now()}`);
      const file = path.join(outDir, `${shot.name}.png`);
      const result = spawnSync(
        chrome,
        [
          "--headless=new",
          "--disable-gpu",
          "--no-first-run",
          "--disable-dev-shm-usage",
          "--virtual-time-budget=6000",
          `--user-data-dir=${profile}`,
          `--window-size=${shot.size}`,
          `--screenshot=${file}`,
          baseUrl
        ],
        { stdio: "inherit", windowsHide: true }
      );
      if (result.status !== 0) throw new Error(`${shot.name} screenshot failed`);
      const bytes = fs.statSync(file).size;
      if (bytes < 10000) throw new Error(`${shot.name} screenshot looks too small: ${bytes} bytes`);
      screenshots.push({ ...shot, file, bytes });
      fs.rmSync(profile, { recursive: true, force: true });
    }

    const report = {
      url: baseUrl,
      checkedAt: new Date().toISOString(),
      screenshots
    };
    fs.writeFileSync(path.join(outDir, "report.json"), JSON.stringify(report, null, 2));
    console.log(JSON.stringify(report, null, 2));
  } finally {
    if (server && !server.killed) server.kill();
  }
}

main().catch((error) => {
  console.error(error.message);
  process.exitCode = 1;
});
