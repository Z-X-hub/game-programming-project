# Stage 2 Source Code Staging Plan

Updated: 2026-06-02

The source code should not be uploaded as one final dump. It should be committed in staged groups so the GitHub history shows a realistic development process.

## Current Correction

Stage 2 documentation has been uploaded, but the actual Unity source code still needs to be committed in staged source-code batches. The Kanban has been corrected to show this work as in progress.

## Planned Source Upload Stages

### Code Stage 2A - Base Unity Runtime Skeleton

Goal: upload the core runtime scripts that make the runner project start, score, and manage screens.

Planned files:

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
```

Suggested commit message:

```text
Stage 2A: add Fox Dash base Unity runtime source
```

### Code Stage 2B - Runner World Systems

Goal: upload terrain generation, collectables, enemies, camera, and shared runtime utilities.

Planned files:

```text
Assets/Scripts/FoxDash/TerrainGeneration/
Assets/Scripts/FoxDash/Collectables/
Assets/Scripts/FoxDash/Enemies/
Assets/Scripts/FoxDash/ObjectPool/
Assets/Scripts/FoxDash/Skeleton/
Assets/Scripts/FoxDash/Utilities/
Assets/Scripts/CameraControl.cs
```

Suggested commit message:

```text
Stage 2B: add runner world gameplay systems
```

### Code Stage 2C - Character Selection

Goal: upload the code that introduces selectable character roles and connects the menu to the playable character.

Planned files:

```text
Assets/Scripts/FoxDash/Characters/PlayerCharacterSelection.cs
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
```

Suggested commit message:

```text
Stage 2C: add character selection source
```

### Code Stage 2D - Three Character Abilities

Goal: upload the main character implementation showing fast movement, SOLDIER one-time revive, and ADVENTURER double jump.

Planned files:

```text
Assets/Scripts/FoxDash/Characters/RedCharacter.cs
```

Suggested commit message:

```text
Stage 2D: add character ability implementation
```

### Code Stage 2E - Character Visuals And UI Polish

Goal: upload the visual adaptation and remaining UI scripts used for the playable vertical slice.

Planned files:

```text
Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs
Assets/Scripts/FoxDash/UI/
```

Suggested commit message:

```text
Stage 2E: add character visual and UI polish source
```

## Files That Should Not Be Uploaded

Do not commit generated Unity/editor files:

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

## Important Note

The local GitHub CLI authentication is currently invalid. A full staged source upload should be completed with normal Git after re-authenticating GitHub, because normal Git can preserve clean multi-file commits better than connector-only file uploads.

Recommended command after authentication:

```text
gh auth login -h github.com
```

Then stage and commit the files in the order above.
