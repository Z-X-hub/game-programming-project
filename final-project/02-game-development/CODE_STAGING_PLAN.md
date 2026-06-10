# Stage 2 Source Code Staging Plan

Updated: 2026-06-02

The source code was uploaded in staged groups so the GitHub history shows a realistic development process instead of one final dump.

## Status

```text
Complete
```

## Completed Source Upload Stages

### Code Stage 2A - Base Unity Runtime Skeleton

Goal: upload the core runtime scripts that make the runner project start, score, and manage screens.

Commit:

```text
53a5ec3a7f7a2e1ca02e7b72ae67b15fc808e541
```

Committed files include:

```text
Assets/Scripts/FoxDash/GameManager.cs
Assets/Scripts/FoxDash/UIManager.cs
Assets/Scripts/FoxDash/AudioManager.cs
Assets/Scripts/FoxDash/Characters/Character.cs
Assets/Scripts/Utils/Property.cs
Assets/Scripts/Utils/PropertyEvent.cs
Assets/Scripts/Utils/UtilsUI.cs
Packages/manifest.json
Packages/packages-lock.json
ProjectSettings/ProjectVersion.txt
ProjectSettings/EditorBuildSettings.asset
ProjectSettings/ProjectSettings.asset
ProjectSettings/InputManager.asset
```

### Code Stage 2B - Runner World Systems

Goal: upload terrain generation, collectables, enemies, camera, and shared runtime utilities.

Commit:

```text
c3cb0eb899199b7de32f77aae2308d0989ff86e6
```

Committed files include:

```text
Assets/Scripts/FoxDash/TerrainGeneration/
Assets/Scripts/FoxDash/Collectables/
Assets/Scripts/FoxDash/Enemies/
Assets/Scripts/FoxDash/ObjectPool/
Assets/Scripts/FoxDash/Skeleton/
Assets/Scripts/FoxDash/Utilities/
Assets/Scripts/CameraControl.cs
```

### Code Stage 2C - Character Selection

Goal: upload the code that introduces selectable character roles and connects the menu to the playable character.

Commit:

```text
b02bac92951d8e5a3fdf5975e92d5baeb65d4324
```

Committed files:

```text
Assets/Scripts/FoxDash/Characters/PlayerCharacterSelection.cs
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
```

### Code Stage 2D - Three Character Abilities

Goal: upload the main character implementation showing fast movement, SOLDIER one-time revive, and ADVENTURER double jump.

Commit:

```text
c18155784c1e8cfeb90e1205452aedb75e957410
```

Committed file:

```text
Assets/Scripts/FoxDash/Characters/FoxDashCharacter.cs
```

### Code Stage 2E - Character Visuals And UI Polish

Goal: upload the visual adaptation and remaining UI scripts used for the playable vertical slice.

Commit:

```text
aaca69d84b36bc3ad70a3ef0e57d8f58c5d8025a
```

Committed files include:

```text
Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs
Assets/Scripts/FoxDash/AudioControl/VolumeControl.cs
Assets/Scripts/FoxDash/UI/
```

## Source Location In Repository

The staged source code is stored under:

```text
final-project/02-game-development/source/FoxDash/
```

## Files That Were Not Uploaded

Generated Unity/editor files were not committed:

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

## Notes

The staged upload focuses on source code and source metadata. Large runtime assets should still be reviewed carefully before any future full asset-level sync.
