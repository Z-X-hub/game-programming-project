# Stage 2 - Core Playable Game

Status: complete.

Updated: 2026-06-02

This folder records the current Fox Dash Unity vertical slice. Stage 2 focuses on the playable game implementation: source structure, main scene, character selection, gameplay systems, run instructions, and staged source-code upload.

## Stage 2 Evidence

- `SOURCE_MANIFEST.md` - Unity project source structure, included folders, excluded generated folders, and key script index.
- `RUN_INSTRUCTIONS.md` - how to open and run the game in Unity.
- `IMPLEMENTATION_NOTES.md` - implemented gameplay systems and key script responsibilities.
- `UPLOAD_NOTES.md` - upload approach, generated-folder exclusions, and large-file notes.
- `CODE_STAGING_PLAN.md` - completed staged source-code upload order.
- `source/FoxDash/` - staged source-code files uploaded in development-order commits.

## Completed Source Upload Stages

```text
Stage 2A - base Unity runtime source
Stage 2B - runner world gameplay systems
Stage 2C - character selection source
Stage 2D - character ability implementation
Stage 2E - character visual and UI polish source
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

## What Was Not Uploaded

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

## Next Stage

Stage 3 will add testing and iteration evidence after playtesting sessions: bugs found, fixes made, before/after notes, and a final stability checklist.
