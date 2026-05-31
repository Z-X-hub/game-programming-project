# Fox Dash Presentation Notes

These notes are for the final Game Programming demo / presentation.

## 1. Opening Summary

Fox Dash is a 2D Unity platform runner. The player chooses one of three characters, runs through generated platform sections, collects coins and chests, avoids hazards, and tries to travel as far as possible.

The project was originally called **旋转世界 / Rotating World**, but I changed the final direction to Fox Dash because it gave me a clearer and more realistic scope for a polished vertical slice.

## 2. What The Player Does

- Choose a character from the start screen.
- Start the runner level.
- Move, jump, and roll through platforms.
- Collect coins and chests.
- Avoid water, spikes, saws, and mace hazards.
- Try to survive longer and improve the score.

## 3. Main Character Features To Demonstrate

### PLAYER

- Moves faster than the other two characters.
- Designed for a more risky, speed-focused play style.
- Animation is tuned to feel more like running.

### SOLDIER

- Has one automatic revive after falling or landing in water.
- Does not need the old `E` shield input anymore.
- Designed to be more forgiving for newer players.

### ADVENTURER

- Can double jump.
- Designed for more flexible movement and recovery from gaps.

## 4. Programming Systems To Explain

### GameManager

Controls game state, start, pause, score, death, reset, and screen transitions.

### RedCharacter

Controls movement, jump, roll, death, role abilities, and revive behaviour.

### PlayerCharacterSelection

Stores which role is selected and provides menu labels and ability text.

### KenneyCharacterVisual

Handles runtime character sprite display and animation timing.

### TerrainGenerator

Generates platform blocks and background blocks while the player moves forward.

### UI Screens

The start screen was redesigned to show the game name, cover art, character selection, and cleaner button layout.

## 5. Testing And Improvements To Mention

- Fixed a broken state where the game was not playable after early character changes.
- Fixed Unity 2022 font issue by using a safer font fallback.
- Fixed main menu overlap by redesigning button and character card layout.
- Changed the second character from manual `E` shield to automatic revive after testing the design.
- Improved revive so the character continues near the death position instead of jumping back to an unrelated platform.
- Adjusted character animation timing because early walking/running looked too fast and stiff.
- Removed fake arm-swing overlays because they looked unnatural.

## 6. Honest Limitations

- Character animation is limited by the small number of source sprite frames.
- Procedural platform generation still needs more balancing for difficulty.
- This is a vertical slice, not a full commercial game.
- More accessibility options could be added in future, such as remappable controls and adjustable speed.

## 7. What I Learned

- A focused vertical slice is better than a large unfinished idea.
- Testing in Unity Play mode is essential because a feature can compile but still feel bad.
- Simple abilities are easier for players to understand.
- Some visual effects should be removed if they reduce clarity.
- Documentation, testing records, and GitHub history are part of professional game development.

## 8. Suggested Demo Order

1. Show the main menu and title.
2. Point out the three character cards.
3. Play as `PLAYER` and show faster movement.
4. Restart or return to menu.
5. Play as `SOLDIER` and intentionally fall once to show revive.
6. Play as `ADVENTURER` and show double jump.
7. Show collectables, hazards, score, and end screen.
8. End by explaining the main technical systems and one problem that was fixed through testing.
