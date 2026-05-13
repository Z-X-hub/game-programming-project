# Shift the World - Testing Log

This log records planned and completed testing for the coursework vertical slice. It should stay honest: tests that have not yet been run in Unity are marked as not tested yet.

## Current Testing Status

The project has had local code review and script cleanup. Full Unity Editor playtesting is still required after manually creating the scenes and assigning Inspector references.

## Test Plan

| Date | Test | Expected Result | Actual Result | Issue Found | Fix / Improvement |
| --- | --- | --- | --- | --- | --- |
| 2026-05-13 | Unity compile test | All scripts import with no red Console errors | Not tested in Unity yet | Unity scene/project import still required | Open Unity, import `Assets/`, and check Console before scene assembly |
| 2026-05-13 | AutoWalker movement test | Walker automatically moves along the X axis without direct player input | Not tested in Unity yet | Pending Play Mode test | Verify Rigidbody settings and `AutoWalker3D` speed |
| 2026-05-13 | 2.5D constraint test | Walker and gameplay objects stay near `Z = 0`; no free 3D movement | Not tested in Unity yet | Pending Play Mode test | Use Rigidbody Freeze Position Z and fixed side-view camera |
| 2026-05-13 | Object selection test | `A/D` or arrow keys cycle through controllable objects in world order | Not tested in Unity yet | Pending Play Mode test | Confirm `WorldObjectSelector` finds all `ControllableObject` components |
| 2026-05-13 | Highlight feedback test | Selected object changes to yellow/cyan and slightly scales up; deselected object restores colour/scale | Not tested in Unity yet | Pending Play Mode test | `ControllableObject` now uses `MaterialPropertyBlock` and scale feedback |
| 2026-05-13 | No selectable object UI test | If no controllable objects exist, UI shows a reasonable message | Not tested in Unity yet | Pending Play Mode test | UI should show `Selected: No controllable objects` |
| 2026-05-13 | Moving platform test | Pressing `Space` on the selected moving platform moves it between points | Not tested in Unity yet | Pending Play Mode test | Tune endpoint offset and speed if platform feels unclear |
| 2026-05-13 | Moving platform carry test | Walker standing on the platform is carried safely | Not tested in Unity yet | Pending Play Mode test | `Carry Walker On Top` should be enabled |
| 2026-05-13 | Rotating platform test | `Q/E` rotates selected bridge smoothly by 90 degrees | Not tested in Unity yet | Pending Play Mode test | Verify local rotation axis is `(0, 0, 1)` |
| 2026-05-13 | Switch and door test | Walker or selected switch opens the linked door | Not tested in Unity yet | Pending Play Mode test | Drag `DoorController3D` into switch activation targets |
| 2026-05-13 | Hazard fail test | Touching hazard stops the walker and shows fail panel with clear reason | Not tested in Unity yet | Pending Play Mode test | Default reason is `The walker hit a hazard.` |
| 2026-05-13 | Exit win test | Reaching exit stops the walker and shows `Level Complete` | Not tested in Unity yet | Pending Play Mode test | Check exit trigger size and placement |
| 2026-05-13 | UI text test | Objective, selected object, controls, tutorial hint, win, fail, and pause text are readable | Not tested in Unity yet | Pending UI setup | Use Canvas Scaler and high-contrast panels |
| 2026-05-13 | Pause and restart test | `Esc` pauses during gameplay; `R` restarts; after win/fail, `Esc` returns to menu | Not tested in Unity yet | Pending Play Mode test | Confirm scene names in `GameManager` and Build Settings |
| 2026-05-13 | Player understanding test | A new player understands that the walker moves automatically and the world is controlled instead | Not tested with player yet | Needs playtester | Observe first playthrough and improve tutorial/UI wording if needed |

## Testing Evidence To Collect

- Screenshot of clean Unity Console after import.
- Screenshot of selected object highlight.
- Screenshot of moving platform or rotating bridge in use.
- Screenshot of switch opening the door.
- Screenshot of fail panel.
- Screenshot of `Level Complete` panel.
- Short gameplay recording showing a full route from start to exit.

## Known Testing Risks

- Moving platform carry behaviour may need tuning after Unity physics testing.
- Door collider disabling should be checked carefully so the walker can pass only when intended.
- Trigger zones need fair sizes: large enough to work, but not so large that they feel unfair.
- UI may need resizing for laptop screens if text overlaps.
- The current project supplies scripts and setup instructions; the actual Unity scene still needs to be assembled and tested manually.
