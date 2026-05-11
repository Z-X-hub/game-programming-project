# Shift the World - Unity Setup Guide

This guide explains how to assemble the prototype manually in Unity without generated `.unity` scene files.

## 1. Create the Unity Project

1. Open Unity Hub.
2. Create a new 3D project.
3. Name it `ShiftTheWorld`.
4. Use a stable Unity LTS version if available.
5. Copy or keep this repository's `Assets/` folder inside the Unity project.
6. Let Unity import the scripts.
7. Check the Console for compile errors before building scenes.

## 2. Create Required Scenes

Create these scenes in `Assets/Scenes/`:

- `MainMenu.unity`
- `LevelSelect.unity`
- `Level01.unity`

Add them to `File > Build Settings` in this order:

1. `MainMenu`
2. `LevelSelect`
3. `Level01`

## 3. MainMenu Scene

Create:

- `Canvas`
- `EventSystem`
- Title text: `Shift the World`
- Button: `Play`
- Button: `Quit`
- Empty GameObject: `SceneLoader`

Add `SceneLoader.cs` to the `SceneLoader` GameObject.

Button setup:

- `Play` button `OnClick`: call `SceneLoader.LoadLevelSelect`
- `Quit` button `OnClick`: call `SceneLoader.QuitGame`

Suggested menu text:

- Title: `Shift the World`
- Subtitle: `Guide the walker by shifting the world.`

## 4. LevelSelect Scene

Create:

- `Canvas`
- `EventSystem`
- Text: `Select Level`
- Button: `Level 01`
- Button: `Back`
- Empty GameObject: `SceneLoader`

Add `SceneLoader.cs` to the `SceneLoader` GameObject.

Button setup:

- `Level 01` button `OnClick`: call `SceneLoader.LoadLevel01`
- `Back` button `OnClick`: call `SceneLoader.LoadMainMenu`

## 5. Level01 Scene Core Objects

Create these empty GameObjects:

- `GameManager`
- `WorldObjectSelector`
- `UIManager`

Add scripts:

- `GameManager` gets `GameManager.cs`
- `WorldObjectSelector` gets `WorldObjectSelector.cs`
- `UIManager` gets `UIManager.cs`

## 6. Camera Setup

1. Select `Main Camera`.
2. Position it at approximately `(5, 3, -10)`.
3. Rotation should be `(0, 0, 0)` so it looks along the Z axis.
4. Set Projection to `Orthographic`.
5. Set Orthographic Size around `5`.
6. Add `CameraFollow2_5D.cs`.
7. Assign the player transform as the target after creating the player.

This creates a fixed side-view 2.5D camera.

## 7. Player Setup

Create a simple player:

1. Create a Capsule.
2. Rename it `AutoWalker`.
3. Position it near the left side of the level, for example `(-6, 1, 0)`.
4. Add `Rigidbody`.
5. Add or keep `CapsuleCollider`.
6. Add `AutoWalker3D.cs`.

Recommended Rigidbody values:

- Use Gravity: enabled
- Is Kinematic: disabled
- Constraints: Freeze Rotation, Freeze Position Z

Recommended `AutoWalker3D` values:

- Walk Speed: `2.2`
- Start Direction: `1`
- Turn Around When Blocked: enabled
- Freeze Depth Position: enabled

## 8. Ground and Static Platforms

Create simple cubes:

- Main ground: position `(0, -0.25, 0)`, scale `(14, 0.5, 2)`
- Raised platform: position `(3, 1.25, 0)`, scale `(2.5, 0.4, 2)`

Use bright cartoon materials. Static platforms only need a Collider.

## 9. Moving Platform

Create:

- Cube named `MovingPlatform`
- Position `(-1, 1, 0)`
- Scale `(2, 0.3, 2)`
- Add `Rigidbody`
- Add `BoxCollider`
- Add `MovingPlatform3D.cs`
- Add `ControllableObject.cs`

Recommended Rigidbody:

- Is Kinematic: enabled
- Use Gravity: disabled

Recommended `MovingPlatform3D`:

- End Offset: `(0, 2, 0)` for a vertical lift, or `(3, 0, 0)` for a horizontal bridge
- Move Speed: `2`
- Toggle Target On Activate: enabled
- Return To Start When Inactive: enabled
- Carry Walker On Top: enabled

Recommended `ControllableObject`:

