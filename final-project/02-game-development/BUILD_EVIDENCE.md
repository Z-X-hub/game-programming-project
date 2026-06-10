# Final Build Evidence

Updated: 2026-06-10

Unity version: `2022.3.62f3c1`
Build platform target: macOS standalone
Main scene: `Assets/Scenes/Play.unity`
Build evidence date: 2026-06-10

## Build Result

The final macOS standalone build was exported successfully from an activated
Unity Editor and packaged as:

```text
FoxDash_Final_Build_Mac.zip
```

The exported app metadata was checked after packaging:

```text
Product name: Fox Dash
Bundle identifier: com.zhuxuan.foxdash
App icon: Fox Dash icon, not the earlier RedRunner icon
```

The Windows standalone build is not included in this evidence because the
available local Unity installation only has `MacStandaloneSupport` installed.
The project source and build settings still support exporting Windows if the
Unity `Windows Build Support` module is installed later.

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

A second non-batch attempt was also tried and stopped at the same Unity license
check. The final macOS build was therefore exported manually through the Unity
GUI after the editor was opened and activated.

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

## Manual Export Steps Used

The submitted macOS build was produced with these steps:

1. Open `/Users/zhuxuan/Downloads/FoxDash` in Unity `2022.3.62f3c1`.
2. Open `Assets/Scenes/Play.unity`.
3. Go to `File > Build Settings`.
4. Select macOS as the target platform.
5. Add `Assets/Scenes/Play.unity` to Scenes In Build if it is not already listed.
6. Apply Fox Dash project branding and icon settings.
7. Export the standalone build.
8. Compress the result as `FoxDash_Final_Build_Mac.zip`.
9. Upload the ZIP as GitHub Release evidence.

## Final Run Test Checklist

This checklist should be completed after the exported app is opened:

| Check | Result |
| --- | --- |
| Game opened successfully | Pass |
| Main menu displayed correctly | Pass |
| Character selection worked for `PLAYER` | Pass |
| Character selection worked for `SOLDIER` | Pass |
| Character selection worked for `ADVENTURER` | Pass |
| Player could enter gameplay scene | Pass |
| Jump, roll, movement, coins, hazards, restart, and home button worked | Pass |
| No console errors appeared during final test | Pass |

## Build Link

GitHub Release / submitted ZIP: [FoxDash_Final_Build_Mac.zip](https://github.com/Z-X-hub/game-programming-project/releases/tag/foxdash-final-macos-build)
