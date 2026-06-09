# Stage 3 Bug Log

Updated: 2026-06-09

| ID | Area | Problem | Fix / Status |
| --- | --- | --- | --- |
| BUG-3A-001 | UI flow | Generated pause/end UI could remain active and block clicks when screens changed. | Fixed by attaching generated UI to the screen content parent and toggling generated panels with screen state. |
| BUG-3A-002 | End feedback | End screen did not clearly explain why the run ended or how many coins were earned in the run. | Fixed by adding death reason, score, high score, new-record status, current-run coins, and total coins. |
| BUG-3A-003 | Gameplay HUD | Player could not clearly see current-run coin progress while playing. | Fixed by adding a live in-game coin stats HUD. |
| BUG-3A-004 | Terrain cleanup | `TerrainGenerator.Remove()` could hit a null reference when a generated block reference was already destroyed. | Fixed by removing blocks by dictionary key and safely cleaning null entries. |
| BUG-3D-001 | Character animation | The fast `PLAYER` became visibly larger when switching from idle to the new run animation. | Fixed by regenerating the run frames at the same `80x110` canvas size as the idle/stand sprites, limiting the run-frame transparent height to about `88-96px`, and keeping runner scale fixed in `KenneyCharacterVisual.cs`. |

## Stage 3D Regression Checks

| Check | Result |
| --- | --- |
| Confirmed staged source contains the high-frame PLAYER run sequence. | Passed: `85` run PNG frames and `85` matching `.meta` files are present under `Assets/Resources/FoxDash/KenneyCharacters/Player/Run`. |
| Confirmed the runtime visual script matches the local project. | Passed: `KenneyCharacterVisual.cs` matches the local Fox Dash source used for the latest Unity test. |
| Confirmed no new gameplay code was required after the sprite-size fix. | Passed: local script differences are only encoding/Unity folder metadata noise, so no unnecessary code churn was uploaded for Stage 3D. |
| Confirmed the Unity C# project still compiles. | Passed: `dotnet build Assembly-CSharp.csproj --no-restore` completed with `0` warnings and `0` errors on 2026-06-09. |

## Final Manual Checks Before Submission

These checks remain useful before the final hand-in, but they are not blocking
the specific `BUG-3D-001` sprite-size fix.

- Confirm restart and home buttons after several play/death cycles.
- Confirm the in-game coin HUD updates during longer runs and after restart.
- Confirm the end screen displays the correct death reason for water, spikes, saws, maces, and falling.
- Confirm the fast `PLAYER` looks the same approximate size when transitioning between idle, run, jump, and roll during a real Unity Play Mode run.
