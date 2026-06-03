# Stage 3A Before / After Changes

Updated: 2026-06-03

## Before

- The home menu did not explain the game clearly enough for a first-time player.
- Pause and end screens had overlapping legacy and generated controls.
- End-game feedback was limited.
- Current-run coin progress was not visible during play.
- Terrain cleanup could throw a null-reference exception.

## After

- The home menu now includes a compact quick guide.
- Pause flow now provides clear resume, restart, and home choices.
- End-game feedback now explains the reason for death and run results.
- The in-game HUD now shows current-run coins and total coins.
- Terrain cleanup now handles missing generated block references safely.

## Assessment Link

This small iteration supports:

- playable and stable build
- clear controls
- reliable UI feedback
- evidence of testing, debugging, and improvement
