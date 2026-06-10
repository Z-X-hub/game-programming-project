# Fox Dash Final Report

Status: final submission version for repository evidence.

Updated: 2026-06-10

## 1. Introduction

Fox Dash is a 2D Unity platform runner vertical slice inspired by RedRunner.
The player chooses one of three characters, runs through generated platform
sections, collects coins and chests, avoids hazards, and tries to survive for
as long as possible.

The project focuses on a small but complete player experience rather than a
large unfinished game. The main design goal is to make the run easy to
understand, visually clear, and meaningfully different depending on the selected
character. It is not a fixed level-clear game. The main challenge is improving
personal distance, coin collection, and route execution across changing
obstacle combinations.

## 2. Game Concept And Player Experience

The core loop is:

1. Choose a character on the home screen.
2. Start the runner section.
3. Jump gaps, roll when needed, and avoid hazards.
4. Collect coins and chests.
5. Receive score, coin, and death feedback.
6. Restart or return home.

This loop was chosen because it is suitable for a vertical slice. It allows the
game to demonstrate movement, collision, UI, animation, feedback, scoring,
collectables, generated terrain, and restart flow without requiring a very large
amount of hand-authored level content.

Although Fox Dash is single-player, the high-score loop gives the player a
competitive goal. The player is trying to beat their own record, choose better
routes, and use the selected character more effectively.

## 3. Main Design Decisions

### Three Playable Roles

The three-character system is the main creative feature.

| Character | Ability | Design Purpose |
| --- | --- | --- |
| `PLAYER` | Faster movement | High-risk, high-reward speed role. |
| `SOLDIER` | One automatic revive after falling or landing in water | More forgiving role for players learning the game. |
| `ADVENTURER` | Double jump | Movement-flexibility role for recovering from gaps. |

This gives the player a meaningful choice before the run starts. The same level
flow feels different depending on the selected role.

### Generated Obstacles And Coin Risk

The game uses reusable platform blocks selected by the terrain generator. This
keeps the project within scope while still making the run replayable. The
challenge comes from combinations of gaps, water, spikes, saws, maces, coins,
and chests.

Coins are not only decoration. They add a risk-reward decision: some routes can
lead to more coins, but those routes may also require tighter jumps or better
timing around hazards. This helps the runner feel less repetitive because the
player can choose between safer survival and more rewarding but harder routes.

### SOLDIER Revive Instead Of Active Shield

An earlier idea was to give SOLDIER an active `E` shield ability. This was
removed because it added another control and made the game less clear for a
small runner. The final design uses one automatic revive, which fits the runner
loop better because the player does not need to remember an extra key.

### Clearer Feedback

Testing showed that the player needed more information during and after a run.
The game now shows:

- current-run coins
- total coins
- score
- high score
- new-record feedback
- death reason
- restart and home options

This makes failure easier to understand and makes collecting coins feel more
meaningful.

## 4. Technical Decisions

### Unity Project Structure

The final Unity project is uploaded at:

```text
final-project/FoxDash/
```

The main scene is:

```text
Assets/Scenes/Play.unity
```

The project keeps Unity generated folders out of GitHub:

```text
Library/
Temp/
Logs/
UserSettings/
.vscode/
*.csproj
*.sln
```

This keeps the repository more readable and avoids uploading local cache files.

### Important Scripts

| Area | Main Files | Purpose |
| --- | --- | --- |
| Game flow | `GameManager.cs`, `UIManager.cs` | Start, pause, death, restart, score, and run state. |
| Player logic | `FoxDashCharacter.cs`, `Character.cs` | Movement, jump, death, revive, role stats, and character behaviour. |
| Character selection | `PlayerCharacterSelection.cs`, `StartScreen.cs` | Stores selected role and applies it to the run. |
| Character visuals | `KenneyCharacterVisual.cs` | Loads character sprites, role visuals, running frames, and speed feedback. |
| UI screens | `StartScreen.cs`, `InGameScreen.cs`, `PauseScreen.cs`, `EndScreen.cs` | Home menu, HUD, pause controls, and end feedback. |
| Level flow | `TerrainGenerator.cs`, `DefaultTerrainGenerator.cs` | Generated runner blocks, random/probability-based block choice, and cleanup. |
| Collectables/hazards | `Coin.cs`, `Chest.cs`, `Water.cs`, `Spike.cs`, `Saw.cs`, `Mace.cs` | Rewards, death triggers, and failure feedback. |

