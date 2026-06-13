# Game Programming Project Repository

This repository records my work for the Game Programming module. It is used to show both the final game outcome and the development process behind it.

## Quick Marking Guide

1. Open `final-project/README.md` for the assessment evidence map.
2. Open `final-project/FoxDash/` for the current Unity project source.
3. Open `final-project/02-game-development/RUN_INSTRUCTIONS.md` to run the game in Unity.
4. Open `final-project/02-game-development/BUILD_EVIDENCE.md` for the macOS build/export evidence and release link.
5. Open `final-project/03-testing/` for testing, bugs, improvements, and limitations.
6. Open `final-project/04-report/FINAL_REPORT.md` for the final written report.
7. Open `final-project/05-presentation/` for the demo script and project-structure mind map.
8. Open `final-project/FoxDash/THIRD_PARTY_NOTICES.md` and `final-project/FoxDash/AI_DECLARATION.md` for attribution and AI-use transparency.

## Repository Structure

- `class-exercises/` - classroom exercises and practice Unity projects
- `final-project/` - final course project planning, documentation, development evidence, testing evidence, report evidence, and final game material

## Final Course Project: Fox Dash

The final project is now **Fox Dash**, replacing the earlier working title **Rotating World**.

Fox Dash is a 2D Unity platform runner. The player runs through generated platform sections, collects coins and chests, avoids hazards, and chooses between three playable characters with different strengths.

## Current Submission Stage

The project is being uploaded and managed in smaller stages that match the assessment criteria.

Completed:

```text
Stage 1 - Game Concept and Design
Stage 2 - Core Playable Game source evidence
Stage 3A - Playable stability and controls testing
Stage 3B - Character ability and balance testing
Stage 3C - Level flow, difficulty, and feedback iteration
Stage 3D - Bug fixing and regression record
Stage 4A - Report design and technical decisions evidence
Stage 4B - Report testing, limitations, and reflection evidence
Stage 4C - Report final completeness checklist
Professionalism - remove old ShiftTheWorld folder and upload current FoxDash project source
Professionalism - final repository evidence audit
Presentation - demo completed and project-structure mind map added
```

Current:

```text
Final repository evidence is complete.
```

Next:

```text
Optional Windows standalone build if Unity Windows Build Support is installed later.
```

Remaining items:

```text
None
```

## Assessment Evidence Map

| Assessment Area | Repository Evidence |
| --- | --- |
| Game Concept and Design | `final-project/01-concept-design/` |
| Final Game / Playable Build | `final-project/02-game-development/` |
| Testing and Improvement | `final-project/03-testing/` |
| Report | `final-project/04-report/` |
| Professionalism | `final-project/00-project-management/`, Issues, README files, commit history |
| Presentation | `final-project/05-presentation/`, demo script and project-structure mind map |

Detailed process breakdown:

```text
final-project/00-project-management/ASSESSMENT_PROCESS_BREAKDOWN.md
```

Project management and Kanban tracking:

```text
final-project/00-project-management/
```

## Character Roles

- `PLAYER` - faster movement and running-style animation
- `SOLDIER` - one automatic revive after falling or landing in water
- `ADVENTURER` - double jump

## Controls

- Move: `A / D` or left/right arrow keys
- Jump: `Space`, `W`, or up arrow
- Roll: `Left Shift`, `Right Shift`, or `S`
- Select character on menu: click character card or use `1`, `2`, `3`

## Unity Version

The Unity project is developed as **FoxDash** using Unity `2022.3.62f3c1` or a compatible Unity 2022 LTS version.

Main scene:

```text
Assets/Scenes/Play.unity
```

Run instructions:

```text
final-project/02-game-development/RUN_INSTRUCTIONS.md
```

Current Unity source project:

```text
final-project/FoxDash/
```

The older `ShiftTheWorld` prototype folder has been removed from the current
final-project tree so the repository clearly points to Fox Dash as the final
submission game.

## Credits And Licences

Fox Dash uses RedRunner as an open-source reference and retains or adapts selected MIT-licensed material where appropriate. It also uses Kenney Platformer Characters for character-art reference/source material. Stage 1 legal and asset planning notes are in:

```text
final-project/01-concept-design/LEGAL_ACCESSIBILITY_SECURITY.md
```
