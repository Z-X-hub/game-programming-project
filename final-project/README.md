# Final Project: Fox Dash

**Fox Dash** is my final Game Programming module project. It replaces the earlier working title **旋转世界 / Rotating World**.

The game is a 2D Unity platform runner built as a vertical slice. The goal is to deliver a small but complete playable experience with clear movement, useful feedback, character choice, hazards, collectibles, score, UI, sound, and testing evidence.

## Game Concept

The player runs across generated platform sections, jumps over gaps, collects coins and chests, and avoids enemies or traps. The main design idea is simple: keep the runner gameplay easy to understand, but add meaningful character choice so the player can approach the same level in different ways.

## Character Design

The game has three playable characters.

| Character | Ability | Design Purpose |
| --- | --- | --- |
| `PLAYER` | Faster movement | Feels fast and risky; best for players who want speed and distance. |
| `SOLDIER` | One automatic revive after falling or landing in water | More forgiving; gives new players a second chance without needing a button input. |
| `ADVENTURER` | Double jump | More flexible movement; helps cross gaps and recover from mistakes. |

The first character is intended to feel like a runner, while the other two use calmer walking movement. This helps the player feel the difference between the speed-focused character and the utility-focused characters.

## Core Gameplay Loop

1. Choose a character from the start screen.
2. Start running through generated platform sections.
3. Jump, roll, and avoid hazards.
4. Collect coins and chests.
5. Try to travel farther and increase the score.
6. On death, view the end screen and restart.

## Controls

- Move: `A / D` or left/right arrow keys
- Jump: `Space`, `W`, or up arrow
- Roll: `Left Shift`, `Right Shift`, or `S`
- Character select: click a character card or press `1`, `2`, `3`

## Main Unity Scene

```text
Assets/Scenes/Play.unity
```

Recommended Unity version:

```text
Unity 2022.3.62f3c1
```

A compatible Unity 2022 LTS editor should also work.

## Main Systems

- `GameManager` controls game start, pause, death, score, and reset flow.
- `RedCharacter` controls player movement, jumping, rolling, death, revive, and role abilities.
- `PlayerCharacterSelection` stores the selected character and menu labels.
- `KenneyCharacterVisual` handles the runtime character sprite display and movement animation style.
- `TerrainGenerator` generates platform blocks and background blocks during play.
- `Collectables` handle coins and chests.
- `Enemies` handle saws, spikes, water, and mace hazards.
- `UI` handles start screen, character selection, score, pause, and end screen.

## Assessment Evidence

The following supporting files are included for the module assessment:

- `TESTING.md` - playtesting and debugging record
- `DEVELOPMENT_LOG.md` - development process and progress evidence
- `REPORT_DRAFT.md` - draft report for design and technical reflection
- `THIRD_PARTY_NOTICES.md` - external asset and licence notes

## Current Known Limitations

- Character animation uses a limited two-frame source set, so extra polish is done through movement timing and transform animation rather than full hand-authored animation clips.
- The game is a vertical slice, not a complete commercial-length game.
- Some original RedRunner-derived assets and systems remain, but they are renamed, reorganised, documented, and extended for this project.