### Role Implementation

`FoxDashCharacter.cs` applies role behaviour. The speed role changes run and
max run speed. SOLDIER receives a one-time revive flag and grace period.
ADVENTURER uses jump-count logic so it can jump twice before landing.

`PlayerCharacterSelection.cs` stores the chosen role with `PlayerPrefs`, which
allows the menu choice to be used when gameplay begins.

### Visual Implementation

`KenneyCharacterVisual.cs` loads Kenney-style sprites and optional high-frame
PLAYER run animation frames. This was important because the fast character
needed to feel different visually, not only numerically.

## 5. External Resources And Influence

Fox Dash uses RedRunner as an open-source reference foundation under the MIT
License. It also uses Kenney Platformer Characters for character-art source
material. These are credited in:

```text
final-project/FoxDash/THIRD_PARTY_NOTICES.md
```

The project is not presented as fully original from nothing. The original
runner structure was studied and adapted, while the Fox Dash-specific work
includes the title, character selection, character abilities, UI changes,
testing records, documentation, and final repository organisation.

AI assistance is declared separately in:

```text
final-project/FoxDash/AI_DECLARATION.md
```

AI was used as selective support for difficult debugging and structuring, not as
a replacement for student responsibility.

## 6. Original Contribution

Fox Dash was developed from an open-source runner foundation, but the final
assessed work includes several project-specific contributions:

- redesigned the game identity as Fox Dash
- added a three-character selection system
- implemented `PLAYER` as a faster high-risk role
- implemented `SOLDIER` as a one-time automatic revive role
- implemented `ADVENTURER` as a double-jump role
- redesigned the home screen to explain the game and character abilities
- added clearer score, coin, death reason, restart, and home feedback
- improved the fast character run animation and fixed the run-size mismatch
- fixed UI overlap and terrain cleanup problems found during testing
- documented testing, limitations, third-party use, and AI assistance

These changes are the main student contribution beyond using RedRunner as a
reference structure.

## 7. Testing And Improvement Summary

Stage 3 testing evidence is stored in:

```text
final-project/03-testing/
```

Important changes after testing included:

- fixing UI overlap and unclickable screen states
- adding pause/restart/home flow
- adding death reason and end-screen result feedback
- adding in-game coin statistics
- fixing terrain cleanup null-reference risk
- improving PLAYER running animation
- fixing the issue where PLAYER appeared larger while running
- recording external playtest feedback from David, Zane, and Ken
- documenting limitations and remaining final checks

The local build check completed successfully:

```text
dotnet build Assembly-CSharp.csproj --no-restore
0 warnings
0 errors
```

After the final evidence cleanup, the runtime and editor C# projects were
checked again on 2026-06-10 with `0 warnings` and `0 errors`.

The final build/export evidence is recorded in:

```text
final-project/02-game-development/BUILD_EVIDENCE.md
```

The macOS standalone build was exported through the Unity GUI and uploaded as
release evidence. A Windows build was not included because the available Unity
installation only had macOS standalone support installed.

## 8. Technical Limitation

The game is a vertical slice, so it has limitations:

- platform and hazard balance could be improved with more player data
- Windows build export would still be useful if the assessor marks on Windows
- accessibility is basic and could be improved with remappable controls or
  larger text options
- `FoxDashCharacter.cs` still contains several responsibilities inherited from the
  original runner structure. With more time, I would separate movement, role
  abilities, and visual feedback into smaller components such as
  `PlayerMovement`, `CharacterAbilityController`, and `CharacterVisualController`.

One professionalism improvement was completed before final submission evidence:
`Assets/Sounds/Enemies/Water.wav` was compressed from about `85 MB` to about
`9.8 MB` while keeping the same file path and Unity `.meta` GUID.

## 9. Conclusion

Fox Dash meets the goal of a small, playable Unity vertical slice. It has a
clear runner loop, three role-based characters, collectables, hazards, UI
feedback, testing evidence, and staged GitHub documentation.

The strongest part of the project is the character role system and the way
testing changed the game from a basic runner into a more complete and readable
player experience.
