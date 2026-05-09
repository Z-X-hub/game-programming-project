# Submission Notes

## Assignment Requirements Covered

- Main menu or start screen: New Game, Level Select, Instructions, Credits / Changes, Exit.
- HUD values: score, high score, hull, objective progress, timer, threat level, active power-up status.
- Gameplay change: power-ups, dynamic difficulty, boss enemy, second level, and destructible hazards.
- Feedback: sound effects, hit flashes, camera shake, low-hull flash, status messages, and color-coded enemies.
- Objective text: shown at level start, in the pause screen, and through the HUD objective counter.
- Restart and return flow: retry, next sector, and main menu buttons are included.

## Balance Notes

Sector 1 is tuned as the first playable learning level:

- 8 enemies required
- 8 hull
- Slow spawn timing
- 3 enemies maximum alive
- No boss
- No asteroid hazards
- Slow threat scaling

Sector 2 is the challenge level:

- 16 enemies required
- Boss ship appears late in the run
- Destructible asteroid hazards enabled
- Faster threat scaling

## Test Notes

Unity batch validation passed for:

- `Assets/SpaceShooter/Scenes/MainMenu.unity`
- `Assets/SpaceShooter/Scenes/Level1.unity`
- `Assets/SpaceShooter/Scenes/Level2.unity`

The project includes `Packages`, `ProjectSettings`, and the Unity `.meta` files needed for asset references.
