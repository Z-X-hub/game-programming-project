# Shift the World - Unity Setup Guide

This guide explains how to assemble the prototype manually in Unity without generated `.unity` scene files.

## 1. Open the Project From This Repository

The current GitHub repository path on the local machine is:

```text
/Users/zhuxuan/2D 游戏课堂练习/github-submit/game-programming-project-upload
```

The final project files are inside:

```text
final-project/ShiftTheWorld/
```

Open the folder in Finder with:

```bash
open "/Users/zhuxuan/2D 游戏课堂练习/github-submit/game-programming-project-upload/final-project/ShiftTheWorld"
```

Important: `final-project/ShiftTheWorld/` currently contains Unity-ready `Assets/` and documentation, but it may not yet be a complete Unity project root because `Packages/` and `ProjectSettings/` are normally created by Unity.

## 2. Create or Connect a Unity 3D Project

Option A - try opening `ShiftTheWorld` directly:

1. Open Unity Hub.
2. Click `Add > Add project from disk`.
3. Select `final-project/ShiftTheWorld/`.
4. If Unity Hub recognises it as a project, open it.
5. If Unity Hub does not recognise it because `Packages/` and `ProjectSettings/` are missing, use Option B below.

Option B - create a fresh Unity project and copy the game files:

1. Open Unity Hub.
2. Create a new `3D` project named `ShiftTheWorld`.
3. Open the new Unity project folder in Finder.
4. Copy this repository folder's `final-project/ShiftTheWorld/Assets/` into the Unity project.
5. Copy `final-project/ShiftTheWorld/Docs/` and `README.md` if you want the documentation inside the Unity project folder.

Use a stable Unity LTS version if available. After import, wait for Unity to compile all scripts.

## 3. Check Console Compile Errors

1. In Unity, open `Window > General > Console`.
2. Clear the Console.
3. Wait for script import to finish.
4. If any red errors appear, double-click the error and fix that script before building scenes.
5. Do not start scene assembly until the Console has no red compile errors.

Warnings are less serious, but red compile errors must be fixed because they stop all scripts from running.

## 4. Create Required Scenes

Create these scenes in `Assets/Scenes/`:

- `MainMenu.unity`
- `LevelSelect.unity`
- `Level01.unity`

Add them to `File > Build Settings` in this order:

1. `MainMenu`
2. `LevelSelect`
3. `Level01`

## 5. MainMenu Scene

Create:

- `Canvas`
- `EventSystem`
- Empty GameObject: `SceneLoader`
- Panel: `MenuPanel`
- Text: `TitleText`
- Text: `SubtitleText`
- Button: `PlayButton`
- Button: `QuitButton`

Add `SceneLoader.cs` to the `SceneLoader` GameObject.

Recommended Canvas setup:

- Render Mode: `Screen Space - Overlay`
- Canvas Scaler UI Scale Mode: `Scale With Screen Size`
- Reference Resolution: `1920 x 1080`
- Match: `0.5`

Recommended menu layout:

- `MenuPanel`: centered vertical layout, bright blue or light cream background, rounded-looking panel if using a sliced sprite
- `TitleText`: `Shift the World`, large and bold
- `SubtitleText`: `Guide the walker by shifting the world.`
- `PlayButton`: bright green or blue
- `QuitButton`: neutral grey or red

Button setup:

- `PlayButton` `OnClick`: drag in `SceneLoader`, call `SceneLoader.LoadLevelSelect`
- `QuitButton` `OnClick`: drag in `SceneLoader`, call `SceneLoader.QuitGame`

Recommended `SceneLoader` values:

- Main Menu Scene Name: `MainMenu`
- Level Select Scene Name: `LevelSelect`
- Level 01 Scene Name: `Level01`
- Quit Game On Cancel: enabled if you want `Esc` to quit from the main menu in a built version

Suggested menu text:

- Title: `Shift the World`
- Subtitle: `Guide the walker by shifting the world.`

## 6. LevelSelect Scene

Create:

- `Canvas`
- `EventSystem`
- Empty GameObject: `SceneLoader`
- Panel: `LevelSelectPanel`
- Text: `TitleText`
- Text: `LevelDescriptionText`
- Button: `Level01Button`
- Button: `BackButton`

Add `SceneLoader.cs` to the `SceneLoader` GameObject.

Recommended Canvas setup:

- Render Mode: `Screen Space - Overlay`
- Canvas Scaler UI Scale Mode: `Scale With Screen Size`
- Reference Resolution: `1920 x 1080`
- Match: `0.5`

Suggested screen copy:

- Title: `Select Level`
- Description: `Level 01 - First World Shift`
- Small note: `Use platforms, switches, and rotating bridges to guide the walker.`

Button setup:

