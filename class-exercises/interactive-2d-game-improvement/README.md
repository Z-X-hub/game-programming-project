# Interactive 2D Game Improvement

Unity coursework exercise for improving a simple 2D space shooter.

## How To Run

1. Open this folder in Unity `2022.3.62f3c1` or a compatible Unity 2022 LTS version.
2. Open `Assets/SpaceShooter/Scenes/MainMenu.unity`.
3. Press Play.

## Controls

- Move: WASD or Arrow Keys
- Aim: Mouse
- Shoot: Left Mouse Button or Space
- Pause: Esc

## Game Goal

Clear each sector by defeating the target number of enemies before the ship hull reaches zero.

Sector 1 is a training level with slower enemy spawning, more hull, no boss, and no asteroid hazards. Sector 2 adds the harder enemy mix, destructible hazards, faster threat scaling, and a boss ship.

## Damage Code For Group Study

- Main script studied: `Assets/SpaceShooter/Scripts/Health.cs`
- Selected function: `TakeDamage(int damageAmount)`
- Related event-chain script: `Assets/SpaceShooter/Scripts/Damage.cs`

Projectile collision flow:

`ArcadeProjectile.cs` -> `Damage.cs` -> `TakeDamage()`

## Main Improvements

- Main menu, level select, instructions, credits, pause, retry, next-level, and return-to-menu flow.
- HUD with score, high score, hull, objective progress, timer, threat level, active power-up status, hull bar, and objective bar.
- Gameplay features: two levels, power-ups, dynamic difficulty, boss enemy, and destructible asteroid hazards.
- Feedback and polish: sound effects, background music, hit flashes, camera shake, low-hull danger flash, color-coded enemies, player glow, and persistent control hint.

## Credits

Starter art, font, and the original asset package were provided as class materials for the 2D game improvement exercise. Audio files in this submission are lightweight classroom-friendly replacements using the same Unity asset references.
