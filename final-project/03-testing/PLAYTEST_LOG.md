# Stage 3A Playtest Log

Updated: 2026-06-03

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
- Remaining warnings are Unity/package serialized-field warnings already present in the project style.

Status:

- Stage 3A remains in progress because more hands-on playtesting is still needed.
