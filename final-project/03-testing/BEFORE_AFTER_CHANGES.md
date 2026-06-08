# Stage 3 Before / After Changes

Updated: 2026-06-08

## Stage 3A UI And Feedback Pass

### Before

- The home menu did not explain the game clearly enough for a first-time player.
- Pause and end screens had overlapping legacy and generated controls.
- End-game feedback was limited.
- Current-run coin progress was not visible during play.
- Terrain cleanup could throw a null-reference exception.

### After

- The home menu now includes a compact quick guide.
- Pause flow now provides clear resume, restart, and home choices.
- End-game feedback now explains the reason for death and run results.
- The in-game HUD now shows current-run coins and total coins.
- Terrain cleanup now handles missing generated block references safely.

## Stage 3C/3D Animation Polish And Bug Fix

### Before

- The fast `PLAYER` role used a short low-frame run sequence, so the animation looked choppy.
- A later higher-frame video sequence made the run smoother, but the run sprites were exported at `96x128` with a larger transparent area than the idle sprite.
- In Unity, the character therefore appeared to grow when moving from idle to running.

### After

- The run animation now uses an 85-frame sequence extracted from the run reference video.
- The exported run sprites now use the same `80x110` canvas size as the existing idle/stand sprites.
- The visible run-sprite height was checked against the idle sprite: idle height is about `96px`, and run frames now sit around `88-96px`.
- `KenneyCharacterVisual.cs` keeps the runner scale fixed while the run sequence is playing, so the sprite animation carries the movement instead of code-driven stretching.

## Assessment Link

This small iteration supports:

- playable and stable build
- clear controls
- reliable UI feedback
- evidence of testing, debugging, and improvement
- improved animation feedback and visual consistency for the character with the speed-focused role
