# Chapter 7: Ward Ka Faisla

## Purpose

Chapter 7 turns the party foundation into a playable local election. The player researches issues, canvasses two lanes, trains booth workers, publishes expenses, debates a fictional rival, responds to misinformation, chooses a final campaign strategy, prepares polling, and receives a computed result.

## Election State

- `WARD` support
- `BOOTH` readiness
- `VOTE` persisted computed vote share
- `wardElectionWon` only when vote share is at least 50 percent
- Save v7 stores the chosen campaign strategy

## Vote Calculation

The formula combines ward support, booth readiness, reputation, proof, political power, and opposition pressure. The result is rounded, clamped to 25-75 percent, and saved. The result objective evaluates those saved stats; it never sets victory directly.

## Campaign Safeguards

- No vote buying, gifts, intimidation, hate, or real parties and candidates
- Voter secrecy and lawful voter-list assistance
- Public fictional campaign receipts
- Fictional rival Arvind Rana of Nagar Badlav Dal
- Evidence-led corrections without abuse or doxxing

## Strategy Outcomes

- Ground Campaign: Trust 100, Funds Rs 350, Reputation 90, Proof 42, Power 28, Team 55, Pressure 17, Ward 62, Booth 85, Vote 59 percent
- Mega Rally: Trust 100, Funds Rs 500, Reputation 85, Proof 38, Power 30, Team 52, Pressure 25, Ward 66, Booth 70, Vote 58 percent
- Both outcomes are computed wins and persist campaign strategies 1 and 2 respectively

## Verification

- Twelve objectives, both branches, vote shares, and wins are asserted by automation
- Seven-card menu, campaign HUD, strategy decision, dynamic result, and completion were checked at 844x390
- Android 0.8.0 package, API levels, ARMv7 architecture, hash, and v2 signature were verified
- Physical-device touch testing is pending a connected Android phone
