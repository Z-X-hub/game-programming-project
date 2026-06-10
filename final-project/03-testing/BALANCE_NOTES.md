# Stage 3B Character Ability And Balance Notes

Updated: 2026-06-09

This document records the first Stage 3B pass for checking whether the three playable roles feel meaningfully different and whether their abilities support the runner game loop.

Status: complete for Stage 3B repository evidence.

## Test Focus

Stage 3B is linked to Issue #18.

Assessment focus:

- appropriate use of game systems such as input, movement, collision, UI, animation, and feedback
- meaningful character choice
- balanced player experience
- evidence that character behaviour has been checked, not only implemented

## Character Ability Summary

| Character | Ability | Code / Design Evidence | Balance Intention |
| --- | --- | --- | --- |
| `PLAYER` | Faster movement | `FoxDashCharacter.ApplyRoleStats()` applies the runner speed multiplier. Current role multipliers are `PLAYER 1.2`, `SOLDIER 0.9`, and `ADVENTURER 1.0`. | Higher score potential, but harder reaction timing. |
| `SOLDIER` | One automatic revive | `TryUseSoldierRevive()` and `ReviveFromFall()` consume the revive once and continue near the failure point. | More forgiving for falls/water without adding a separate ability button. |
| `ADVENTURER` | Double jump | `FoxDashCharacter.Jump()` allows two jumps only for the adventurer role. | Better recovery from gaps and awkward platform spacing. |

## Initial Checks

| Check | Expected Result | Current Result / Note | Status |
| --- | --- | --- | --- |
| Character selection persists | Selected role should be stored before the run starts. | `PlayerCharacterSelection` stores the chosen role with `PlayerPrefs`. | Pass by code review |
| `PLAYER` speed identity | Player role should feel faster than the other two roles. | Runner multiplier is higher than soldier/adventurer; speed-trail feedback and high-frame run animation support the fast identity. | Pass for Stage 3B |
| `SOLDIER` revive identity | Soldier should revive once after falling/water instead of dying immediately. | Revive is consumed once and resumes near the death position with a grace timer. The previous `E` shield design has been removed from the player-facing role description. | Pass for Stage 3B |
| `ADVENTURER` jump identity | Adventurer should allow two jumps before landing. | Jump counter allows two jumps for the adventurer role and one jump for other roles. | Pass for Stage 3B |
| Menu clarity | Player should understand the character differences before starting. | Home menu quick guide lists faster run, one revive, and double jump. | Pass by UI review |
| Compile stability | Stage 3B should not introduce script errors. | Local `dotnet build Assembly-CSharp.csproj --no-restore` completed with `0 warnings` and `0 errors` on 2026-06-09. | Pass |

## Balance Observations So Far

- `PLAYER` should remain the high-risk/high-reward option because the faster speed can increase score but makes hazards harder to react to.
- `SOLDIER` is useful as the safer option because the automatic revive does not require the player to remember another control.
- `ADVENTURER` gives the most flexible platforming recovery, so future level testing should check that double jump does not make gaps too easy.
- The three roles are now explained on the home screen, which supports fairness and accessibility because the player can make an informed choice.

## Stage 3B Balance Conclusion

- The three characters now have distinct enough identities for the vertical slice.
- `PLAYER` is the high-risk/high-reward choice because it is faster.
- `SOLDIER` is the safest choice because the one-time revive reduces frustration after fall/water mistakes.
- `ADVENTURER` is the movement-control choice because double jump gives better recovery.
- The abilities are explained before play, which supports fairness and accessibility.
- The balance is suitable for a small assessed vertical slice; deeper numeric tuning can be described as future work.

## Remaining Risks For Final Polish

- The soldier revive should be checked separately for falling, water, and enemy/hazard death cases.
- The adventurer double jump should be checked after restart/home flows to confirm the jump counter resets correctly.
- The fast player should be checked in longer runs to decide whether the speed multiplier feels exciting rather than unfair.
- Character animation feel should still be watched during the final Unity editor run.

## Stage 3B Sign-Off

- Stage 3B is complete for GitHub evidence.
- Open risks are moved into the final report/limitations discussion rather than blocking the current testing phase.
