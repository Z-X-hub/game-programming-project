# Stage 3 Before / After Changes

Updated: 2026-06-09

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

## Stage 3B Character Ability Balance Pass

### Before

- Character choice existed, but the testing notes did not clearly prove that each role had a different purpose.
- `SOLDIER` previously had an active shield idea that added control complexity.
- `PLAYER` speed, `SOLDIER` revive, and `ADVENTURER` double jump needed to be described as balanced design choices, not just features.

### After

- `PLAYER` is documented as the fast high-risk/high-reward role.
- `SOLDIER` is documented as the forgiving role with one automatic revive.
- `ADVENTURER` is documented as the movement-flexibility role with double jump.
- The balance notes now explain why the three-role design fits a small runner vertical slice.

## Stage 3C Level Flow And Feedback Pass

### Before

- The runner level flow existed, but the repository did not clearly explain how platforms, collectables, hazards, and UI feedback worked together.
- Testing evidence for difficulty and feedback was spread across separate notes.

### After

- `LEVEL_FLOW_NOTES.md` records how generated blocks, collectables, hazards, score, death reason, and restart/home flow support the player experience.
- Stage 3C is treated as a documentation and evidence pass rather than a major new feature pass.
- Remaining tuning work is recorded as a limitation/future improvement instead of being left unclear.

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

### Stage 3D Upload Note

- The Stage 3D upload records the bug-fix evidence rather than pushing a noisy full-project sync.
- A dry-run source comparison showed that the remaining local script differences were encoding or Unity folder metadata noise, not gameplay logic.
- The local Unity C# build was checked again on 2026-06-09 and completed with `0` warnings and `0` errors.

## Assessment Link

This small iteration supports:

- playable and stable build
- clear controls
- reliable UI feedback
- evidence of testing, debugging, and improvement
- improved animation feedback and visual consistency for the character with the speed-focused role
