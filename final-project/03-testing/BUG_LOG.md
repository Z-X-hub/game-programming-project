# Stage 3A Bug Log

Updated: 2026-06-03

| ID | Area | Problem | Fix / Status |
| --- | --- | --- | --- |
| BUG-3A-001 | UI flow | Generated pause/end UI could remain active and block clicks when screens changed. | Fixed by attaching generated UI to the screen content parent and toggling generated panels with screen state. |
| BUG-3A-002 | End feedback | End screen did not clearly explain why the run ended or how many coins were earned in the run. | Fixed by adding death reason, score, high score, new-record status, current-run coins, and total coins. |
| BUG-3A-003 | Gameplay HUD | Player could not clearly see current-run coin progress while playing. | Fixed by adding a live in-game coin stats HUD. |
| BUG-3A-004 | Terrain cleanup | `TerrainGenerator.Remove()` could hit a null reference when a generated block reference was already destroyed. | Fixed by removing blocks by dictionary key and safely cleaning null entries. |

## Still To Test

- Confirm restart and home buttons after several play/death cycles.
- Confirm the in-game coin HUD updates during longer runs and after restart.
- Confirm the end screen displays the correct death reason for water, spikes, saws, maces, and falling.
