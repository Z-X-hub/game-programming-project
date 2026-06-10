# Fox Dash Demo Script

Updated: 2026-06-10

## 1. Introduction

Fox Dash is a 2D platform runner vertical slice. The player chooses a character,
runs through generated platforms, collects coins, avoids hazards, and tries to
survive for longer.

## 2. Main Goal

The goal is to show a small but complete game loop: choose a role, start the
run, react to platforms and hazards, collect rewards, then receive clear score
and death feedback.

## 3. Features To Demonstrate

- Main menu and character selection.
- `PLAYER` fast movement and running animation.
- `SOLDIER` one-time automatic revive.
- `ADVENTURER` double jump.
- Coin collection and score UI.
- Hazard death reason.
- Restart and home flow.

## 4. Technical Points

- `PlayerPrefs` saves the selected role through `PlayerCharacterSelection.cs`.
- `FoxDashCharacter.cs` handles role-specific movement, revive, and double-jump checks.
- `KenneyCharacterVisual.cs` handles the imported character visuals and fast-run animation.
- `GameManager.cs` handles score, coins, death reason, and game flow.
- `TerrainGenerator.cs` manages generated runner sections and cleanup.

## 5. Reflection

The main improvements came from testing: clearer menu guidance, better pause and
death feedback, live coin statistics, animation polish, bug fixes, and stronger
character identity.

## 6. Likely Questions

| Question | Short Answer |
| --- | --- |
| What is the game in one sentence? | Fox Dash is a 2D Unity runner where three characters create different ways to survive the same platform challenge. |
| What does the player do moment to moment? | The player jumps, rolls, collects coins, avoids hazards, and decides how to use the selected character's strength. |
| What is original? | The Fox Dash identity, three-role system, menu redesign, feedback UI, Kenney visual integration, testing records, and bug fixes are project-specific work. |
| What was modified from references? | The runner foundation was adapted from RedRunner, then reorganised, rebranded, extended, and documented for Fox Dash. |
| What changed after testing? | UI blocking was fixed, death feedback was added, coins became visible during play, SOLDIER revive was simplified, and PLAYER animation/scale was improved. |
