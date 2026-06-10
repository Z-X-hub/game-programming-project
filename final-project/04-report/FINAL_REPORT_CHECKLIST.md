# Stage 4C Final Report Checklist

Updated: 2026-06-10

This checklist maps the report evidence against the module assessment
requirements.

## Game Concept And Design

- [x] Clear game idea: Fox Dash is a 2D platform runner vertical slice.
- [x] Player goal explained: survive, collect coins, avoid hazards, improve score.
- [x] Core loop explained: choose role, run, jump/roll, collect, avoid, restart.
- [x] Original design contribution explained: three-role character system.
- [x] Realistic scope explained: small playable vertical slice, not a large unfinished game.
- [x] Tools and assets explained: Unity, RedRunner reference, Kenney character assets.
- [x] Legal/accessibility/security considerations documented.

Evidence:

```text
final-project/01-concept-design/
final-project/04-report/FINAL_REPORT.md
```

## Final Game And Technical Evidence

- [x] Main Unity project uploaded under `final-project/FoxDash/`.
- [x] Main scene documented as `Assets/Scenes/Play.unity`.
- [x] Key systems documented: input, game flow, UI, character logic, animation, terrain, hazards, collectables.
- [x] Character selection and ability implementation documented.
- [x] Build/run instructions documented.
- [x] Build/export evidence file added.
- [x] macOS standalone build exported and linked through GitHub Release.
- [x] Generated Unity folders excluded from source upload.

Evidence:

```text
final-project/FoxDash/
final-project/02-game-development/
final-project/02-game-development/BUILD_EVIDENCE.md
```

## Testing, Debugging, And Improvement

- [x] Playtest log included.
- [x] Stability checklist included.
- [x] Final Unity editor pre-submission check completed.
- [x] External playtest entries from David, Zane, and Ken recorded.
- [x] Character balance notes included.
- [x] Level flow notes included.
- [x] Bug log included.
- [x] Before/after changes included.
- [x] Known limitations included.
- [x] Local build check recorded with `0 warnings` and `0 errors`.

Evidence:

```text
final-project/03-testing/
final-project/04-report/REPORT_TESTING_REFLECTION.md
```

## Legal, Ethical, Accessibility, Security, And AI Use

- [x] RedRunner reference and MIT License noted.
- [x] Kenney character asset source noted.
- [x] Other bundled third-party references noted.
- [x] AI assistance declared selectively in `AI_DECLARATION.md`.
- [x] Accessibility considerations documented.
- [x] Security risk is low because the game is offline/local, but local save/input considerations are documented.

Evidence:

```text
final-project/01-concept-design/LEGAL_ACCESSIBILITY_SECURITY.md
final-project/FoxDash/THIRD_PARTY_NOTICES.md
final-project/FoxDash/AI_DECLARATION.md
```

## Professionalism

- [x] Class exercises and final project are separated.
- [x] Old `ShiftTheWorld` folder removed from the current final-project tree.
- [x] Current final game source is clearly uploaded as `final-project/FoxDash/`.
- [x] GitHub issues and Kanban show staged development.
- [x] README files explain project structure.
- [x] `AGENTS.md` is excluded because it is a local assistant maintenance file, not submission evidence.
- [x] Large `Water.wav` asset compressed from about `85 MB` to about `9.8 MB`.
- [x] Final professionalism audit issue completed.
- [x] Demo script prepared.

Evidence:

```text
final-project/00-project-management/
final-project/README.md
README.md
final-project/05-presentation/DEMO_SCRIPT.md
```

## Still To Finish

- [ ] Live demo / presentation rehearsal.
- [ ] Optional Windows standalone build if Windows Build Support is installed later.
