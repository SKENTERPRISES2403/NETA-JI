# Prototype 1 Design Lock

## Cast

- **Azad**, 31, a Daraganj community social worker and part-time NGO field coordinator
- **Shanti**, 30, B.A. English, home tutor and volunteer social worker
- **Sandhya**, 7, their curious and kind daughter
- **Constable Samrat**, 35, an honest ground-level police ally

## World

The first playable space is a connected Prayagraj/Daraganj-inspired district. It includes a riverfront, ghats, markets, homes, parks, public buildings, roads, and stylized references to Allahabad University, the High Court, Chandrashekhar Azad Park, and Sangam. Real landmarks provide geographic and visual context, while all NGOs, hospitals, police cases, gangs, contractors, and political groups are fictional.

Players can enter the district directly from the main menu and walk around without accepting a mission. A live minimap, full city map, driveable car, and driveable scooter support free exploration. Helpers Hand is also the save-aware story hub: it presents the next incomplete chapter, launches it after confirmation, and receives Azad back after a completed mission. The current world and characters are procedural low-poly prototype art; production-quality models, animation, traffic, and environmental audio remain later work.

## Opening

A short seven-shot cinematic introduces Azad through family life, community service, Sandhya's non-graphic abduction, the rescue, a hospital crisis, family recovery, and a public-service resolve. It uses visual staging and music instead of exposition text and can be skipped.

## First Mission

`Ravivaar Ki Seva: Ghat Se Ghar Tak` introduces Azad through ordinary public service before the origin crisis. Azad speaks with Shanti, clears three litter clusters, reports to a volunteer, meets Sandhya at home, reads Shanti's household ledger, works with Constable Samrat on an elderly resident's pension verification, and submits the file through Helpers Hand.

## Tone And Safety

- 70% hopeful social drama, 30% safe local humour
- Mild non-graphic action only
- No real political parties, leaders, symbols, or allegations
- The Indian national flag is not used as a party symbol
- Sensitive family events begin only after two service chapters and use non-graphic, investigation-focused framing

## Verification Status

- Windows executable smoke-tested at 1280x720 and 844x390
- Mission automation verifies final values: Trust 35, Funds Rs 950, Reputation 16
- Runtime log verifies procedural audio initialization without errors
- Mobile screenshots verify route hints, chapter overlays, and readable world signage
- Menu automation verifies title/story views and the menu-to-game smoke bypass
- Prologue automation renders and visually verifies all seven wordless shots
- Free-roam automation verifies walking, full-map display, car entry/exit, scooter entry/exit, player collision lifecycle, grounded wheels, and measured vehicle travel
- Story-hub automation verifies a fresh Chapter 1 launch, mission completion, persisted progress, return near Helpers Hand, `1/24` HUD state, and Chapter 2 availability
- Outfit automation verifies the political kurta/stole/kolhapuri set from four cardinal views and checks left/right symmetry
- Legacy `missionStep` and save-v2 through save-v23 profiles migrate into twenty-four-chapter save progress
- Chapter 9 automation verifies safe and risky constituency-expansion routes plus computed MLA nomination
- Chapter 10 automation verifies both assembly-campaign routes and the computed seat result
- Chapter 11 automation verifies public-allocation branches and the computed MLA performance review
- Chapter 12 automation verifies candidate-selection branches and the computed district-network review
- Chapter 13 automation verifies multi-seat strategy branches and the computed state-foothold result
- Chapter 14 automation verifies caucus-selection branches and the computed state-leadership review
- Chapter 15 automation verifies statewide campaign branches and the computed forty-seat CM mandate
- Chapter 16 automation verifies governance-control branches and the computed CM hundred-day review
- Chapter 17 automation verifies five-year reform branches and the computed full-term outcome review
- Chapter 18 automation verifies national-federation branches and the computed regional-readiness review
- Chapter 19 automation verifies both first-national-campaign branches, lawful counting, and the planned accountable-opposition result
- Chapter 20 automation verifies both five-year opposition strategies and the computed second-campaign comeback review
- Chapter 21 automation verifies both second-national-campaign strategies, the independent fictional count, and the computed Prime Minister mandate
- Chapter 22 automation verifies both first-100-day national-governance strategies and the computed Prime Minister public review
- Chapter 23 automation verifies both ten-year national-development strategies and the computed four-outcome public review
- Chapter 24 automation verifies cooperation and spectacle routes, the independent four-pillar review, and the earned Vishwa Guru outcome
- Windows and Android demo packages build without Unity's Development Build watermark
- Android version is `0.28.0` (`versionCode 28`), min SDK 26, target SDK 36, sensor-landscape, ARM64 plus ARMv7, and IL2CPP
- APK package metadata, both native ABI libraries, absence of the debuggable manifest flag, and Android v2 signature are verified after the consolidated build
- The current APK uses an Android debug certificate for local/company demo installs; a production keystore and AAB remain required for store submission
- Physical Android install and touch-feel testing still require a connected phone
