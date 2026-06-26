# NETA JI Safety Policy

NETA JI must remain a fictional comedy arcade game.

## Allowed

- Fictional party names
- Fictional party symbols
- Fictional AI opponents
- Generic cartoon leaders
- Generic campaign language
- Open map shapes after license review
- Satirical but non-targeted random events

## Blocked

- Real political party names
- Real leader names
- Real party symbols
- Real slogans
- Hate speech or harassment
- Caste or religion targeting
- Violent or separatist names
- Sexual, abusive, or demeaning names
- Current real-world political claims

## UX Wording

Safer wording:

- Win influence
- Run a campaign
- Win booths
- Win district mandate
- Rally boost

Avoid:

- Capture people
- Take over religion or caste groups
- Defeat a real party
- Attack a real leader
- Occupy a real state

## Moderation Notes

The current build uses a strict local blocked-term list for party names and slogans. It normalizes spacing, repeated letters, and common number substitutions so simple obfuscations are blocked. This is not enough for production. Before public release, add:

- Server-side moderation for shared names
- Better multilingual filtering
- Rate-limits for name creation
- Report button
- Review queue for viral/shared cards
- Store-safe disclaimer
