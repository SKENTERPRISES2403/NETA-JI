# NETA JI Demo Privacy Note

## Current Offline PWA

The current pitch demo has no accounts, analytics SDK, advertising SDK, MongoDB, Render service, or gameplay backend.

The app stores only campaign progress and onboarding state in the browser's local storage on the user's device. Party name, slogan, color, won regions, and Neta Meter values are not uploaded by the app.

Poster export is created locally in the browser. Sharing happens only after the player presses a share/save control and uses the device's browser or operating-system share interface.

The player can clear local campaign data with the in-game `Reset` control or by clearing site data in the browser.

## Before Public Accounts Or Sharing

Add and review a full privacy policy before introducing any of the following:

- User accounts or cloud progress.
- Analytics, advertising, crash reporting, or attribution SDKs.
- Hosted party names, slogans, posters, or replay clips.
- Multiplayer rooms, chat, leaderboards, or friend systems.
- Device identifiers, notifications, payments, or age-related data.

Public user-generated content also requires server-side moderation, reporting, takedown, retention limits, access control, rate limits, and audit logging. See `docs/production-roadmap.md` and `docs/safety-policy.md`.

This note describes the current demo behavior and is not a substitute for final legal review or app-store privacy disclosures.
