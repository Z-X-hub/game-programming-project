# Fox Dash Development Log

This document records the development process for the Fox Dash final project. It is written to show planning, iteration, problem solving, and response to testing.

## Project Direction

The project originally used the working title **旋转世界 / Rotating World**. The final project direction was changed to **Fox Dash**, a focused 2D platform runner vertical slice.

This change made the scope more realistic for the module. Instead of trying to build a large game, Fox Dash focuses on one polished gameplay loop: choose a character, run through generated platforms, avoid hazards, collect items, and try to survive longer.

## Development Milestones

### 1. Project Cleanup And Branding

- Renamed and organised the project as Fox Dash.
- Added project structure documentation.
- Preserved third-party notices for RedRunner-derived code and assets.
- Checked the project against the original reference project to recover from unstable changes.

### 2. Restoring Stable Gameplay

- The game became unplayable after early attempts to add extra characters.
- Runtime errors were investigated through the Unity Console.
- Broken character references and script issues were fixed.
- The game was brought back to a playable state before adding new features again.

### 3. Character Selection System

- Added a character selection system on the home screen.
- Added persistent selected-character state through `PlayerCharacterSelection`.
- Added three role identities:
  - `PLAYER`
  - `SOLDIER`
  - `ADVENTURER`

### 4. Character Abilities

Initial character ideas were refined through testing.

- `PLAYER` became the faster character.
- `SOLDIER` was changed from a manual `E` shield ability to one automatic revive after falling or landing in water.
- `ADVENTURER` kept the double jump ability.

The revive change was made because it is clearer for players and easier to understand without extra controls.

### 5. Character Visuals

- Added Kenney Platformer Characters as the character sprite source.
- Selected three visually different characters from the pack.
- Added runtime sprite loading under `Assets/Resources/FoxDash/KenneyCharacters`.
- Increased character scale to improve readability.
- Tuned movement animation so the fast character feels like running while the other two feel like walking.

A failed attempt was made to fake arm swinging using extra overlay objects. Testing showed this looked like an unknown object moving near the character instead of a real arm, so it was removed. This was a useful design lesson: a simpler animation that matches the source art is better than an artificial effect that breaks visual clarity.

### 6. Main Menu Redesign

- Replaced the old RedRunner-style title with `FOX DASH`.
- Removed unused social platform buttons.
- Added a generated cover image inspired by a colourful voxel platformer style.
- Reworked button layout so the UI no longer blocks the background art.
- Added compact character cards for the three playable roles.

### 7. Testing And Bug Fixing

Important fixes included:

- Unity 2022 font compatibility issue with old `Arial.ttf` built-in font usage.
- Null reference errors in character update and visual logic.
- Character selection overlap on the main menu.
- Unnatural walk/run animation speeds.
- Incorrect revive behaviour for the second character.

Testing was done through Unity Play mode and repeated C# compile checks.

## Current Final Slice

The current vertical slice includes:

- A themed start screen
- Three playable character roles
- Role-specific movement abilities
- Procedurally generated runner gameplay
- Collectables and hazards
- Score and UI flow
- Sound and particle feedback
- Documentation and testing evidence

## What I Learned

- Keeping scope realistic is important. A smaller game with a clear loop is easier to polish.
- Visual clarity matters more than adding complicated effects.
- Testing in Play mode is necessary because compile success does not prove gameplay feels good.
- Character abilities should be obvious to players and should not depend on too many hidden controls.
- Documentation and GitHub history are part of the project, not something to leave until the end.

## Next Improvements

- Push the full Unity project source into the final project folder if it is not already included.
- Make a final build and test it outside the Unity Editor.
- Capture screenshots or a short demo video for the presentation.
- Finish the final written report using `REPORT_DRAFT.md` as the base.
