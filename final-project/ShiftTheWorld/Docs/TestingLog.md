# Shift the World - Testing Log

This log should be updated during Unity playtesting. It is structured to show evidence of testing, debugging, and improvement for coursework marking.

## Test Case Table

| Date | Test | Expected Result | Actual Result | Issue Found | Fix / Improvement |
| --- | --- | --- | --- | --- | --- |
| 2026-05-10 | Scripts imported into Unity | Scripts compile without errors | Not tested in Unity yet | Unity scene not assembled | Create Unity project and run first compile test |
| 2026-05-10 | Auto-walker side-view movement | Walker moves along X only and does not drift on Z | Not tested in Unity yet | Pending | Add Rigidbody constraints and verify in Play Mode |
| 2026-05-10 | Object selection | `A/D` or arrow keys cycle through controllable objects | Not tested in Unity yet | Pending | Verify selected highlight and UI text |
| 2026-05-10 | Moving platform | Selected platform moves between two points when activated | Not tested in Unity yet | Pending | Tune movement speed and endpoint positions |
| 2026-05-10 | Rotating platform | `Q/E` rotates selected block smoothly by 90 degrees | Not tested in Unity yet | Pending | Tune rotation speed and axis |
| 2026-05-10 | Switch and door | Walker or selected switch opens the door | Not tested in Unity yet | Pending | Link switch activation targets in Inspector |
| 2026-05-10 | Hazard fail state | Walker touching hazard shows fail panel | Not tested in Unity yet | Pending | Check trigger collider and GameManager reference |
| 2026-05-10 | Exit win state | Walker reaching exit shows win panel | Not tested in Unity yet | Pending | Check trigger collider and exit placement |
| 2026-05-10 | Restart | Pressing `R` reloads the level | Not tested in Unity yet | Pending | Add Level01 to Build Settings |
| 2026-05-11 | Static code review for Stage 3 | Core scripts remain focused and compile-ready for Unity import | Reviewed locally, not Unity-compiled yet | Unity Editor not run in this environment | Refined movement stopping, UI selection refresh, switch safety, door physics timing, and platform carry behaviour |
| 2026-05-11 | Main Menu buttons | Play opens Level Select and Quit exits a built player | Not tested in Unity yet | Pending | Connect buttons to `SceneLoader` in Unity |
| 2026-05-11 | Level Select buttons | Level 01 opens `Level01` and Back returns to Main Menu | Not tested in Unity yet | Pending | Connect buttons to `SceneLoader` in Unity |
| 2026-05-11 | Pause UI | `Esc` opens pause panel, Resume continues, Restart reloads, Main Menu returns | Not tested in Unity yet | Pending | Connect pause panel buttons to `UIManager` |
| 2026-05-11 | Win/Fail UI | Correct result panel appears with useful message and buttons | Not tested in Unity yet | Pending | Assign Win/Fail panels and message text references |

## Planned Iteration Evidence

During testing, record changes such as:

- Platform speed adjustments
- Rotating block placement changes
- Hazard size or position changes
- UI wording improvements
- Camera size and framing improvements
- Input or selection readability improvements

## Known Testing Risks

- Moving platforms may need tuning to carry the walker reliably.
- Door collider disabling should be tested to ensure the walker can pass after the door opens.
- Trigger zones need clear collider sizes so fail/win states feel fair.
- Moving platform carry behaviour should be tested with the walker standing fully on top of the platform.
