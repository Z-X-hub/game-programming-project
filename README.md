# Game Programming Project Repository

This repository records my work for the Game Programming module. It is used to show both the final game outcome and the development process behind it.

## Repository Structure

- `class-exercises/` - classroom exercises and practice Unity projects
- `final-project/` - final course project planning, documentation, development evidence, and final game material

## Final Course Project: Fox Dash

The final project is now **Fox Dash**, replacing the earlier working title **旋转世界 / Rotating World**.

Fox Dash is a 2D Unity platform runner. The player runs through generated platform sections, collects coins and chests, avoids hazards, and chooses between three playable characters with different strengths.

## Current Submission Stage

The project is being uploaded in stages to match the assessment criteria and to show steady development through GitHub.

Completed:

```text
Stage 1 - Game Concept and Design
Stage 2 - Core Playable Game documentation
```

In progress:

```text
Stage 2 - staged source-code upload
```

Stage 3 testing should begin after the Stage 2 source-code upload is complete.

Stage 1 documents are in:

```text
final-project/01-concept-design/
```

Stage 2 development evidence and source-code staging plan are in:

```text
final-project/02-game-development/
```

Project management and Kanban tracking are in:

```text
final-project/00-project-management/
```

## Final Project Folder Layout

```text
final-project/
|-- README.md
|-- 00-project-management/
|   |-- DEVELOPMENT_PLAN.md
|   `-- KANBAN.md
|-- 01-concept-design/
|   |-- GAME_CONCEPT.md
|   |-- CHARACTER_DESIGN.md
|   |-- SCOPE_TOOLS_ASSETS.md
|   `-- LEGAL_ACCESSIBILITY_SECURITY.md
|-- 02-game-development/
|   |-- README.md
|   |-- SOURCE_MANIFEST.md
|   |-- RUN_INSTRUCTIONS.md
|   |-- IMPLEMENTATION_NOTES.md
|   |-- UPLOAD_NOTES.md
|   `-- CODE_STAGING_PLAN.md
|-- 03-testing/
|   `-- README.md
|-- 04-report/
|   `-- README.md
`-- 05-presentation/
    `-- README.md
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

Source-code staging plan:

```text
final-project/02-game-development/CODE_STAGING_PLAN.md
```

## Credits And Licences

Fox Dash uses RedRunner as an open-source reference and retains or adapts selected MIT-licensed material where appropriate. It also uses Kenney Platformer Characters for character-art reference/source material. Stage 1 legal and asset planning notes are in:

```text
final-project/01-concept-design/LEGAL_ACCESSIBILITY_SECURITY.md
```
