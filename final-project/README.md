# Final Project: Fox Dash

**Fox Dash** is my final Game Programming module project. It replaces the earlier working title **Rotating World**.

The project is uploaded in stages so the repository clearly shows planning, development, testing, reflection, and professional working practice over time.

All non-presentation repository evidence is complete. Presentation/demo preparation remains deferred for now.

## Current Stage

```text
Stage 1 - Game Concept and Design: complete
Stage 2 - Core Playable Game source evidence: complete
Stage 3A - Playable stability and controls testing: complete
Stage 3B - Character ability and balance testing: complete
Stage 3C - Level iteration and feedback evidence: complete
Stage 3D - Bug fixing and regression record: complete
Stage 4A-4C - Report evidence: complete
Professionalism - final repository evidence audit: complete
Presentation - deferred
```

## Folder Structure

```text
final-project/
|-- README.md
|-- FoxDash/
|   |-- Assets/
|   |-- Packages/
|   |-- ProjectSettings/
|   |-- README.md
|   |-- PROJECT_STRUCTURE.md
|   |-- THIRD_PARTY_NOTICES.md
|   `-- AI_DECLARATION.md
|-- 00-project-management/
|   |-- ASSESSMENT_PROCESS_BREAKDOWN.md
|   |-- DEVELOPMENT_PLAN.md
|   |-- FINAL_REPOSITORY_AUDIT.md
|   `-- KANBAN.md
|-- 01-concept-design/
|   |-- GAME_CONCEPT.md
|   |-- CHARACTER_DESIGN.md
|   |-- SCOPE_TOOLS_ASSETS.md
|   `-- LEGAL_ACCESSIBILITY_SECURITY.md
|-- 02-game-development/
|   |-- README.md
|   |-- SOURCE_MANIFEST.md
|   |-- RUN_INSTRUCTIONS.md
|   |-- IMPLEMENTATION_NOTES.md
|   |-- UPLOAD_NOTES.md
|   |-- CODE_STAGING_PLAN.md
|   `-- source/FoxDash/
|-- 03-testing/
|   |-- README.md
|   |-- PLAYTEST_LOG.md
|   |-- BUG_LOG.md
|   |-- BEFORE_AFTER_CHANGES.md
|   |-- BALANCE_NOTES.md
|   |-- STABILITY_CHECKLIST.md
|   |-- LEVEL_FLOW_NOTES.md
|   `-- KNOWN_LIMITATIONS.md
|-- 04-report/
|   |-- README.md
|   |-- REPORT_DRAFT.md
|   |-- REPORT_TESTING_REFLECTION.md
|   `-- FINAL_REPORT_CHECKLIST.md
`-- 05-presentation/
    `-- README.md
```

## Assessment Mapping

| Assessment Area | Repository Evidence |
| --- | --- |
| Game Concept and Design | `01-concept-design/`, Issue #13 |
| Final Game / Playable Build | `02-game-development/`, Issue #14 |
| Testing and Improvement | `03-testing/`, Issues #15 and #18-#20 |
| Report | `04-report/`, Issues #16 and #21-#22 |
| Professionalism | `00-project-management/`, commit history, README files, Issue #23 |
| Demo / Presentation | `05-presentation/`, Issue #17, deferred for now |

## Game Summary

Fox Dash is a 2D Unity platform runner. The player chooses one of three characters, runs through generated platform sections, collects coins and chests, avoids hazards, and tries to survive for as long as possible.

## Character Roles

| Character | Ability | Purpose |
| --- | --- | --- |
| `PLAYER` | Faster movement | Speed-focused and riskier play style |
| `SOLDIER` | One automatic revive after falling or landing in water | More forgiving route for less experienced players |
| `ADVENTURER` | Double jump | More flexible movement and recovery |

## Unity Project

Local project folder used for development:

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

Run instructions are in:

```text
final-project/02-game-development/RUN_INSTRUCTIONS.md
```

Source code is uploaded under:

```text
final-project/FoxDash/
```

The earlier `ShiftTheWorld` prototype folder has been removed from the current
final-project tree. Historical commits still show the previous prototype work,
but the active final game source is now Fox Dash.

## Current Focus

All non-presentation evidence is complete. The remaining deferred stage is the
demo/presentation, which should use the completed planning, source, testing,
report, and professionalism evidence as supporting material.
