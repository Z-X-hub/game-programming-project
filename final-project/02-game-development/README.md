# Stage 2 - Core Playable Game

Status: documentation complete; staged source-code upload in progress.

Updated: 2026-06-02

This folder records the current Fox Dash Unity vertical slice. Stage 2 focuses on the playable game implementation: source structure, main scene, character selection, gameplay systems, run instructions, and staged source-code upload.

## Stage 2 Evidence

- `SOURCE_MANIFEST.md` - Unity project source structure, included folders, excluded generated folders, and key script index.
- `RUN_INSTRUCTIONS.md` - how to open and run the game in Unity.
- `IMPLEMENTATION_NOTES.md` - implemented gameplay systems and key script responsibilities.
- `UPLOAD_NOTES.md` - upload approach, generated-folder exclusions, and large-file notes.
- `CODE_STAGING_PLAN.md` - planned staged source-code upload order.

## Current Correction

The Stage 2 documentation is complete, but the actual Unity source code still needs to be uploaded in staged code commits. The source code should not be committed as one final dump.

Planned source-code stages:

```text
Code Stage 2A - base Unity runtime source
Code Stage 2B - runner world gameplay systems
Code Stage 2C - character selection source
Code Stage 2D - character ability implementation
Code Stage 2E - character visual and UI polish source
```

## Current Playable Slice

The current Fox Dash build includes:

- main menu with Fox Dash branding and character selection
- three playable character roles
- `PLAYER` with faster movement
- `SOLDIER` with one automatic revive after falling or landing in water
- `ADVENTURER` with double jump
- Kenney character visual integration and movement animation tuning
- generated platform-runner flow
- coins, chests, hazards, water, enemies, score, UI, audio, and restart flow

## Unity Project

Local project folder used for Stage 2:

```text
/Users/zhuxuan/Downloads/FoxDash
```

Unity version:

```text
Unity 2022.3.62f3c1
```

Main scene:

```text
Assets/Scenes/Play.unity
```

## What Should Not Be Uploaded

Unity generated cache folders are intentionally excluded because they are not source files and should not be committed:

```text
Library/
Temp/
Logs/
UserSettings/
.vscode/
*.csproj
*.sln
*.slnx
```

## Next Action

Re-authenticate GitHub locally, then upload the Unity source code in the staged order recorded in `CODE_STAGING_PLAN.md`.
