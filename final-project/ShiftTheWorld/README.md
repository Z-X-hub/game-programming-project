# Shift the World

`Shift the World` is a small Unity 2.5D puzzle platformer prototype for the Game Programming coursework final project.

The character walks automatically. The player does not directly control the hero. Instead, the player controls selected world objects such as platforms, rotating blocks, switches, and doors to guide the walker safely to the exit.

## Controls

- `A` / `D` or `Left Arrow` / `Right Arrow`: select controllable object
- `Q` / `E`: rotate selected object left or right
- `Space`: activate selected object
- `R`: restart level
- `Esc`: pause the level, or return to menu after win/fail

## How to Run

1. Create or open a Unity 3D project.
2. Copy or keep this `ShiftTheWorld` folder as the Unity project root, or copy the `Assets/` folder into a Unity project.
3. In Unity, manually create these scenes inside `Assets/Scenes/`:
   - `MainMenu`
   - `LevelSelect`
   - `Level01`
4. Add the scenes to `File > Build Settings`.
5. Follow `Docs/UnitySetupGuide.md` to assemble the menu, level, player, objects, UI, and camera.
6. Press Play from `MainMenu` or `Level01`.

No `.unity` scene files are generated in this repository because hand-written Unity scene YAML is fragile and can break projects.

## Implemented Features

- Auto-walking 3D character constrained to side-view 2.5D movement
- Fixed side-view camera helper
- Keyboard selection of controllable world objects
- Selected object highlighting and selected-name UI feedback
- Moving platform activation
- Smooth 90 degree rotating platform/block
- Switch/button trigger that can control mechanisms
- Door/gate controlled by switch activation
- Hazard trigger and fail state
- Exit trigger and win state
- Restart and pause support
- Menu scene loading helper
- Beginner-friendly modular C# scripts

## Project Structure

```text
ShiftTheWorld/
├── Assets/
│   ├── Scripts/
│   │   ├── Player/
│   │   ├── WorldControl/
│   │   ├── Interactables/
│   │   ├── Managers/
│   │   └── UI/
│   ├── Prefabs/
│   ├── Materials/
│   ├── Art/
│   ├── Audio/
│   └── Scenes/
├── Docs/
└── README.md
```

## Known Limitations

- The repository provides scripts and setup guidance, not generated Unity scene files.
- Art, sound, and materials are placeholder-ready and should be created in Unity.
- The current scope is one polished vertical slice level, not a full multi-level game.
- UI uses Unity's built-in `Text` component to avoid extra package dependencies.

## Credits

See `Docs/Credits.md`.
