const fs = require("fs");
const path = require("path");
const zlib = require("zlib");

const root = path.resolve(__dirname, "..");
const outDir = path.join(root, "assets");
const SCALE = 2;

const colors = {
  ink: [21, 21, 21, 255],
  orange: [240, 93, 35, 255],
  orangeDark: [198, 61, 25, 255],
  paper: [255, 253, 247, 255],
  teal: [19, 169, 154, 255],
  yellow: [244, 197, 66, 255],
  skin: [240, 179, 126, 255],
  white: [255, 255, 255, 255],
  transparent: [0, 0, 0, 0]
};

function crc32(buffer) {
  let crc = -1;
  for (let i = 0; i < buffer.length; i += 1) {
    crc ^= buffer[i];
    for (let j = 0; j < 8; j += 1) {
      crc = (crc >>> 1) ^ (0xedb88320 & -(crc & 1));
    }
  }
  return (crc ^ -1) >>> 0;
}

function chunk(type, data) {
  const typeBuffer = Buffer.from(type, "ascii");
  const length = Buffer.alloc(4);
  const crc = Buffer.alloc(4);
  length.writeUInt32BE(data.length, 0);
  crc.writeUInt32BE(crc32(Buffer.concat([typeBuffer, data])), 0);
  return Buffer.concat([length, typeBuffer, data, crc]);
}

function pngEncode(width, height, rgba) {
  const header = Buffer.from([137, 80, 78, 71, 13, 10, 26, 10]);
  const ihdr = Buffer.alloc(13);
  ihdr.writeUInt32BE(width, 0);
  ihdr.writeUInt32BE(height, 4);
  ihdr[8] = 8;
  ihdr[9] = 6;

  const stride = width * 4;
  const raw = Buffer.alloc((stride + 1) * height);
  for (let y = 0; y < height; y += 1) {
    const rowStart = y * (stride + 1);
    raw[rowStart] = 0;
    rgba.copy(raw, rowStart + 1, y * stride, y * stride + stride);
  }

  return Buffer.concat([
    header,
    chunk("IHDR", ihdr),
    chunk("IDAT", zlib.deflateSync(raw, { level: 9 })),
    chunk("IEND", Buffer.alloc(0))
  ]);
}