- `Level01Button` `OnClick`: drag in `SceneLoader`, call `SceneLoader.LoadLevel01`
- `BackButton` `OnClick`: drag in `SceneLoader`, call `SceneLoader.LoadMainMenu`

Recommended `SceneLoader` values:

- Load Main Menu On Cancel: enabled
- Cancel Key: `Escape`

This lets the player press `Esc` on the level select screen to return to the main menu.

## 7. Level01 Scene Core Objects

Create these empty GameObjects:

- `GameManager`
- `WorldObjectSelector`
- `UIManager`

Add scripts:

- `GameManager` gets `GameManager.cs`
- `WorldObjectSelector` gets `WorldObjectSelector.cs`
- `UIManager` gets `UIManager.cs`

## 8. Camera Setup

1. Select `Main Camera`.
2. Position it at approximately `(5, 3, -10)`.
3. Rotation should be `(0, 0, 0)` so it looks along the Z axis.
4. Set Projection to `Orthographic`.
5. Set Orthographic Size around `5`.
6. Add `CameraFollow2_5D.cs`.
7. Assign the player transform as the target after creating the player.

This creates a fixed side-view 2.5D camera.

## 9. Player Setup

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

## 10. Ground and Static Platforms

Create simple cubes:

- Main ground: position `(0, -0.25, 0)`, scale `(14, 0.5, 2)`
- Raised platform: position `(3, 1.25, 0)`, scale `(2.5, 0.4, 2)`

Use bright cartoon materials. Static platforms only need a Collider.

## 11. Recommended Level01 Layout

Use a small left-to-right route so the mechanic is easy to understand:

| Part | Suggested Position | Purpose |
| --- | --- | --- |
| Start | `(-6, 1, 0)` | Walker begins here and automatically moves right |
| Static ground | `(0, -0.25, 0)` | Safe base floor |
| First moving platform | `(-2, 0.8, 0)` | Teaches `Space` activation |
| Rotating bridge | `(1.5, 1.1, 0)` | Teaches `Q/E` rotation |
| Door | `(5.5, 1, 0)` | Blocks progress until switch is triggered |
| Switch | `(4, 0.15, 0)` | Opens the door |
| Hazard | `(2.8, -0.1, 0)` | Creates fail state and restart reason |
| Exit | `(7.5, 1, 0)` | Triggers win state |

Keep the route visible in one camera view where possible. If the level is longer, let `CameraFollow2_5D` follow only the X axis.

## 12. Recommended Cartoon Materials

Create simple Unity materials in `Assets/Materials/`:

| Object Type | Suggested Colour | Why |
| --- | --- | --- |
| Player | blue or white | friendly and readable |
| Controllable objects | bright blue or purple | tells the player these are special |
| Selected object | yellow or cyan | strong contrast for selection feedback |
| Hazard | pink or red | clear danger language |
| Exit | green or cyan | clear success/goal language |
| Static platforms | soft blue-purple | calm background support |
| Door | orange or red when closed, cyan when open | state feedback |
| Switch | grey when inactive, green when active | readable mechanism feedback |

For a more polished 2.5D look:

- Use slightly thick platforms, for example Z scale `2`.
- Add a directional light and soft ambient colour.
- Use orthographic camera for clarity.
- Keep all gameplay objects near `Z = 0`.
- Use rounded-looking proportions even if the objects are simple cubes.

## 13. Moving Platform

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
- Use Normal Color Override: optional
- Normal Color: bright blue or purple
- Selected Highlight Color: yellow or cyan
- Scale When Selected: enabled
- Selected Scale Multiplier: `(1.08, 1.08, 1.08)`

## 14. Rotating Platform / Block

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
- Selected Highlight Color: yellow or cyan
- Scale When Selected: enabled
- Selected Scale Multiplier: `(1.08, 1.08, 1.08)`

Use `Q` and `E` to rotate this object when selected.

## 15. Switch and Door

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

## 16. Hazard Zone

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

## 17. Exit Zone

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

## 18. UI Canvas for Level01

Create a Canvas with:

- Empty GameObject or Panel: `HUDRoot`
- Text: `ObjectiveText`
- Text: `SelectedObjectText`
- Text: `RestartHintText`
- Panel: `WinPanel`
- Panel: `FailPanel`
- Panel: `PausePanel`
- Panel: `TutorialHintPanel`

Assign these objects to `UIManager.cs`.

Recommended Canvas setup:

- Render Mode: `Screen Space - Overlay`
- Canvas Scaler UI Scale Mode: `Scale With Screen Size`
- Reference Resolution: `1920 x 1080`
- Match: `0.5`

Recommended `UIManager` references:

