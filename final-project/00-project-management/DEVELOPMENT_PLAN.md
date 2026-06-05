# Fox Dash Development Plan

This plan follows the module assessment structure and breaks the work into smaller evidence stages. The repository should show how Fox Dash developed through planning, implementation, testing, reflection, and professional project management.

Presentation/demo preparation is intentionally deferred for now. The current focus is the game, testing evidence, report evidence, and professionalism.

## Assessment Breakdown

- Game Concept and Design: 20%
- Final Game: 60%
  - Game and Report: 40%
  - Game Demo / Presentation: 20% (deferred for now)
- Professionalism: 20%

## Current Evidence Status

```text
Stage 1 - Game Concept and Design: complete
Stage 2 - Core Playable Game source evidence: complete
Stage 3A - Playable stability and controls testing: testing
Stage 3B - Character ability and balance testing: active
Stage 3C-3D - Level iteration and bug evidence: next
Stage 4A-4C - Final report evidence: planned after testing
Professionalism - ongoing
Presentation - deferred
```

## Stage 1 - Game Concept and Design (Complete)

Goal: define the game idea, intended player experience, realistic scope, design reasoning, tools, assets, and relevant legal, ethical, accessibility, and security considerations.

Sub-stages:

- Stage 1A: clear game idea and intended player experience.
- Stage 1B: design principles, creativity, originality, and realistic scope.
- Stage 1C: tools, assets, credits, legal, accessibility, ethics, and security planning.

Evidence:

- `01-concept-design/GAME_CONCEPT.md`
- `01-concept-design/CHARACTER_DESIGN.md`
- `01-concept-design/SCOPE_TOOLS_ASSETS.md`
- `01-concept-design/LEGAL_ACCESSIBILITY_SECURITY.md`
- Issue #13

Status: complete.

## Stage 2 - Core Playable Game Source Evidence (Complete)

Goal: record visible development of the playable Unity vertical slice through staged source-code commits.

Completed source stages:

- Stage 2A: base Unity runtime source.
- Stage 2B: runner world gameplay systems.
- Stage 2C: character selection source.
- Stage 2D: character ability implementation.
- Stage 2E: character visual and UI polish source.

Evidence:

- `02-game-development/README.md`
- `02-game-development/SOURCE_MANIFEST.md`
- `02-game-development/RUN_INSTRUCTIONS.md`
- `02-game-development/IMPLEMENTATION_NOTES.md`
- `02-game-development/UPLOAD_NOTES.md`
- `02-game-development/CODE_STAGING_PLAN.md`
- `02-game-development/source/FoxDash/`
- Issue #14

Status: complete.

## Stage 3 - Testing And Iteration (Active)

Goal: prove that the game is playable, tested, debugged, and improved over time.

Sub-stages:

- Stage 3A: playable stability and controls testing. Issue #15.
- Stage 3B: character ability and balance testing. Issue #18.
- Stage 3C: level flow, difficulty, and feedback iteration. Issue #19.
- Stage 3D: bug fixing and regression record. Issue #20.

Current evidence:

- `03-testing/PLAYTEST_LOG.md`
- `03-testing/BUG_LOG.md`
- `03-testing/BEFORE_AFTER_CHANGES.md`
- `03-testing/BALANCE_NOTES.md`

Planned evidence:

- `03-testing/STABILITY_CHECKLIST.md`
- `03-testing/KNOWN_LIMITATIONS.md`

Status: Stage 3A testing; Stage 3B active.

## Stage 4 - Report Evidence (Planned)

Goal: write the final report from actual game and testing evidence, not from planned features only.

Sub-stages:

- Stage 4A: design and technical decisions. Issue #16.
- Stage 4B: testing, limitations, and reflection. Issue #21.
- Stage 4C: legal, accessibility, and final completeness check. Issue #22.

Planned evidence:

- `04-report/REPORT_DRAFT.md`
- `04-report/REPORT_TESTING_REFLECTION.md`
- `04-report/FINAL_REPORT_CHECKLIST.md`

Status: planned after Stage 3 evidence.

## Professionalism (Ongoing)

Goal: show consistent, organised, responsible development through GitHub.

Evidence:

- `00-project-management/KANBAN.md`
- `00-project-management/ASSESSMENT_PROCESS_BREAKDOWN.md`
- README files
- staged commits
- GitHub Issues
- credits and third-party notices
- AI declaration
- Issue #23

Status: ongoing.

## Deferred - Demo / Presentation

Issue #17 is kept as deferred. It should be prepared later, after testing evidence and report evidence are stronger.

## Professional GitHub Practice

- Commit work in meaningful stages.
- Keep the Kanban updated whenever stage status changes.
- Keep documentation close to the evidence it describes.
- Do not commit Unity generated cache folders such as `Library`, `Temp`, `Logs`, or `UserSettings`.
- Use the repository to show steady development, not only the final result.
