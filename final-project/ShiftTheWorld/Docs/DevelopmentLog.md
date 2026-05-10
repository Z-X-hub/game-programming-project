# Shift the World - Development Log

## 2026-05-10 - Project Structure and Core Scripts

### Implemented

- Created the `final-project/ShiftTheWorld/` folder structure.
- Added Unity-ready script folders for player, world control, interactables, managers, and UI.
- Added the core gameplay scripts:
  - `AutoWalker3D`
  - `ControllableObject`
  - `WorldObjectSelector`
  - `MovingPlatform3D`
  - `RotatingPlatform3D`
  - `SwitchTrigger3D`
  - `DoorController3D`
  - `HazardZone3D`
  - `ExitZone3D`
  - `GameManager`
  - `SceneLoader`
  - `UIManager`
  - `CameraFollow2_5D`
- Added starter documentation files for design, testing, credits, and Unity setup.

### Problems Encountered

- Unity scene files should not be hand-written because generated YAML is fragile and can break when opened in another Unity version.
- The project needs to be GitHub-ready even though empty Unity folders are not tracked by Git by default.

### Solutions

- Created scripts and detailed setup instructions instead of generated `.unity` files.
- Added `.gitkeep` files to preserve empty folders for prefabs, materials, art, audio, and scenes.

### Next Steps

- Create the Unity project and assemble scenes manually.
- Add simple materials and UI panels.
- Test each mechanic in `Level01`.
- Record testing evidence and improvement notes in `TestingLog.md`.
