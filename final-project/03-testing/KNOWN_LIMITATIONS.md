# Stage 3 Known Limitations

Updated: 2026-06-10

This file records honest limitations that should be discussed in the final
report. These do not block the current vertical slice, but they show awareness
of what could be improved with more time.

## Limitations

| Area | Limitation | Impact | Possible Future Improvement |
| --- | --- | --- | --- |
| Playtest sample size | Testing evidence is based on short development checks and user-observed Unity editor runs. An external playtest template has been added, but real classmates still need to fill it in. | Balance conclusions are suitable for a student vertical slice, but not statistically strong. | Ask two or three players to try the game and record score/coin/death results in Session 4 of `PLAYTEST_LOG.md`. |
| Level balance | Platform spacing, hazard density, and coin placement have not been tuned from a large set of player results. | Some sections may feel easier or harder depending on selected character. | Record repeated runs for each role and adjust block/hazard frequency. |
| Build evidence | Local C# compilation passes and a Unity build attempt is documented, but final standalone export was blocked by local Unity license activation. | The project is source-complete, but packaged build evidence still needs final confirmation. | Export a final build from an activated Unity Editor and update `BUILD_EVIDENCE.md` with the ZIP/release link. |
| Presentation evidence | A demo script is prepared, but the live presentation rehearsal is still deferred. | The speaking structure is ready, but the final live demonstration still needs rehearsal. | Use `05-presentation/DEMO_SCRIPT.md` for the final presentation. |
| Accessibility | Controls and character differences are documented, but there are no advanced accessibility settings. | Basic clarity is present; deeper accessibility is limited. | Add remappable controls, larger text mode, or audio/visual feedback options. |

## Resolved Before Final Evidence

| Area | Improvement Made | Result |
| --- | --- | --- |
| Audio file size | `Assets/Sounds/Enemies/Water.wav` was compressed in place from about `85 MB` to about `9.8 MB`, while keeping the same file path and Unity `.meta` GUID. | The repository is smaller and the previous GitHub large-file warning is no longer expected for this asset. |

## Report Use

These limitations should be used in Stage 4B and Stage 4C so the final report is
honest about what was tested, what changed because of testing, and what would be
improved after the submitted vertical slice.
