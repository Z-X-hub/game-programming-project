# Stage 3 Known Limitations

Updated: 2026-06-09

This file records honest limitations that should be discussed in the final
report. These do not block the current vertical slice, but they show awareness
of what could be improved with more time.

## Limitations

| Area | Limitation | Impact | Possible Future Improvement |
| --- | --- | --- | --- |
| Playtest sample size | Testing evidence is based on short development checks and user-observed Unity editor runs, not a large external playtest group. | Balance conclusions are suitable for a student vertical slice, but not statistically strong. | Ask two or three players to try the game and record score/coin/death results. |
| Level balance | Platform spacing, hazard density, and coin placement have not been tuned from a large set of player results. | Some sections may feel easier or harder depending on selected character. | Record repeated runs for each role and adjust block/hazard frequency. |
| Audio file size | `Assets/Sounds/Enemies/Water.wav` is about `84.95 MB`. GitHub accepts it, but it is above the recommended 50 MB size. | Repository is larger than ideal. | Compress or replace the water sound before a final distribution build. |
| Build evidence | Local C# compilation passes, but a final exported standalone build is not yet recorded in the repository. | The project is source-complete, but build packaging still needs final confirmation. | Export a final build and document platform/build settings. |
| Presentation evidence | Demo/presentation preparation is still deferred. | Does not affect current Stage 3 testing, but must be done before presentation assessment. | Prepare a short demo script and screenshots after report evidence is complete. |
| Accessibility | Controls and character differences are documented, but there are no advanced accessibility settings. | Basic clarity is present; deeper accessibility is limited. | Add remappable controls, larger text mode, or audio/visual feedback options. |

## Report Use

These limitations should be used in Stage 4B and Stage 4C so the final report is
honest about what was tested, what changed because of testing, and what would be
improved after the submitted vertical slice.

