# NETA JI Game Design

## One Line

Create a fictional party, run campaign routes, close loops, and turn the arena into your party color while comedy events create meme moments.

## Current Pitch Scope

- India hub with all 28 states and 8 union territories
- Each selected region expands into a large real-silhouette campaign arena
- Single-player against AI parties
- Mobile and laptop controls
- Custom party name, color, slogan, and symbol
- Campaign trail capture loop
- Poster and banner style captured areas
- Namaste-style leader figure after mandate
- Shareable text result

## Core Loop

1. Start inside owned campaign area.
2. Move outside and leave a campaign route.
3. Return to owned area to close the loop.
4. New booths become your influence.
5. Booth wins increase public support, funds, and local power.
6. Pick short campaign decisions when the team is ready.
7. Random events change the pace and the Neta Meter.
8. Win only after the minimum campaign time and both influence/mandate targets are crossed.
9. Winning every state/UT completes the fictional national mandate and shows a world-yatra teaser.

## Neta Meter

- Public support: helps convert influence into mandate score.
- Funds: spent on rallies and poster waves.
- Local power: grows through booths, rivals joining, and campaign work.
- Reputation: boosts mandate score but can be traded for quick funds.

## Current Decisions

- Tea Rally: spends funds for public support and reputation.
- Poster Wave: spends funds, claims nearby booths, and adds local power.
- Donor Lunch: gains funds and power, but costs reputation and a little support.

## Round Rules

- Demo: minimum 16 seconds, 55% influence, mandate score 59.
- Standard: minimum 22 seconds, 62% influence, mandate score 65.
- A small loop captures only its enclosed cells; the region boundary is the flood-fill outside edge.
- Self-crossing is allowed and never causes a recount by itself.
- A rival converts only when the player cuts the rival route. Body contact only creates danger feedback.
- A converted rival disappears and its supporters join the player's loose moving crowd.

## Theme Rules

Use these words in UI:

- campaign
- influence
- booths
- district
- mandate
- rally
- volunteers

Avoid these words in user-facing UI:

- capture country
- political war
- real party
- real leader
- caste vote
- religion vote

## Meme Hooks

- Funny fictional party names
- Repeating posters in captured area
- Namaste leader result card
- Breaking poster news headline
- Short share text
- Walking yatra routes and booth queues for reel-friendly movement
- Rounded trails, shaded territory, motion dust, and touch-tuned movement for mobile feel
- National mandate completion card
- Party-colored result card with symbol badge
- Future: 10-second replay export
- Current pitch flow: Quick Demo, `?demo=1`, onboarding overlay, pitch card, and victory ceremony

## Data Plan

Current gameplay data is local and fictional. State/UT silhouettes use attributed open-license geoBoundaries ADM1 data; Survey of India maps are reference-only. Do not import real party, real leader, real slogan, voter targeting, caste, religion, or election result data into gameplay.
