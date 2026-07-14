# Chapter 15: Pradesh Ka Janadesh

## Purpose

Chapter 15 turns Azad's fictional state-leadership mandate into a forty-seat general election. The player verifies eligibility, publishes a costed manifesto, checks every candidate and donation, listens to voters, chooses a campaign strategy, completes debate and polling preparation, and submits the full count to an independent election panel.

## Election State

- `SUPPORT`: statewide issue reach, volunteer contact, and voter confidence
- `RULES`: candidate disclosure, clean funding, equal access, and campaign compliance
- `OPS`: booth coverage, safety, training, evidence logs, and polling readiness
- `VOTE`: computed statewide vote share
- `SEATS`: computed seats won out of forty fictional constituencies
- Save v15 stores Chapter 15 progress, election strategy, result, and CM mandate

## Mandate Calculation

Vote share combines statewide Support, Rules, Operations, the Chapter 14 leadership score, Reputation, Proof, and an Opposition Pressure penalty. Seats are derived from vote share rather than granted by an objective. Azad becomes CM-designate only with prior state-leader selection, Rules and Operations of at least 60, at least 50% statewide vote, and a 21-seat majority.

## Playable Work

- Independent state-election and CM-candidate eligibility audit
- Costed manifesto with measurable education, health, safety, and jobs commitments
- Disclosure review for all forty fictional candidates
- Clean-funding ledger with donor limits and public expense records
- Shanti-led women, youth, caregiver, and accessibility forum
- Issue-led statewide jan-samvad yatra
- Local issue campaign versus personality mega-rally decision
- Evidence-backed public debate and live fact-check desk
- Polling-agent training, route planning, and incident escalation
- Samrat-led neutral public-safety and queue-management briefing
- Final compliance room and independent forty-seat result panel

## Safety Rules

- Every seat, candidate, opponent, claim, institution, result, and campaign incident is fictional
- No real party, leader, government, constituency, caste, religion, or identity-targeted message is used
- Police and administration remain neutral; Samrat handles lawful public-safety procedure only
- Donations, candidate records, expenses, corrections, complaints, and booth operations remain auditable
- CM status is an earned democratic result, never a capture or automatic promotion

## Verified Outcomes

- Issue-Led Local Campaign: Trust 100, Funds Rs 30, Reputation 100, Proof 100, Power 59, Team 218, Pressure 88, Support 78, Rules 100, Operations 90, Vote 56%, Seats 27/40, CM-designate
- Personality Mega Rallies: Trust 100, Funds Rs 150, Reputation 90, Proof 98, Power 64, Team 213, Pressure 96, Support 88, Rules 75, Operations 68, Vote 52%, Seats 23/40, CM-designate
- Both outcomes preserve the earlier six-seat foothold and state-leadership selection while persisting strategies 1 and 2

## Verification

- Twelve ordered objectives and both campaign branches pass exact Windows runtime assertions
- Fifteen-card menu, adaptive six-row HUD, campaign decision, election campus, result panel, and completion flow were visually checked at 844x390
- Android package `com.skenterprises.netaji.prototype` is version `0.16.0` (`versionCode 16`), min SDK 26, target SDK 36, and ARMv7
- APK size: 62,766,823 bytes
- APK SHA-256: `6BD5F69DFD04E8D1ECFA6D3088B2D4FBDE28B7AC1AFD58B1D78621AF2677B818`
- APK Signature Scheme v2 verification passed with the Android debug certificate
- Physical-device install and touch-feel testing remain pending until an Android phone is connected
