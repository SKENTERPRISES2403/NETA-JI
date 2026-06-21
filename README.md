# NETA JI

NETA JI is a mobile-first political comedy capture game inspired by simple territory games. The player creates a fictional party, picks a color and symbol, runs campaign routes, and wins areas by closing loops on the map.

The tone is intentionally fictional, light, and meme-friendly. The game must not use real parties, real leaders, real party symbols, real slogans, caste/religion targeting, or hate content.

## Current Prototype

- Static HTML5 canvas game
- Works on mobile and laptop browsers
- Party name, color, slogan, and symbol customization
- Paper-style campaign route and territory fill
- Fictional AI opponents
- Random comedy campaign events
- Safety filter for user-generated party names
- Meme-style result card with share text

## Run Locally

Use any static server from the repo root:

```powershell
python -m http.server 5173
```

Then open:

```text
http://localhost:5173
```

## Product Direction

Build order:

1. Playable district arena
2. Better capture algorithm and AI
3. Fictional state campaign mode
4. India map using open map data
5. PWA and Android APK
6. Optional multiplayer after the offline version is fun

## Safety Direction

NETA JI should feel like a funny fictional election game, not a real politics simulator. See [docs/safety-policy.md](docs/safety-policy.md).

