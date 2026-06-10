# Stage 4B Testing, Difficulties, And Reflection

Updated: 2026-06-10

This file records problems encountered during development and testing, what was
changed, and what was learned. The aim is to show the process behind the final
game, not only the final result.

## Testing Evidence Used

Main evidence files:

```text
final-project/03-testing/PLAYTEST_LOG.md
final-project/03-testing/STABILITY_CHECKLIST.md
final-project/03-testing/BALANCE_NOTES.md
final-project/03-testing/LEVEL_FLOW_NOTES.md
final-project/03-testing/BUG_LOG.md
final-project/03-testing/BEFORE_AFTER_CHANGES.md
final-project/03-testing/KNOWN_LIMITATIONS.md
```

## Difficulties And Changes

| Area | Difficulty Found | Change Made | Reflection |
| --- | --- | --- | --- |
| Character expansion | Adding new characters first broke parts of the original runner setup and caused the game to be difficult to play normally. | Removed incorrect added-character content, restored the runner foundation, then rebuilt the feature as a cleaner three-role selection system. | I learned that adding features directly into an existing Unity project can break references, so I needed a smaller staged approach. |
| SOLDIER ability | The original active shield idea using `E` did not fit the simple runner controls, and the revive behaviour initially did not feel right because it should continue near the death point. | Replaced the active shield with one automatic revive. Added revive state, revive consumption, Rigidbody2D reset, grace time, and continuation near the failure position. | A simpler automatic ability was better for the player experience than a more complicated control. |
| ADVENTURER double jump | The double jump needed to work only for ADVENTURER, without giving every character two jumps. | Used role checks and jump-count logic in `FoxDashCharacter.cs`. | This made the ability clear and avoided changing the base movement rules for all characters. |
| PLAYER speed identity | PLAYER was faster in code, but the animation did not clearly communicate speed at first. Some early running animations had unnatural timing. | Added faster role feedback, speed trails, and a high-frame run sequence for PLAYER. | A role ability needs visual feedback, not only different numbers. |
| PLAYER sprite size bug | After importing a high-frame run sequence, PLAYER appeared larger while running than while standing still. | Regenerated run frames to match the existing `80x110` sprite canvas and kept scale fixed in `KenneyCharacterVisual.cs`. | Asset size consistency is important in Unity because even a good animation can look wrong if frame dimensions do not match. |
| UI overlap | Pause and end UI could overlap or stay active in a way that made buttons hard to click. | Generated pause/end panels were attached to the correct screen roots and toggled with screen state. Added clear restart/home actions. | UI state needs to be tested through full scene flow, not only by checking one screen. |
| Death feedback | Early end feedback did not explain enough about why the run ended or how well the player did. | Added death reason, score, high score, new-record state, current-run coins, and total coins. | Feedback after failure helps the player understand the game and want to restart. |
| Coin feedback | Coins were collectable, but current-run coin progress was not visible enough during play. | Added live in-game coin statistics. | Rewards are more meaningful when the player can see progress immediately. |
| Terrain cleanup | Generated terrain cleanup had a null-reference risk when generated blocks were already destroyed or missing. | Changed cleanup to remove by dictionary key and safely handle null entries. | Object lifetime in generated levels needs defensive code, especially when objects can be destroyed. |
| Unity compatibility | Unity 2022 produced compatibility issues such as old font lookup behaviour. | Replaced old built-in font assumptions with compatible runtime handling where needed. | Updating Unity versions can expose assumptions from older projects. |
| GitHub structure | The repository still contained an old `ShiftTheWorld` final-project folder, which made the final project unclear. | Removed `final-project/ShiftTheWorld/`, uploaded the current complete Fox Dash project under `final-project/FoxDash/`, and updated README files. | Repository organisation is part of professionalism because the teacher should quickly understand what is current. |
| Large asset warning | GitHub warned that `Water.wav` was about `84.95 MB`, above the recommended 50 MB size. | Compressed the same `Water.wav` asset in place to about `9.8 MB`, keeping the file path and Unity `.meta` GUID. | Large assets should be reviewed before upload; compression can improve professionalism without changing gameplay. |
| Standalone build export | Command-line Unity export was attempted first, but the local Unity installation stopped at license activation. | Exported the macOS build manually through the activated Unity GUI, packaged it as `FoxDash_Final_Build_Mac.zip`, and linked it in `BUILD_EVIDENCE.md`. | Build evidence should be honest: the failed command-line attempt is recorded, and the final successful GUI export is documented separately. |
| External playtest evidence | Testing was mainly self-directed and based on local checks. | Recorded external feedback from David, Zane, and Ken, with each tester trying a different character role. | The role design was validated because each tester described a different play style and difficulty profile. |

## What Changed Because Of Testing

Testing changed the project in these concrete ways:

- The home screen now explains controls and character differences.
- The game has a clearer pause/restart/home loop.
- End screen feedback is more useful.
- Coin feedback is visible during gameplay.
- Terrain cleanup is more robust.
- PLAYER run animation is smoother and no longer changes character size.
- Character abilities are documented as balanced roles rather than isolated
  features.
- External playtest evidence now supports the three-role design.
- The macOS standalone build is packaged and linked as release evidence.
- The GitHub repository now points clearly to Fox Dash as the final project.

## What I Would Improve Next

With more time, I would:

- collect repeated score/coin/death data for each character
- tune platform gaps and hazard frequency from player results
- add more accessibility options such as larger text or remappable controls
- export a Windows standalone build if Windows Build Support is installed later
- rehearse the demo using the prepared demo script

## Reflection

The main lesson from this project is that a small game still needs careful
iteration. Many problems did not appear as syntax errors; they appeared as
player-experience problems, such as unclear feedback, UI overlap, or animation
that felt wrong.

The final project improved because I tested the game in smaller stages, recorded
problems, and changed features based on what made the game clearer and more
playable.
