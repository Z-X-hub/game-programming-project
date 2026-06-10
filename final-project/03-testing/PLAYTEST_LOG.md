# Stage 3 Playtest Log

Updated: 2026-06-10

This log records small testing and stability progress for the playable Fox Dash slice.

## Session 1 - UI Flow And Basic Feedback Check

Date: 2026-06-03

Focus:

- home-menu guidance
- pause/restart/home flow
- end-screen feedback
- in-game coin feedback
- obvious runtime errors during the Unity editor check

Observed issues:

- The run could open into a layered end/pause UI state where buttons were hard to click.
- The end screen did not clearly show enough run feedback for the player.
- The player could see total coins, but not a clear current-run coin count during play.
- Unity editor logs showed a `TerrainGenerator.Remove()` null-reference risk when generated terrain references were already destroyed.

Changes made:

- Added a short home-menu quick guide for goal, controls, and character differences.
- Added clearer pause actions: resume, restart, and home.
- Added end-screen feedback for death reason, score, high score, new-record status, coins this run, and total coins.
- Added an in-game coin HUD showing current-run coins and total coins.
- Fixed generated UI visibility so inactive screens do not block clicks.
- Hardened terrain removal so null generated-block references are cleaned safely.

Validation:

- `dotnet restore Assembly-CSharp.csproj`
- `dotnet build Assembly-CSharp.csproj --no-restore`

Result:

- Build completed with `0 errors`.
- A later build check on 2026-06-09 completed with `0 warnings` and `0 errors`.

Status:

- Stage 3A is complete for repository evidence. A final quick Unity editor run is still recommended before hand-in as part of the overall final check.

## Session 2 - PLAYER Run Animation Visual Consistency Check

Date: 2026-06-08

Focus:

- fast `PLAYER` run animation
- visual scale consistency between idle and running
- evidence that visual polish changes were checked instead of only added

Observed issues:

- The first imported run sequence had too few frames and looked choppy.
- The higher-frame reference video gave a better running motion, but early exported sprites used a larger `96x128` canvas.
- During Play Mode observation, the character looked noticeably larger while running than while standing still.

Changes made:

- Extracted the full 85-frame run sequence from the reference video.
- Re-exported the run frames to `80x110`, matching the existing `player_idle.png` and `player_stand.png` canvas size.
- Compared transparent sprite bounds: idle/stand height is about `96px`; the corrected run frames now stay around `88-96px`.
- Updated `KenneyCharacterVisual.cs` so the run sequence uses fixed `BaseVisualScale` instead of extra squash/stretch scaling.

Validation:

- Counted the exported run frames: `player_run_01.png` to `player_run_85.png`.
- Checked sample sprite bounds for idle, stand, and run frames.
- Ran `dotnet build Assembly-CSharp.csproj --no-restore`.

Result:

- Build completed with `0 warnings` and `0 errors` locally on 2026-06-08.
- The sprite-size mismatch is recorded as `BUG-3D-001` in `BUG_LOG.md`.

Status:

- Animation polish has improved, but final confirmation should still be done inside Unity after asset reimport.

## Session 3 - Stage 3A Stability And Controls Sign-Off

Date: 2026-06-09

Focus:

- confirm the project has enough evidence for playable stability and control flow
- check that documented controls match the menu guidance
- check that the game source still compiles after repository cleanup and full Fox Dash source upload

Checks recorded:

- Main project source is now present in `final-project/FoxDash/`.
- Main scene remains `Assets/Scenes/Play.unity`.
- Menu guidance explains the runner goal, character differences, jump, roll, movement, and pause input.
- Pause and end screens include restart/home flow so the player is not trapped after a failed run.
- In-game HUD and end-screen feedback now include coin/score result information.
- Local build command completed successfully:

```bash
dotnet build Assembly-CSharp.csproj --no-restore
```

Result:

- Build completed with `0 warnings` and `0 errors` on 2026-06-09.
- Stage 3A is considered complete as testing documentation evidence.
- Any remaining Unity-editor-only confirmation is moved to the final completeness check rather than blocking Stage 3A.

## Session 4 - External Playtest Record

Date: 2026-06-10

Testers: David, Zane, Ken

Focus: controls, role clarity, difficulty, feedback, restart flow, and whether
the three roles feel meaningfully different.

| Tester | Character Used | Observation | Change Made / Decision |
| --- | --- | --- | --- |
| David | `PLAYER` | Felt the speed was suitable. The faster movement gives room for player skill, because maintaining speed and timing jumps can help clear harder obstacle combinations. | Kept `PLAYER` as the faster role. The role supports a higher-risk play style instead of simply making the game easier. |
| Zane | `SOLDIER` | Felt this role is suitable for players who are less confident with movement timing. The one-time revive gives another chance to continue and overcome a mistake. | Kept the revive automatic rather than requiring an extra key, because it supports learning and recovery. |
| Ken | `ADVENTURER` | Felt the double jump depends strongly on player control. To pass obstacles well, the player must use the second jump at the right time instead of pressing it randomly. | Kept `ADVENTURER` as the skill-based movement role and kept the menu explanation for `2X JUMP`. |

## Session 4 Questions

- Did the player understand the goal without extra explanation?
- Did the player understand the selected character ability?
- Did the player notice coins and score feedback?
- Did the player understand why they died?
- Did restart and home flow feel clear?
- Which character felt easiest or hardest?

## Session 4 Summary

The external playtest supported the role design. David's feedback confirmed
that the faster character can create skill expression through timing and speed
control. Zane's feedback confirmed that the revive character gives a safer
option for players who are still learning. Ken's feedback confirmed that the
double-jump character is more demanding and rewards careful air control.

No major ability redesign was needed after this session. The main decision was
to keep the three-character structure because each tester described a different
play style.
