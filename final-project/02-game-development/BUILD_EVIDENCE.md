# Final Build Evidence

Updated: 2026-06-10

Unity version: `2022.3.62f3c1`
Build platform target: macOS standalone
Main scene: `Assets/Scenes/Play.unity`
Build evidence date: 2026-06-10

## Build Result

The final standalone build export was attempted from the local Unity project,
but it could not be completed by command line in this environment because the
Unity Editor license was not available for automated execution.

Observed Unity log result:

```text
No valid Unity Editor license found. Please activate your license.
```

This means the repository currently contains source, run instructions, testing
evidence, and a documented build attempt, but the final exported ZIP still
needs to be produced manually from an activated Unity Editor before final
submission.

## Build Attempt

Attempted project:

```text
/Users/zhuxuan/Downloads/FoxDash
```

Attempted command:

```bash
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -quit \
  -projectPath /Users/zhuxuan/Downloads/FoxDash \
  -executeMethod FoxDash.EditorTools.FoxDashBuildCommand.BuildMac \
  -logFile /Users/zhuxuan/Downloads/FoxDash/BuildLogs/FoxDash_Final_Build_Mac.log
```

A second non-batch attempt was also tried, but it stopped at the same Unity
license check.

## Compilation Validation

After adding the build helper and compressing the water audio asset, local C#
project compilation was checked again on 2026-06-10:

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

## Manual Export Steps

To complete this evidence before hand-in:

1. Open `/Users/zhuxuan/Downloads/FoxDash` in Unity `2022.3.62f3c1`.
2. Open `Assets/Scenes/Play.unity`.
3. Go to `File > Build Settings`.
4. Select macOS or Windows as the target platform.
5. Add `Assets/Scenes/Play.unity` to Scenes In Build if it is not already listed.
6. Export the standalone build.
7. Compress the result as `FoxDash_Final_Build_Mac.zip` or `FoxDash_Final_Build_Windows.zip`.
8. Add the final ZIP to the submission package or GitHub Release.
9. Update the Build Link section below.

## Final Run Test Checklist

This checklist should be completed after the exported app is opened:

| Check | Result |
| --- | --- |
| Game opened successfully | Pending exported build |
| Main menu displayed correctly | Pending exported build |
| Character selection worked for `PLAYER` | Pending exported build |
| Character selection worked for `SOLDIER` | Pending exported build |
| Character selection worked for `ADVENTURER` | Pending exported build |
| Player could enter gameplay scene | Pending exported build |
| Jump, roll, movement, coins, hazards, restart, and home button worked | Pending exported build |
| No console errors appeared during final test | Pending exported build |

## Build Link

GitHub Release / submitted ZIP: pending after manual Unity export.
