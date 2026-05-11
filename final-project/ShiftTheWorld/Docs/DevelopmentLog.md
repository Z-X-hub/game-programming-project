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

## 2026-05-11 - Core Playable Logic Refinement

### Implemented

- Improved `AutoWalker3D` so the walker fully stops horizontal movement when the level is paused, won, or failed.
- Improved `MovingPlatform3D` so a walker standing on top can be carried by the platform during movement.
- Improved `WorldObjectSelector` so missing or disabled controllable objects are ignored safely.
- Improved `RotatingPlatform3D` so rotation pauses when gameplay is no longer active.
- Improved `SwitchTrigger3D` with safer event handling and relative pressed-scale feedback.
- Improved `DoorController3D` so physics movement uses `FixedUpdate` and the collider is re-enabled when the door closes.
- Improved `GameManager` so win/fail states stop the walker immediately.
- Improved `UIManager` so selected-object UI stays correct even if Unity runs script `Start` methods in a different order.

### Problems Encountered

- A moving platform in a 3D physics scene may not reliably carry a Rigidbody character if it only moves underneath the player.
- Unity script startup order can cause UI text to show `Selected: None` after an object was already selected.
- Win/fail UI can appear while the walker still has Rigidbody velocity from the previous physics frame.

### Solutions

- Added simple walker parenting while the walker is standing on top of a moving platform.
- Added selected-object refresh logic in `UIManager`.
- Added explicit horizontal velocity clearing in `AutoWalker3D` and walker stopping in `GameManager`.

### Next Steps

- Import the scripts into Unity and check for Console errors.
- Assemble `Level01` using the setup guide.
- Playtest the moving platform, rotating platform, switch-door route, hazard fail state, exit win state, and restart key.

## 2026-05-11 - UI and Menu Support

### Implemented

- Improved `SceneLoader` for Main Menu and Level Select button flows.
- Added optional `Esc` handling in menu scenes, so Level Select can return to Main Menu.
- Improved `UIManager` with optional HUD root control, clearer default win/fail/pause text, and button-friendly methods.
- Added UI button wrapper methods:
  - `RestartButtonPressed`
  - `ResumeButtonPressed`
  - `MainMenuButtonPressed`
- Expanded `UnitySetupGuide.md` with detailed Canvas, panel, button, and testing instructions.

### Problems Encountered

- Menu scenes need to be manually assembled, so button setup must be very explicit.
- In-game buttons are easier to connect if all panel buttons can target a single UI object.
- UI should support a cartoon look without depending on external UI assets.

### Solutions

- Kept UI implementation based on standard Unity Canvas, `Text`, `Button`, and `Panel` objects.
- Used `SceneLoader` for menu navigation and `UIManager` for in-game HUD and panel actions.
- Documented recommended colours, layout, anchors, and button connections instead of generating fragile scene YAML.

### Next Steps

- Build the Main Menu, Level Select, and Level01 Canvas objects inside Unity.
- Verify all button `OnClick` events.
- Record UI readability and navigation test results in `TestingLog.md`.
