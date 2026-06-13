# Stage 3 Known Limitations

Updated: 2026-06-13

This file records honest limitations that should be discussed in the final
report. These do not block the current vertical slice, but they show awareness
of what could be improved with more time.

## Limitations

| Area | Limitation | Impact | Possible Future Improvement |
| --- | --- | --- | --- |
| Playtest sample size | External feedback was gathered from David, Zane, and Ken, but each tester only played a short session. | Balance conclusions are suitable for a student vertical slice, but not statistically strong. | Run longer sessions and record score/coin/death results for each role. |
| Level balance | Platform spacing, hazard density, and coin placement have not been tuned from a large set of player results. | Some sections may feel easier or harder depending on selected character. | Record repeated runs for each role and adjust block/hazard frequency. |
| Windows build evidence | The macOS standalone build has been exported and uploaded, but a Windows build is not included because the local Unity install only has macOS standalone support. | Assessors on macOS can run the submitted app directly; assessors on Windows may need to run from Unity source unless a Windows build is produced later. | Install Unity Windows Build Support and export `FoxDash_Final_Build_Windows.zip` if cross-platform marking is required. |
| Presentation evidence | The live presentation has been completed and supporting materials are recorded in `05-presentation/`. | The repository now includes presentation support evidence, but no optional video recording is included. | Add a short gameplay recording later if required by a future submission method. |
| Accessibility | Controls and character differences are documented, but there are no advanced accessibility settings. | Basic clarity is present; deeper accessibility is limited. | Add remappable controls, larger text mode, or audio/visual feedback options. |

## Resolved Before Final Evidence

| Area | Improvement Made | Result |
| --- | --- | --- |
| Audio file size | `Assets/Sounds/Enemies/Water.wav` was compressed in place from about `85 MB` to about `9.8 MB`, while keeping the same file path and Unity `.meta` GUID. | The repository is smaller and the previous GitHub large-file warning is no longer expected for this asset. |
| macOS build evidence | `FoxDash_Final_Build_Mac.zip` was exported from the Unity GUI and uploaded as GitHub Release evidence. | The repository now has a downloadable packaged build for final marking/demo evidence. |

## Report Use

These limitations should be used in Stage 4B and Stage 4C so the final report is
honest about what was tested, what changed because of testing, and what would be
improved after the submitted vertical slice.
