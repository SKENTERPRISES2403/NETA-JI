# Chapter 13: Pradesh Ki Dastak

## Purpose

Chapter 13 converts Azad's verified district organization into a lawful eight-seat fictional state campaign. The player audits expansion eligibility, builds a comparable issue atlas, vets a candidate slate, raises receipt-backed small donations, trains polling volunteers, runs an equal-time debate, chooses a campaign strategy, protects intimidation evidence, rehearses polling logistics, holds a public convention, and faces a computed multi-seat result.

## State Campaign

- `REACH`: public issue desks and campaign contact across eight fictional seats
- `INTEGRITY`: candidate disclosures, public interviews, conflicts, and slate standards
- `OPS`: polling training, accessibility, routes, incident logs, and lawful operations
- `SCORE`: computed state expansion performance
- `SEATS`: deterministic seats won out of eight, derived from the score
- Save v13 stores Chapter 13 progress, strategy, score, seats, and foothold result

## Result Calculation

The state score combines Campaign Reach, Candidate Slate Integrity, Election Operations, the earlier District Expansion score, Reputation, Proof, and an Opposition Pressure penalty. Seats won are derived from that score rather than scripted. A state foothold requires a ready district network, Integrity and Operations of at least 60, a score of at least 72, and at least five of eight seats.

## Playable Work

- Independent district-mandate and expansion-eligibility audit
- Comparable public-issue atlas for eight fictional seats
- Disclosure-based candidate-slate vetting
- Receipt-backed small-donor drive with donor cap and conflict register
- Consent, accessibility, shift, safety, and incident-log volunteer training
- Shanti-led equal-time public candidate debate
- Phased field campaign versus all-seat media blitz decision
- Evidence-safe intimidation report through a neutral public-safety channel
- Eight-seat polling logistics and accessibility dry-run
- Evidence-led public convention with opposition questions
- Candidate, funding, consent, correction, and anti-hate compliance review
- Computed independent campaign score, seats won, and state foothold

## Safety Rules

- All seats, candidates, results, threats, claims, and organizations are fictional
- Real parties, leaders, officeholders, constituencies, and voting claims are not used
- Candidate selection excludes caste, religion, family identity, rumours, and other protected traits
- Constable Samrat remains neutral and supports only public-safety evidence procedure
- Party funds, public funds, volunteer consent, complaints, and corrections remain auditable

## Verified Outcomes

- Phased Field Campaign: Trust 100, Funds Rs 40, Reputation 100, Proof 100, Power 52, Team 165, Pressure 99, Reach 80, Integrity 89, Operations 98, Score 82, Seats 6/8, foothold secured
- All-Seat Media Blitz: Trust 100, Funds Rs 130, Reputation 90, Proof 99, Power 56, Team 161, Pressure 100, Reach 92, Integrity 67, Operations 74, Score 73, Seats 5/8, foothold secured
- Both branches preserve District Expansion 89 and MLA Performance 78 while persisting state strategies 1 and 2

## Verification

- Twelve ordered objectives and both campaign branches pass exact Windows runtime assertions
- Thirteen-card menu, dynamic card typography, state HUD, decision modal, result dialogue, and chapter actions were visually checked at 844x390
- Android package `com.skenterprises.netaji.prototype` is version `0.14.0` (`versionCode 14`), min SDK 26, target SDK 36, and ARMv7
- APK size: 62,658,331 bytes
- APK SHA-256: `25743ECAB509977DF335532C506134077D214A9CA473BBFF14D9B3DCDB5D0550`
- APK Signature Scheme v2 verification passed with the Android debug certificate
- Physical-device install and touch-feel testing remain pending because no Android device is connected
