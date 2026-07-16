# NETA JI Map Data

The India hub reads `data/india-map-shapes.json`. The same geometry supplies the home-map silhouette and the enlarged single-region gameplay arena.

## Current Source

- Dataset: geoBoundaries India ADM1 (`IND-ADM1-1811400`)
- Upstream source named by geoBoundaries: DataMeet India community and Election Commission of India
- License: Creative Commons Attribution 2.5 India (CC BY 2.5 IN)
- Pinned release: geoBoundaries commit `9469f09`
- Runtime projection: `normalized-geoboundaries-india-adm1-v1`
- Required attribution: `geoBoundaries; DataMeet India community; Election Commission of India`

The geometry is simplified for a mobile game. It must not be presented as a legal, navigation, or survey map.

Survey of India outline and state maps are used only as visual references. Their current copyright policy requires written permission before reproducing website material, so Survey of India data is not bundled in this sellable demo.

## Rebuilding

Download the pinned simplified GeoJSON from:

```text
https://github.com/wmgeolab/geoBoundaries/raw/9469f09/releaseData/gbOpen/IND/ADM1/geoBoundaries-IND-ADM1_simplified.geojson
```

Use Mapshaper to simplify it for phone rendering, then convert it to the game schema:

```powershell
mapshaper geoBoundaries-IND-ADM1_simplified.geojson -simplify weighted 1.25% keep-shapes -o geoboundaries-ind-adm1-game.geojson format=geojson precision=0.0001
node tools/convert-map-geojson.cjs geoboundaries-ind-adm1-game.geojson data/india-map-shapes.json
```

## QA

Run:

```powershell
npm.cmd run logic:qa
npm.cmd run map:qa
npm.cmd run smoke:map
```

`map:qa` verifies all 28 states and 8 union territories, valid polygon coordinates, polygon hit targets, source metadata, and attribution. `logic:qa` also proves that a small closed route captures only its enclosed cells instead of filling the whole state.