- HUD Root: drag `HUDRoot`
- Objective Text: drag `ObjectiveText`
- Selected Object Text: drag `SelectedObjectText`
- Restart Hint Text: drag `RestartHintText`
- Win Panel: drag `WinPanel`
- Fail Panel: drag `FailPanel`
- Pause Panel: drag `PausePanel`
- Win Message Text: drag the body text inside `WinPanel`
- Fail Message Text: drag the body text inside `FailPanel`
- Pause Message Text: drag the body text inside `PausePanel`
- Hide HUD When Panel Open: optional; keep disabled for a simple prototype

Recommended HUD layout:

- `HUDRoot` anchored to the top of the screen
- `ObjectiveText` top-left or top-center
- `SelectedObjectText` under the objective
- `RestartHintText` bottom-center or bottom-left

Suggested text:

- Objective: `Guide the walker to the exit`
- Selected: `Selected: None`
- Hint: `A/D or Left/Right: select | Q/E: rotate | Space: activate | R: restart | Esc: pause`

Panel setup:

- `WinPanel`: centered panel, title `Success`, body `The walker reached the exit.`, buttons `Restart`, `Main Menu`
- `FailPanel`: centered panel, title `Try Again`, body `The walker touched a hazard.`, buttons `Restart`, `Main Menu`
- `PausePanel`: centered panel, title `Paused`, body `Take a moment to plan the next world shift.`, buttons `Resume`, `Restart`, `Main Menu`
- Disable all three panels in the Inspector before pressing Play. `UIManager` will turn them on when needed.

Button setup:

- Restart buttons: drag in `UIManager`, call `UIManager.RestartButtonPressed`
- Resume button: drag in `UIManager`, call `UIManager.ResumeButtonPressed`
- Main Menu buttons: drag in `UIManager`, call `UIManager.MainMenuButtonPressed`

Alternative button setup:

- Restart buttons may call `GameManager.RestartLevel`
- Resume button may call `GameManager.TogglePause`
- Main Menu buttons may call `GameManager.LoadMainMenu`

Using `UIManager` button wrappers is slightly easier because all panel buttons can target one object.

Recommended visual style:

- Bright background panels with high contrast text
- Large button labels
- Yellow or cyan selected-object feedback
- Green success panel
- Red/orange fail panel
- Blue pause panel
- Keep text short and readable

## 19. Tutorial Hint Setup

Create a small first-time hint panel:

1. Inside the Level01 Canvas, create a Panel named `TutorialHintPanel`.
2. Anchor it near the top-center or upper-left, where it does not cover the walker.
3. Add a Text child named `TutorialHintText`.
4. Add `TutorialHint.cs` to `TutorialHintPanel`.
5. Assign:
   - Hint Panel: `TutorialHintPanel`
   - Hint Text: `TutorialHintText`
6. Suggested message:

```text
The walker moves by itself.
Your job is to shift the world.
Select highlighted objects and guide the walker to the exit.
```

Recommended values:

- Show On Start: enabled
- Auto Hide: enabled
- Auto Hide Delay: `7`
- Dismiss Key: `Return`
- Hide When Player Uses World Controls: enabled

This gives enough tutorial information without adding dialogue or a large tutorial system.

## 20. How to Test the Level

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

## 21. How to Test UI and Menus

1. Open `MainMenu`.
2. Press Play.
3. Confirm `LevelSelect` loads.
4. Press Back.
5. Confirm `MainMenu` loads again.
6. Open `LevelSelect`.
7. Press `Esc`.
8. Confirm it returns to `MainMenu` if `Load Main Menu On Cancel` is enabled.
9. Press `Level 01`.
10. Confirm `Level01` loads.
11. Confirm the HUD shows:
    - `Guide the walker to the exit`
    - `Selected: <object name>`
    - restart/control hints
12. Press `A/D` or arrow keys.
13. Confirm selected-object text updates.
14. Press `Esc` in `Level01`.
15. Confirm the pause panel appears.
16. Press Resume.
17. Confirm gameplay resumes.
18. Trigger hazard failure.
19. Confirm the fail panel appears and Restart works.
20. Trigger the exit.
21. Confirm the win panel appears and Main Menu works.

Record results and fixes in `Docs/TestingLog.md`.

## 22. Recording a Demo or Screenshots for the Report

Recommended evidence for coursework:

1. Screenshot the Main Menu.
2. Screenshot the Level Select screen.
3. Screenshot Level01 with the HUD visible.
4. Screenshot a selected object with yellow/cyan highlight.
5. Screenshot the switch opening the door.
6. Screenshot the fail panel after touching a hazard.
7. Screenshot the win panel showing `Level Complete`.
8. Record a 30-60 second gameplay clip:
   - Start from Level01.
   - Select a platform.
   - Activate or rotate mechanisms.
   - Trigger the switch-door interaction.
   - Reach the exit.

On macOS, use:

- `Shift + Command + 5` for screen recording
- `Shift + Command + 4` for screenshots

Keep evidence honest. If something is still not working, record the issue in `TestingLog.md` and explain the planned fix.
