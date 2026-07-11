# NETA JI Map Data

The current India map uses `data/india-map-shapes.json`.

## Current Format

- `projection`: `normalized-cartoon-india-v1`
- Coordinates are normalized to the game map canvas, from `0` to `1`.
- Each state or UT has one or more polygons.
- Small multi-part regions such as Puducherry, Dadra/Daman/Diu, Lakshadweep, and Andaman/Nicobar use multiple polygons.

## QA

Run:

```powershell
npm.cmd run map:qa
npm.cmd run smoke:map
```

`map:qa` checks:

- All 36 regions have polygon data.
- No extra/unknown region IDs exist.
- All points remain inside the normalized canvas.
- Every region centroid selects the same region.

## Boundary Accuracy

This is a stylized game map, not a legal or survey-grade political boundary map. It is based on public India map references and made intentionally cartoonish for gameplay.

For exact official-style boundaries, replace `data/india-map-shapes.json` with licensed GeoJSON/SVG-derived polygons after license review, then rerun `npm.cmd run map:qa` and both smoke tests.
