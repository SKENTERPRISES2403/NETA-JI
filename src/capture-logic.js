const CARDINAL_NEIGHBORS = [
  [-1, 0],
  [1, 0],
  [0, -1],
  [0, 1]
];

export function findEnclosedCells({ cols, rows, regionMask, owner, ownerId }) {
  const cellCount = cols * rows;
  if (!Number.isInteger(cols) || !Number.isInteger(rows) || cols < 1 || rows < 1) {
    throw new TypeError("Capture grid dimensions must be positive integers.");
  }
  if (!regionMask || !owner || regionMask.length !== cellCount || owner.length !== cellCount) {
    throw new TypeError("Capture grid arrays must match cols * rows.");
  }

  const visited = new Uint8Array(cellCount);
  const queue = new Int32Array(cellCount);
  let queueStart = 0;
  let queueEnd = 0;

  const push = (x, y) => {
    if (x < 0 || x >= cols || y < 0 || y >= rows) return;
    const idx = y * cols + x;
    if (!regionMask[idx] || visited[idx] || owner[idx] === ownerId) return;
    visited[idx] = 1;
    queue[queueEnd] = idx;
    queueEnd += 1;
  };

  // The playable state usually sits inside the rectangular grid, so the flood
  // must start from the state's own edge instead of the canvas edge.
  for (let y = 0; y < rows; y += 1) {
    for (let x = 0; x < cols; x += 1) {
      const idx = y * cols + x;
      if (!regionMask[idx] || owner[idx] === ownerId) continue;
      const touchesRegionEdge = CARDINAL_NEIGHBORS.some(([dx, dy]) => {
        const nx = x + dx;
        const ny = y + dy;
        return nx < 0 || nx >= cols || ny < 0 || ny >= rows || !regionMask[ny * cols + nx];
      });
      if (touchesRegionEdge) push(x, y);
    }
  }

  while (queueStart < queueEnd) {
    const idx = queue[queueStart];
    queueStart += 1;
    const x = idx % cols;
    const y = Math.floor(idx / cols);
    for (const [dx, dy] of CARDINAL_NEIGHBORS) push(x + dx, y + dy);
  }

  const enclosed = [];
  for (let idx = 0; idx < cellCount; idx += 1) {
    if (regionMask[idx] && !visited[idx] && owner[idx] !== ownerId) enclosed.push(idx);
  }
  return enclosed;
}