function makePainter(size, maskable) {
  const hi = size * SCALE;
  const buffer = Buffer.alloc(hi * hi * 4);
  const k = hi / 512;
  const logoScale = maskable ? 0.82 : 1;
  const offset = (512 - 512 * logoScale) / 2;
  const map = (value) => offset + value * logoScale;
  const mapSize = (value) => value * logoScale;

  function setPixel(x, y, color) {
    if (x < 0 || y < 0 || x >= hi || y >= hi) return;
    const index = (Math.floor(y) * hi + Math.floor(x)) * 4;
    buffer[index] = color[0];
    buffer[index + 1] = color[1];
    buffer[index + 2] = color[2];
    buffer[index + 3] = color[3];
  }

  function fillRect(x, y, width, height, color, transform = true) {
    const left = (transform ? map(x) : x) * k;
    const top = (transform ? map(y) : y) * k;
    const right = left + (transform ? mapSize(width) : width) * k;
    const bottom = top + (transform ? mapSize(height) : height) * k;
    for (let py = Math.max(0, Math.floor(top)); py < Math.min(hi, Math.ceil(bottom)); py += 1) {
      for (let px = Math.max(0, Math.floor(left)); px < Math.min(hi, Math.ceil(right)); px += 1) {
        setPixel(px, py, color);
      }
    }
  }

  function fillCircle(cx, cy, radius, color) {
    const centerX = map(cx) * k;
    const centerY = map(cy) * k;
    const scaledRadius = mapSize(radius) * k;
    const left = Math.floor(centerX - scaledRadius);
    const right = Math.ceil(centerX + scaledRadius);
    const top = Math.floor(centerY - scaledRadius);
    const bottom = Math.ceil(centerY + scaledRadius);
    for (let py = Math.max(0, top); py < Math.min(hi, bottom); py += 1) {
      for (let px = Math.max(0, left); px < Math.min(hi, right); px += 1) {
        const dx = px + 0.5 - centerX;
        const dy = py + 0.5 - centerY;
        if (dx * dx + dy * dy <= scaledRadius * scaledRadius) setPixel(px, py, color);
      }
    }
  }

  function fillRoundedRect(x, y, width, height, radius, color, transform = true) {
    const wx = transform ? map(x) : x;
    const wy = transform ? map(y) : y;
    const ww = transform ? mapSize(width) : width;
    const wh = transform ? mapSize(height) : height;
    const rr = (transform ? mapSize(radius) : radius);
    const left = Math.floor(wx * k);
    const top = Math.floor(wy * k);
    const right = Math.ceil((wx + ww) * k);
    const bottom = Math.ceil((wy + wh) * k);
    for (let py = Math.max(0, top); py < Math.min(hi, bottom); py += 1) {
      for (let px = Math.max(0, left); px < Math.min(hi, right); px += 1) {
        const ux = (px + 0.5) / k;
        const uy = (py + 0.5) / k;
        const cx = Math.max(wx + rr, Math.min(ux, wx + ww - rr));
        const cy = Math.max(wy + rr, Math.min(uy, wy + wh - rr));
        const dx = ux - cx;
        const dy = uy - cy;
        if (dx * dx + dy * dy <= rr * rr) setPixel(px, py, color);
      }
    }
  }

  function lineDistance(px, py, x1, y1, x2, y2) {
    const dx = x2 - x1;
    const dy = y2 - y1;
    const lengthSq = dx * dx + dy * dy;
    if (!lengthSq) return Math.hypot(px - x1, py - y1);
    const t = Math.max(0, Math.min(1, ((px - x1) * dx + (py - y1) * dy) / lengthSq));
    return Math.hypot(px - (x1 + dx * t), py - (y1 + dy * t));
  }

  function strokeLine(x1, y1, x2, y2, width, color) {
    const ax = map(x1) * k;
    const ay = map(y1) * k;
    const bx = map(x2) * k;
    const by = map(y2) * k;
    const radius = mapSize(width) * k * 0.5;
    const left = Math.floor(Math.min(ax, bx) - radius - 1);
    const right = Math.ceil(Math.max(ax, bx) + radius + 1);
    const top = Math.floor(Math.min(ay, by) - radius - 1);
    const bottom = Math.ceil(Math.max(ay, by) + radius + 1);
    for (let py = Math.max(0, top); py < Math.min(hi, bottom); py += 1) {
      for (let px = Math.max(0, left); px < Math.min(hi, right); px += 1) {
        if (lineDistance(px + 0.5, py + 0.5, ax, ay, bx, by) <= radius) setPixel(px, py, color);
      }
    }
  }

  function fillPolygon(points, color) {
    const mapped = points.map(([x, y]) => [map(x) * k, map(y) * k]);
    const xs = mapped.map(([x]) => x);
    const ys = mapped.map(([, y]) => y);
    const left = Math.floor(Math.min(...xs));
    const right = Math.ceil(Math.max(...xs));
    const top = Math.floor(Math.min(...ys));
    const bottom = Math.ceil(Math.max(...ys));
    for (let py = Math.max(0, top); py < Math.min(hi, bottom); py += 1) {
      for (let px = Math.max(0, left); px < Math.min(hi, right); px += 1) {
        let inside = false;
        for (let i = 0, j = mapped.length - 1; i < mapped.length; j = i, i += 1) {
          const [xi, yi] = mapped[i];
          const [xj, yj] = mapped[j];
          if ((yi > py) !== (yj > py) && px < ((xj - xi) * (py - yi)) / (yj - yi) + xi) {
            inside = !inside;
          }
        }
        if (inside) setPixel(px, py, color);
      }
    }
  }

  function strokePolygon(points, width, color) {
    for (let i = 0; i < points.length; i += 1) {
      const current = points[i];
      const next = points[(i + 1) % points.length];
      strokeLine(current[0], current[1], next[0], next[1], width, color);
    }
  }

  function downsample() {
    const out = Buffer.alloc(size * size * 4);
    for (let y = 0; y < size; y += 1) {
      for (let x = 0; x < size; x += 1) {
        const totals = [0, 0, 0, 0];
        for (let sy = 0; sy < SCALE; sy += 1) {
          for (let sx = 0; sx < SCALE; sx += 1) {
            const index = ((y * SCALE + sy) * hi + (x * SCALE + sx)) * 4;
            totals[0] += buffer[index];
            totals[1] += buffer[index + 1];
            totals[2] += buffer[index + 2];
            totals[3] += buffer[index + 3];
          }
        }
        const outIndex = (y * size + x) * 4;
        out[outIndex] = Math.round(totals[0] / (SCALE * SCALE));
        out[outIndex + 1] = Math.round(totals[1] / (SCALE * SCALE));
        out[outIndex + 2] = Math.round(totals[2] / (SCALE * SCALE));
        out[outIndex + 3] = Math.round(totals[3] / (SCALE * SCALE));
      }
    }
    return out;
  }

  return {
    buffer,
    fillRect,
    fillCircle,
    fillRoundedRect,
    fillPolygon,
    strokePolygon,
    strokeLine,
    downsample
  };
}

