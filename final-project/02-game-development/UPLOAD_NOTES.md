# Stage 2 Upload Notes

Updated: 2026-06-02

## Upload Goal

Stage 2 records the playable Unity vertical slice and shows that the project has moved from planning into implementation.

The uploaded evidence focuses on:

- Unity project structure
- main scene and run instructions
- key gameplay systems
- character role implementation
- source-file mapping
- generated-folder exclusions

## Local Clean Package

A clean source package was prepared locally for verification:

```text
/tmp/FoxDash-stage2-source.zip
```

Package size:

```text
96 MB
```

The package was built from:

```text
/Users/zhuxuan/Downloads/FoxDash
```

Excluded paths:

```text
FoxDash/Library/*
FoxDash/UserSettings/*
FoxDash/Logs/*
FoxDash/.vscode/*
FoxDash/*.csproj
FoxDash/*.sln
FoxDash/*.slnx
FoxDash/obj/*
FoxDash/Temp/*
FoxDash/Build/*
FoxDash/Builds/*
```

## Why Generated Files Are Excluded

Unity rebuilds `Library`, temporary build caches, IDE project files, logs, and local user settings automatically. These files are not source evidence and make GitHub repositories noisy and hard to review.

## Repository Evidence Uploaded

Instead of committing generated archives, this stage uploads readable evidence files:

```text
final-project/02-game-development/README.md
final-project/02-game-development/SOURCE_MANIFEST.md
final-project/02-game-development/RUN_INSTRUCTIONS.md
final-project/02-game-development/IMPLEMENTATION_NOTES.md
final-project/02-game-development/UPLOAD_NOTES.md
```

## GitHub CLI Note

The local `gh` authentication is currently invalid, so normal `git push` from the local Unity folder is not available until GitHub authentication is refreshed. The current Stage 2 upload was therefore completed through the GitHub connector using repository documents and source manifest evidence.

If a full asset-level project sync is required later, refresh GitHub authentication and push the clean Unity source tree while keeping generated folders excluded.
