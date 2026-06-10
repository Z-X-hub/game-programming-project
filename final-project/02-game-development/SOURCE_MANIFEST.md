# Stage 2 Source Manifest

Updated: 2026-06-09

This manifest records the Fox Dash Unity source structure used for the Stage 2 playable vertical slice.

## Current Repository Source Location

The current complete Unity source project is uploaded under:

```text
final-project/FoxDash/
```

This is the folder to open in Unity for the final Fox Dash project.

## Earlier Staged Source Evidence

The staged source code is uploaded under:

```text
final-project/02-game-development/source/FoxDash/
```

This older nested folder is kept as development-process evidence from the staged
source upload. The current final project source is `final-project/FoxDash/`.

## Local Unity Project

```text
/Users/zhuxuan/Downloads/FoxDash
```

Unity editor:

```text
Unity 2022.3.62f3c1
```

Main scene:

```text
Assets/Scenes/Play.unity
```

## Completed Source Commit Stages

```text
53a5ec3a7f7a2e1ca02e7b72ae67b15fc808e541 - Stage 2A: base Unity runtime source
c3cb0eb899199b7de32f77aae2308d0989ff86e6 - Stage 2B: runner world gameplay systems
b02bac92951d8e5a3fdf5975e92d5baeb65d4324 - Stage 2C: character selection source
c18155784c1e8cfeb90e1205452aedb75e957410 - Stage 2D: character ability implementation
aaca69d84b36bc3ad70a3ef0e57d8f58c5d8025a - Stage 2E: character visual and UI polish source
```

## Clean Source Check

A clean source package was prepared locally with generated folders excluded.

```text
Clean package path: /tmp/FoxDash-stage2-source.zip
Clean package size: 96 MB
Source files after exclusions: 1527
Key text/source files counted: 221
```

## Uploaded Source Areas

```text
source/FoxDash/Assets/Scripts/
source/FoxDash/Assets/Resources/FoxDash/KenneyCharacters/
source/FoxDash/Packages/
source/FoxDash/ProjectSettings/
```

The staged upload focuses on readable source code, lightweight character visual resources, and text source metadata. It does not include every large runtime asset.

## Included Local Source Areas

```text
Assets/Animations/
Assets/Editor/
Assets/Fonts/
Assets/Materials/
Assets/Physics Materials 2D/
Assets/Prefabs/
Assets/Resources/
Assets/SaveGameFree/
Assets/Scenes/
Assets/Scripts/
Assets/Shaders/
Assets/Sounds/
Assets/Sprites/
Assets/Standard Assets/
Assets/ThirdParty/
Packages/
ProjectSettings/
README.md
PROJECT_STRUCTURE.md
THIRD_PARTY_NOTICES.md
AI_DECLARATION.md
```

Editor helper scripts include `FoxDashProjectBranding.cs`,
`FoxDashPlayModeLauncher.cs`, and `FoxDashBuildCommand.cs`.

## Excluded Generated Areas

These are intentionally not source evidence:

```text
Library/
Temp/
Logs/
UserSettings/
.vscode/
*.csproj
*.sln
*.slnx
AGENTS.md
obj/
Build/
Builds/
```

## Important Script Index

```text
Assets/Scripts/FoxDash/GameManager.cs
Assets/Scripts/FoxDash/UIManager.cs
Assets/Scripts/FoxDash/AudioManager.cs
Assets/Scripts/FoxDash/Characters/Character.cs
Assets/Scripts/FoxDash/Characters/FoxDashCharacter.cs
Assets/Scripts/FoxDash/Characters/PlayerCharacterSelection.cs
Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
Assets/Scripts/FoxDash/UI/UIScreen/InGameScreen.cs
Assets/Scripts/FoxDash/UI/UIScreen/EndScreen.cs
Assets/Scripts/FoxDash/TerrainGeneration/TerrainGenerator.cs
Assets/Scripts/FoxDash/TerrainGeneration/DefaultTerrainGenerator.cs
Assets/Scripts/FoxDash/TerrainGeneration/TerrainGenerationSettings.cs
Assets/Scripts/FoxDash/TerrainGeneration/Block.cs
Assets/Scripts/FoxDash/Collectables/Coin.cs
Assets/Scripts/FoxDash/Collectables/Chest.cs
Assets/Scripts/FoxDash/Enemies/Enemy.cs
Assets/Scripts/FoxDash/Enemies/Water.cs
Assets/Scripts/FoxDash/Enemies/Saw.cs
Assets/Scripts/FoxDash/Enemies/Spike.cs
Assets/Scripts/FoxDash/ObjectPool/ObjectPool.cs
Assets/Scripts/FoxDash/Utilities/CameraController.cs
Assets/Scripts/FoxDash/Utilities/GroundCheck.cs
```

## Character-Feature Source Mapping

| Feature | Main Source Files |
| --- | --- |
| Character selection | `PlayerCharacterSelection.cs`, `StartScreen.cs` |
| Fast PLAYER role | `FoxDashCharacter.cs`, `KenneyCharacterVisual.cs` |
| SOLDIER one-time revive | `FoxDashCharacter.cs` |
| ADVENTURER double jump | `FoxDashCharacter.cs` |
| Kenney character visuals | `KenneyCharacterVisual.cs`, `FoxDashCharacter.cs` |
| Main menu layout | `StartScreen.cs` |
| Game flow and scoring | `GameManager.cs`, `UIManager.cs` |
| Generated platform runner | `TerrainGenerator.cs`, `DefaultTerrainGenerator.cs`, `TerrainGenerationSettings.cs` |
| Collectibles and hazards | `Collectables/`, `Enemies/` |

## Large Asset Note

The local project contains a large water sound asset:

```text
Assets/Sounds/Enemies/Water.wav - compressed to about 9.8 MB
```

This asset was reduced from the earlier large source file before final evidence
cleanup. The same filename and Unity `.meta` GUID were kept so existing Unity
references do not need to change.

## Stage 3C/3D Animation Resource Update

Updated source/resource evidence:

```text
source/FoxDash/Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs
source/FoxDash/Assets/Resources/FoxDash/KenneyCharacters/
```

Reason:

- The `PLAYER` fast-run role now uses an 85-frame run animation sequence.
- The run sprites were corrected to match the existing `80x110` character canvas size.
- The lightweight Kenney character resource folder is included so the animation fix is visible in repository evidence, not only described in documentation.
