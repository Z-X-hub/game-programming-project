# Stage 3C Level Flow, Difficulty, And Feedback Notes

Updated: 2026-06-09

Stage 3C records how the playable runner flow works as a vertical slice. The
goal is not to make a large game, but to show that Fox Dash has a coherent
player loop, readable difficulty, and useful feedback.

## Core Level Flow

The current game loop is:

1. Player starts at the Fox Dash home screen.
2. Player chooses one of three roles.
3. Player enters the runner scene.
4. Terrain generator builds a side-scrolling platform route.
5. Player jumps gaps, rolls when needed, collects coins/chests, and avoids
   hazards.
6. Game records score, coins, and death reason.
7. End screen gives restart/home options.

## Level Structure Evidence

| Area | Evidence | Purpose |
| --- | --- | --- |
| Generated platform route | `Assets/Prefabs/Blocks/Start.prefab` and multiple `Middle_*` block prefabs. | Gives the runner enough variation for a vertical slice. |
| Terrain generation | `TerrainGenerator.cs`, `DefaultTerrainGenerator.cs`, `TerrainGenerationSettings.cs`. | Manages generated blocks, cleanup, and forward movement pacing. |
| Background layers | Background block prefabs and terrain generation support. | Gives the level more depth without changing the core runner loop. |
| Collectables | `Coin.cs`, `Chest.cs`, coin/chest prefabs. | Gives the player short-term reward goals beyond distance score. |
| Hazards | `Water.cs`, `Spike.cs`, `Saw.cs`, `Mace.cs`. | Creates failure states and difficulty variety. |
| Feedback | `GameManager.cs`, `InGameScreen.cs`, `EndScreen.cs`. | Reports score, coins, death reason, and restart/home choices. |

## Difficulty And Character Interaction

| Character | Effect On Level Flow | Difficulty Role |
| --- | --- | --- |
| `PLAYER` | Faster movement makes the same gaps and hazards arrive sooner. | Higher-risk option for players who want speed. |
| `SOLDIER` | One automatic revive makes falls/water less punishing. | Safer option for players learning the route. |
| `ADVENTURER` | Double jump gives more recovery over gaps and awkward platforms. | Flexible movement option. |

This supports the design goal that character choice changes how the same runner
route feels, without requiring three separate levels.

## Feedback Improvements Made During Stage 3

- Added clearer home-screen guidance so players know the goal and character
  differences before starting.
- Added pause/restart/home flow to reduce frustration when testing.
- Added death reason feedback so failures are easier to understand.
- Added current-run coin display so collectables feel more meaningful.
- Added end-screen score, high score, coin, and new-record feedback.
- Fixed terrain cleanup null-reference risk so longer runs are more stable.
- Improved PLAYER run animation so the fast role has better visual feedback.

## Stage 3C Result

Stage 3C is complete for repository evidence. The level flow is clear enough for
a vertical slice: start, choose role, run, collect, avoid hazards, receive
feedback, restart or return home.

## Remaining Tuning For Future Work

- Longer play sessions could tune exact platform spacing and hazard density.
- Coins and chest placement could be adjusted after more player score data.
- A final built executable would help confirm that the Unity editor behaviour
  matches a submitted build.

