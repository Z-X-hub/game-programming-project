# Stage 2 Upload Notes

Updated: 2026-06-02

## Upload Goal

Stage 2 records the playable Unity vertical slice and should show that the project has moved from planning into implementation.

The uploaded documentation currently covers:

- Unity project structure
- main scene and run instructions
- key gameplay systems
- character role implementation
- source-file mapping
- generated-folder exclusions
- staged source-code upload plan

## Source-Code Upload Correction

The actual Unity source code should be uploaded in staged commits, not as one final dump. The repository should therefore treat Stage 2 source upload as **in progress** until the staged source commits are completed.

The staged upload order is documented in:

```text
final-project/02-game-development/CODE_STAGING_PLAN.md
```

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

## Repository Evidence Uploaded So Far

The current repository evidence includes readable documentation:

```text
final-project/02-game-development/README.md
final-project/02-game-development/SOURCE_MANIFEST.md
final-project/02-game-development/RUN_INSTRUCTIONS.md
final-project/02-game-development/IMPLEMENTATION_NOTES.md
final-project/02-game-development/UPLOAD_NOTES.md
final-project/02-game-development/CODE_STAGING_PLAN.md
```

## GitHub CLI Note

The local `gh` authentication is currently invalid, so normal staged `git push` from the local Unity folder is not available until GitHub authentication is refreshed.

Recommended next command:

```text
gh auth login -h github.com
```

After authentication is fixed, commit the clean Unity source tree in the staged order from `CODE_STAGING_PLAN.md` while keeping generated folders excluded.
