# Chapter 10: Janata Ka Mandate

## Purpose

Chapter 10 turns Azad's earned MLA nomination into a complete, lawful Vidhansabha campaign. The player signs a conduct pledge, costs the manifesto, runs rural and urban public meetings, trains volunteers, corrects misinformation, debates a fictional opponent, chooses a final campaign strategy, audits every expense, verifies accessible polling, keeps police neutral, and receives a computed election result.

## Election State

- `SUPPORT`: constituency-wide voter confidence earned through issue work
- `RULES`: campaign disclosure, consent, spending, and conduct compliance
- `OPS`: volunteer, accessibility, complaint, and polling-day readiness
- `VOTE`: persisted assembly vote share
- `WON`: persisted Vidhansabha result, gated by a valid nomination
- Save v10 stores Chapter 10 progress, campaign strategy, and assembly result

## Vote Calculation

The result combines assembly Reach, Coalition Unity, Readiness, Constituency Support, Campaign Compliance, Election Operations, Reputation, Proof, and an Opposition Pressure penalty. A valid Chapter 9 nomination and at least 50 percent vote share are both required. Neither strategy receives a scripted victory.

## Playable Work

- Independent lawful-candidacy pledge
- Five-promise manifesto with costs, funding sources, and dates
- Rural issue padyatra with written follow-up dockets
- Women-and-youth-led urban town hall with Shanti
- Volunteer consent, spending, and anti-hate training
- Source-backed correction of an edited fictional rumour
- Equal-time public assembly debate
- Issue yatra versus influencer-led media convoy decision
- Donation, print, vehicle, and digital-expense audit
- Accessible, neutral polling-zone verification
- Neutral police perimeter and authorized complaint protocol with Samrat
- Computed independent counting result

## Safety Rules

- All parties, candidates, opponents, officials, claims, and constituency events are fictional
- No real-party symbol, real-leader endorsement, protected-identity targeting, cash, gift, or fear appeal
- Police remain outside campaign activity and handle only lawful perimeter safety
- Polling areas have no party branding or candidate volunteers inside the neutral zone
- Result language does not make claims about a real election or voting system

## Verified Outcomes

- Issue Yatra: Trust 100, Funds Rs 50, Reputation 100, Proof 79, Power 40, Team 99, Pressure 56, Reach 88, Unity 93, Readiness 99, Support 58, Rules 92, Operations 69, Vote 58 percent, seat won
- Media Convoy: Trust 100, Funds Rs 0, Reputation 95, Proof 74, Power 43, Team 96, Pressure 65, Reach 93, Unity 83, Readiness 90, Support 66, Rules 75, Operations 55, Vote 55 percent, seat won
- Both branches preserve the passed 85-point governance review and valid 91-point nomination while persisting strategies 1 and 2

## Verification

- Twelve ordered objectives and both campaign branches pass exact Windows runtime assertions
- Ten-card menu, six-row adaptive HUD, decision modal, candidate outfit, counting dialogue, and chapter actions were visually checked at 844x390
- Android package `com.skenterprises.netaji.prototype` is version `0.11.0` (`versionCode 11`), min SDK 26, target SDK 36, and ARMv7
- APK size: 62,531,443 bytes
- APK SHA-256: `6FC2020335F9BE5FD0135A777848E02FCCA4A62FDCF10B18E4CE2DFC7686F429`
- APK Signature Scheme v2 verification passed with the Android debug certificate
- Physical-device install and touch-feel testing remain pending because no Android device is connected