function drawIcon(size, maskable = false) {
  const p = makePainter(size, maskable);
  p.fillRect(0, 0, 512, 512, colors.orange, false);
  p.fillCircle(440, 82, 46, [255, 209, 102, 80]);
  p.fillCircle(70, 432, 52, [19, 169, 154, 70]);
  p.fillRoundedRect(44, 56, 424, 400, 78, colors.orangeDark);
  p.fillRoundedRect(32, 44, 424, 400, 78, colors.orange);

  p.fillRoundedRect(80, 104, 300, 92, 12, colors.ink);
  p.fillRoundedRect(94, 118, 272, 64, 8, colors.paper);
  p.fillRect(216, 138, 94, 18, colors.ink);
  p.fillRect(292, 138, 20, 80, colors.ink);
  p.fillRect(240, 198, 72, 18, colors.ink);
  p.fillRect(240, 176, 20, 40, colors.ink);
  p.fillRect(330, 138, 70, 18, colors.ink);
  p.fillRect(355, 138, 20, 80, colors.ink);
  p.fillRect(330, 200, 70, 18, colors.ink);

  p.strokeLine(178, 116, 178, 408, 24, colors.ink);
  p.strokeLine(178, 116, 178, 408, 10, colors.paper);
  const flag = [
    [188, 210],
    [374, 250],
    [188, 304]
  ];
  p.strokePolygon(flag, 18, colors.ink);
  p.fillPolygon(flag, colors.teal);
  p.strokeLine(228, 240, 334, 262, 8, [255, 253, 247, 220]);

  p.fillCircle(266, 352, 66, colors.ink);
  p.fillCircle(266, 352, 48, colors.skin);
  p.fillCircle(248, 342, 5, colors.ink);
  p.fillCircle(284, 342, 5, colors.ink);
  p.strokeLine(246, 374, 286, 374, 10, colors.ink);

  p.fillRoundedRect(174, 420, 184, 66, 28, colors.ink);
  p.fillRoundedRect(194, 430, 144, 42, 20, colors.paper);
  p.strokeLine(238, 404, 264, 458, 16, colors.ink);
  p.strokeLine(294, 404, 266, 458, 16, colors.ink);
  p.strokeLine(238, 404, 264, 458, 7, colors.white);
  p.strokeLine(294, 404, 266, 458, 7, colors.white);

  for (const [x, y, color] of [
    [92, 258, colors.yellow],
    [430, 268, colors.paper],
    [82, 336, colors.teal],
    [420, 366, colors.yellow]
  ]) {
    p.fillCircle(x, y, 8, colors.ink);
    p.fillCircle(x, y, 4, color);
  }

  return pngEncode(size, size, p.downsample());
}

fs.mkdirSync(outDir, { recursive: true });
for (const icon of [
  { file: "icon-192.png", size: 192, maskable: false },
  { file: "icon-512.png", size: 512, maskable: false },
  { file: "icon-maskable-512.png", size: 512, maskable: true }
]) {
  fs.writeFileSync(path.join(outDir, icon.file), drawIcon(icon.size, icon.maskable));
  console.log(`Generated assets/${icon.file}`);
}
