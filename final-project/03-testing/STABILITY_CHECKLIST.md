# Stage 3A Stability And Controls Checklist

Updated: 2026-06-10

Stage 3A records whether the Fox Dash vertical slice is stable enough to be
played and explained. It focuses on controls, scene flow, UI flow, and obvious
runtime risks.

## Evidence Type

- Local source review of the current `final-project/FoxDash/` Unity project.
- Earlier Unity editor observations from the development/playtest process.
- Local C# build check using the generated Unity project files.

## Build Check

Command:

```bash
dotnet build Assembly-CSharp.csproj --no-restore
```

Result on 2026-06-09:

```text
0 warnings
0 errors
```

Final C# validation after build-evidence and audio-cleanup changes on
2026-06-10:

```bash
dotnet build Assembly-CSharp.csproj --no-restore
dotnet restore Assembly-CSharp-Editor.csproj
dotnet build Assembly-CSharp-Editor.csproj --no-restore
```

Result:

```text
0 warnings
0 errors
```

## Checklist

| Area | Expected Behaviour | Evidence / Result | Status |
| --- | --- | --- | --- |
| Project source | The final Unity project should be visible in the repository. | Current project source is uploaded at `final-project/FoxDash/`. | Pass |
| Main scene | The game should have a clear scene entry point. | Main scene is documented as `Assets/Scenes/Play.unity`. | Pass |
| Menu flow | Player should start from a home screen, choose a character, then press play. | `StartScreen.cs` handles character cards, title, quick guide, and play button flow. | Pass |
| Controls | Player should understand movement, jump, roll, and pause. | Menu guide and run instructions document movement, jump, roll, and pause inputs. | Pass |
| Character selection | Player role should persist into the run. | `PlayerCharacterSelection.cs` stores the role with `PlayerPrefs`. | Pass |
| Pause flow | Player should be able to pause and choose resume, restart, or home. | `PauseScreen.cs` creates resume/restart/home actions and hides inactive generated panels. | Pass |
| End flow | Player should be able to restart or return home after death. | `EndScreen.cs` creates restart/home actions and displays run feedback. | Pass |
| Score feedback | Player should see run progress. | Score UI is retained and end screen reports score/high score. | Pass |
| Coin feedback | Player should see coin progress during and after a run. | In-game HUD and end screen show current-run coins and total coins. | Pass |
| Death feedback | Player should understand why the run ended. | `GameManager` records death reason; water, spikes, saws, and maces set specific reasons. | Pass |
| Terrain cleanup | Generated blocks should not crash the game when removed. | `TerrainGenerator.Remove()` removes by dictionary keys and safely handles missing block references. | Pass |
| Generated folders | Unity cache files should not be uploaded. | `Library`, `Temp`, `Logs`, `UserSettings`, `.vscode`, IDE files, and `AGENTS.md` are excluded. | Pass |

## Stage 3A Result

Stage 3A is complete for repository evidence. The project has a clear playable
entry point, documented controls, start/pause/end flow, score and coin feedback,
and a successful local build check.

## Final Unity Editor Pre-submission Check

Date prepared: 2026-06-10

Unity version: `2022.3.62f3c1`

Scene: `Assets/Scenes/Play.unity`

Status: completed in the activated Unity GUI.

An automated command-line Unity run was attempted on 2026-06-10, but the local
Unity installation stopped at license activation before entering the editor.
The final pre-submission check was therefore completed manually in the Unity
GUI after opening the project.

| Check | Result |
| --- | --- |
| Project opens in Unity | Pass |
| Play scene loads | Pass |
| Main menu appears | Pass |
| `PLAYER` can be selected and played | Pass |
| `SOLDIER` revive works once | Pass |
| `ADVENTURER` double jump works | Pass |
| Coins update during gameplay | Pass |
| Hazards trigger death reason | Pass |
| End screen shows score, coins, death reason | Pass |
| Restart button works | Pass |
| Home button works | Pass |
| Console errors | 0 errors |

This checklist confirms that the final Unity editor pass was completed before
the macOS build was packaged.