- Display Name: `Moving Lift`
- Highlight Renderers: assign the cube renderer, or leave empty to auto-detect

## 10. Rotating Platform / Block

Create:

- Cube named `RotatingBridge`
- Position `(2, 1, 0)`
- Scale `(3, 0.3, 2)`
- Add `BoxCollider`
- Add `RotatingPlatform3D.cs`
- Add `ControllableObject.cs`

Recommended `RotatingPlatform3D`:

- Local Rotation Axis: `(0, 0, 1)`
- Step Angle: `90`
- Rotation Speed: `180`

Recommended `ControllableObject`:

- Display Name: `Rotating Bridge`

Use `Q` and `E` to rotate this object when selected.

## 11. Switch and Door

### Door

Create:

- Cube named `ExitDoor`
- Position `(6, 1, 0)`
- Scale `(0.4, 2, 2)`
- Add `BoxCollider`
- Add `Rigidbody`
- Add `DoorController3D.cs`

Recommended Rigidbody:

- Is Kinematic: enabled
- Use Gravity: disabled

Recommended `DoorController3D`:

- Open Offset: `(0, 2.5, 0)`
- Move Speed: `3`
- Disable Collider When Open: enabled

### Switch

Create:

- Cylinder or cube named `DoorSwitch`
- Position `(4.5, 0.15, 0)`
- Scale around `(0.8, 0.2, 0.8)`
- Add Collider
- Add `SwitchTrigger3D.cs`
- Add `ControllableObject.cs` if the player should also select it manually

Recommended `SwitchTrigger3D`:

- Trigger By Walker: enabled
- Toggle Mode: disabled
- Deactivate When Walker Leaves: disabled
- Activation Targets: drag `ExitDoor` into the list
- Pressed Scale Multiplier: `(1, 0.55, 1)`

The switch collider is automatically set as a trigger by the script.

## 12. Hazard Zone

Create:

- Cube named `HazardSpikes`
- Position under a gap or near the route
- Scale `(2, 0.3, 2)`
- Add `BoxCollider`
- Add `HazardZone3D.cs`

The collider is automatically set as a trigger by the script.

Suggested visual:

- Red material
- Low flat cube or simple spike shapes

## 13. Exit Zone

Create:

- Cube named `ExitZone`
- Position near the end, for example `(8, 1, 0)`
- Scale `(0.8, 2, 2)`
- Add `BoxCollider`
- Add `ExitZone3D.cs`

The collider is automatically set as a trigger by the script.

Suggested visual:

- Green or blue transparent gate
- Text label above it: `Exit`

## 14. UI Canvas for Level01

Create a Canvas with:

- Text: `ObjectiveText`
- Text: `SelectedObjectText`
- Text: `RestartHintText`
- Panel: `WinPanel`
- Panel: `FailPanel`
- Panel: `PausePanel`

Assign these objects to `UIManager.cs`.

Suggested text:

- Objective: `Guide the walker to the exit`
- Selected: `Selected: None`
- Hint: `A/D or Left/Right: select | Q/E: rotate | Space: activate | R: restart | Esc: pause`

Panel setup:

- `WinPanel`: title `Success`, body `The walker reached the exit.`, button `Restart`
- `FailPanel`: title `Try Again`, body `The walker touched a hazard.`, button `Restart`
- `PausePanel`: title `Paused`, buttons `Resume`, `Restart`, `Main Menu`

Button setup:

- Restart buttons call `GameManager.RestartLevel`
- Main Menu button calls `GameManager.LoadMainMenu`
- Resume button calls `GameManager.TogglePause`

## 15. How to Test the Level

1. Press Play in `Level01`.
2. Confirm the walker automatically moves along X.
3. Confirm the walker does not move along Z.
4. Press `A/D` or arrow keys to change selected objects.
5. Confirm the selected object changes colour and the UI name updates.
6. Select the moving platform and press `Space`.
7. Confirm the moving platform moves between endpoints.
8. Select the rotating bridge and press `Q/E`.
9. Confirm it rotates smoothly by 90 degrees.
10. Let the walker touch the switch.
11. Confirm the door opens.
12. Let the walker touch the hazard.
13. Confirm the fail panel appears.
14. Press `R`.
15. Confirm the level restarts.
16. Guide the walker to the exit.
17. Confirm the win panel appears.

Record results and fixes in `Docs/TestingLog.md`.
