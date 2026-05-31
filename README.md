# Game Programming Project Repository

This repository records my work for the Game Programming module. It is used to show both the final game outcome and the development process behind it.

## Repository Structure

- `class-exercises/` - classroom exercises and practice Unity projects
- `final-project/` - final course project documentation and final game material

## Final Course Project: Fox Dash

The final project is now **Fox Dash**, replacing the earlier working title **旋转世界 / Rotating World**.

Fox Dash is a 2D Unity platform runner. The player runs through generated platform sections, collects coins and chests, avoids hazards, and chooses between three playable characters with different strengths.

### Core Player Experience

The game is designed as a compact vertical slice: one polished, playable runner experience rather than a large unfinished game. The main focus is responsive movement, clear feedback, character choice, and a stable gameplay loop.

### Main Features

- Procedurally generated 2D platform runner level sections
- Three character choices:
  - `PLAYER` - faster movement and running-style animation
  - `SOLDIER` - one automatic revive after falling or landing in water
  - `ADVENTURER` - double jump
- Coins, chests, score, enemies, water, spikes, saws, and moving hazards
- Start menu, character selection, in-game HUD, pause, and end screen
- Sound effects, animation feedback, particle effects, and camera follow
- Testing and development records for assessment evidence

## Final Project Documentation

The final project folder includes the main supporting documents:

- `final-project/README.md` - game overview, controls, and run instructions
- `final-project/TESTING.md` - testing evidence and fixes made
- `final-project/DEVELOPMENT_LOG.md` - development progress and changes over time
- `final-project/REPORT_DRAFT.md` - report draft covering design, technical choices, testing, and reflection
- `final-project/THIRD_PARTY_NOTICES.md` - asset, code, and licence notes

## Running the Final Game

The Unity project is developed as **FoxDash** using Unity `2022.3.62f3c1` or a compatible Unity 2022 LTS version.

Main scene:

```text
Assets/Scenes/Play.unity
```

Before final submission, the Unity project source should be included under the final project area so the repository contains the game, documentation, and process evidence together.

## Controls

- Move: `A / D` or left/right arrow keys
- Jump: `Space`, `W`, or up arrow
- Roll: `Left Shift`, `Right Shift`, or `S`
- Select character on menu: click character card or use `1`, `2`, `3`

## Credits And Licences

Fox Dash is based on RedRunner-derived open-source code and assets, with additional character assets from Kenney Platformer Characters. Licence and credit details are recorded in `final-project/THIRD_PARTY_NOTICES.md`.
