# Stage 2 Source Manifest

Updated: 2026-06-02

This manifest records the Fox Dash Unity source structure used for the Stage 2 playable vertical slice.

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

## Clean Source Check

A clean source package was prepared locally with generated folders excluded.

```text
Clean package path: /tmp/FoxDash-stage2-source.zip
Clean package size: 96 MB
Source files after exclusions: 1527
Key text/source files counted: 221
```

The clean package excludes Unity cache folders and generated IDE files. This repository records the Stage 2 evidence through readable manifests, implementation notes, and run instructions.

## Included Source Areas

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
Assets/Scripts/FoxDash/Characters/RedCharacter.cs
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
| Fast PLAYER role | `RedCharacter.cs`, `KenneyCharacterVisual.cs` |
| SOLDIER one-time revive | `RedCharacter.cs` |
| ADVENTURER double jump | `RedCharacter.cs` |
| Kenney character visuals | `KenneyCharacterVisual.cs`, `RedCharacter.cs` |
| Main menu layout | `StartScreen.cs` |
| Game flow and scoring | `GameManager.cs`, `UIManager.cs` |
| Generated platform runner | `TerrainGenerator.cs`, `DefaultTerrainGenerator.cs`, `TerrainGenerationSettings.cs` |
| Collectibles and hazards | `Collectables/`, `Enemies/` |

## Large Asset Note

The local project contains a large water sound asset:

```text
Assets/Sounds/Enemies/Water.wav - about 85 MB
```

This explains why the clean source package is close to GitHub's normal single-file warning threshold. The file is treated as an asset/source dependency for the Unity project, but generated archives are not committed as repository evidence.
