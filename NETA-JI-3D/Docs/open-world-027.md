# Open World Prototype 0.27

## Player Flow

The main menu now has three clear entry paths:

- `NAYA SAFAR` starts the wordless origin prologue and then Chapter 1
- `SEVA JAARI RAKHEIN` resumes saved chapter progress
- `PRAYAGRAJ GHUMEIN` opens the connected free-roam district without requiring a mission

The existing chapter-selection screen still exposes all 24 story chapters for review.

## Wordless Prologue

Seven short shots show family life, ghat service, Sandhya's abduction, the safe rescue, a non-graphic hospital crisis, recovery, and Azad's public-service resolve. The cinematic uses camera movement, staged actors, props, and a generated score. It has no exposition cards and includes a visible skip control.

## Prayagraj Free Roam

The connected stylized district includes:

- Allahapur and Daraganj-inspired homes
- Allahabad University, High Court, Chandrashekhar Azad Park, Sangam Mall, Helpers Hand, and a fictional hospital
- Ganga riverfront, clean ghats, boats, food stalls, market props, trees, lights, roads, and pedestrians
- A top-left live minimap and a full-screen labelled map with a moving player marker
- A driveable red car and blue scooter with enter, accelerate, steer, brake/reverse, and exit actions
- A seated rider visual while driving; the pedestrian character and collision are restored on exit

Keyboard controls use WASD/arrow movement, Shift sprint, E interaction, M map, and the contextual vehicle actions shown in the HUD. Android touch controls use the existing mobile input layer.

## Character Presentation

Azad keeps his shirt, trousers, shoes, and field bag during the social-worker chapters. The political chapter outfit uses a fitted white kurta-pajama, stole, and kolhapuri footwear. The outfit has been checked from front, back, left, and right views so side angles do not expose duplicate accessories or floating cloth pieces.

## Verified Runtime Results

- Menu: start, story, and 24-card chapter views pass at 844x390
- Prologue: all seven shots render and complete without exposition text
- Free roam: walk/map/car/exit/scooter sequence passes; car reaches about 78 km/h and scooter about 54 km/h in the automated route
- Chapter 1: mission completes at Trust 35, Funds Rs 950, Reputation 16
- Chapter 24 safe path: global score 96 and Vishwa Guru outcome earned
- Chapter 24 risky path: global score 87 and Vishwa Guru outcome not earned
- Windows package is a clean non-development build
- Android APK is `0.27.0` (`versionCode 27`), 26,179,312 bytes, Android 8+, target 36, ARM64 plus ARMv7, IL2CPP, and v2-signed
- APK SHA-256: `8C875390AA29DE7B3E7F6F5302D402FD36A935A92A0E85291AFCCBBC0F7B11AA`

## Honest Scope

This is a functional procedural low-poly game prototype, not GTA-level art or a finished commercial open world. A production pass still needs custom rigged characters, authored animation, higher-quality vehicles and architecture, denser traffic/crowds, mission integration inside the free-roam district, final sound design, device profiling, production signing, and store packaging.

The APK currently uses a debug certificate for local and company-demo installation. A private production keystore and Android App Bundle are required before store release. No physical Android device was connected for this build, so touch feel, sustained FPS, thermals, battery use, and device-specific compatibility remain unverified.
