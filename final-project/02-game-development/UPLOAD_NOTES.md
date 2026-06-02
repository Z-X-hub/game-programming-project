# Stage 2 Upload Notes

Updated: 2026-06-02

## Upload Goal

Stage 2 records the playable Unity vertical slice and shows that the project has moved from planning into implementation.

The uploaded evidence covers:

- Unity project structure
- main scene and run instructions
- key gameplay systems
- character role implementation
- source-file mapping
- generated-folder exclusions
- staged source-code upload history

## Source-Code Upload Completed

The actual Unity source code was uploaded in staged commits, not as one final dump.

Source location:

```text
final-project/02-game-development/source/FoxDash/
```

Completed source commits:

```text
53a5ec3a7f7a2e1ca02e7b72ae67b15fc808e541 - Stage 2A: add Fox Dash base Unity runtime source
c3cb0eb899199b7de32f77aae2308d0989ff86e6 - Stage 2B: add runner world gameplay systems
b02bac92951d8e5a3fdf5975e92d5baeb65d4324 - Stage 2C: add character selection source
c18155784c1e8cfeb90e1205452aedb75e957410 - Stage 2D: add character ability implementation
aaca69d84b36bc3ad70a3ef0e57d8f58c5d8025a - Stage 2E: add character visual and UI polish source
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

## Repository Evidence Uploaded

```text
final-project/02-game-development/README.md
final-project/02-game-development/SOURCE_MANIFEST.md
final-project/02-game-development/RUN_INSTRUCTIONS.md
final-project/02-game-development/IMPLEMENTATION_NOTES.md
final-project/02-game-development/UPLOAD_NOTES.md
final-project/02-game-development/CODE_STAGING_PLAN.md
final-project/02-game-development/source/FoxDash/
```

## Large Asset Note

The staged source upload focuses on code and text source metadata. Large runtime assets should be reviewed carefully before any future full asset-level sync.
