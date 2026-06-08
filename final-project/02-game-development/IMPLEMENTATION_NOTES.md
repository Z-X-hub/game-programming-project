# Fox Dash Implementation Notes

Updated: 2026-06-08

This document records the main playable systems implemented for the Fox Dash vertical slice.

## Main Game Flow

Main files:

```text
Assets/Scripts/FoxDash/GameManager.cs
Assets/Scripts/FoxDash/UIManager.cs
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
Assets/Scripts/FoxDash/UI/UIScreen/InGameScreen.cs
Assets/Scripts/FoxDash/UI/UIScreen/EndScreen.cs
```

Implemented flow:

- load into the main scene
- show the Fox Dash home screen
- allow character selection before starting
- start the runner gameplay
- update score and UI during the run
- detect death and open the end/restart flow

## Character Selection

Main files:

```text
Assets/Scripts/FoxDash/Characters/PlayerCharacterSelection.cs
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
Assets/Scripts/FoxDash/Characters/RedCharacter.cs
```

Implemented behaviour:

- stores selected role with `PlayerPrefs`
- supports three roles: `Runner`, `Knight`, and `Monkey`
- displays them to the player as `PLAYER`, `SOLDIER`, and `ADVENTURER`
- applies the selected role to the playable character before gameplay starts

## Character Abilities

Main file:

```text
Assets/Scripts/FoxDash/Characters/RedCharacter.cs
```

Implemented abilities:

- `PLAYER`: faster run-speed multiplier for a speed-focused play style
- `SOLDIER`: one automatic revive after falling or landing in water
- `ADVENTURER`: double jump by allowing two jumps before landing

Key design decision:

The older active shield idea was removed. The `SOLDIER` role now uses automatic one-time revive because it fits the runner loop better and avoids adding extra control complexity.

## SOLDIER Revive Logic

Relevant methods:

```text
TryUseKnightRevive(...)
ReviveFromFall(...)
Die(...)
Reset(...)
ApplyRole(...)
```

Implemented behaviour:

- revive is available once per run for the `SOLDIER` role
- revive is consumed before normal death when possible
- Rigidbody2D velocity and angular velocity are reset after revive
- skeleton/death visual state is disabled after revive
- a short grace timer prevents immediate repeated death after revive
- revive continues near the death position instead of fully restarting the run

## ADVENTURER Double Jump

Relevant method:

```text
Jump()
```

Implemented behaviour:

- grounded jumps reset the jump counter
- `ADVENTURER` can jump twice before landing
- other roles are limited to one jump
- jump particles, animation trigger, and audio feedback are preserved

## Character Visuals And Animation

Main files:

```text
Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs
Assets/Scripts/FoxDash/Characters/RedCharacter.cs
```

Implemented behaviour:

- loads Kenney-style character sprites from project resources
- keeps character scale consistent with the runner scene
- uses role-specific visuals and tinting
- adds running/speed-trail effect for the fast role
- tunes ground movement pose so PLAYER feels faster without making the step rate unnaturally high
- uses jump/fall/roll/hurt sprite fallbacks where needed

Stage 3 animation polish:

- `PLAYER` now has an optional high-frame run sequence under `Resources/FoxDash/KenneyCharacters/Player/Run`.
- `KenneyCharacterVisual.cs` loads sequential `player_run_XX` frames when available.
- Long run sequences are played at the original video-like frame rate so the fast role feels smoother.
- The run frames were regenerated to match the idle/stand sprite canvas size, fixing a visual bug where the character appeared larger while running.

## Menu And UI Updates

Main file:

```text
Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs
```

Implemented behaviour:

- Fox Dash home-screen identity
- character cards on the start screen
- keyboard selection with `1`, `2`, `3`
- removal/cleanup of old social or platform-style buttons from the main screen
- clearer layout so buttons do not cover the background too heavily

## Terrain, Collectibles, Hazards, And Score

Main files:

```text
Assets/Scripts/FoxDash/TerrainGeneration/
Assets/Scripts/FoxDash/Collectables/
Assets/Scripts/FoxDash/Enemies/
Assets/Scripts/FoxDash/UI/UIScoreText.cs
Assets/Scripts/FoxDash/UI/UICoinText.cs
```

Implemented behaviour:

- generated platform-runner sections
- coins and chests as collectables
- hazards including water, spikes, saws, and enemies
- score based on forward progress
- UI feedback for score and collected coins

## Stage 2 Acceptance Checklist

- [x] Playable Unity scene identified.
- [x] Character selection exists on the main menu.
- [x] Three character roles are implemented.
- [x] PLAYER feels faster than the other roles.
- [x] SOLDIER revive replaces the older shield design.
- [x] ADVENTURER double jump remains implemented.
- [x] Main menu layout and branding are updated.
- [x] Generated runner gameplay, collectibles, hazards, score, and restart flow are documented.
- [x] Unity generated cache folders are excluded from repository upload.

## Next Development Evidence

Stage 3 should now focus on testing:

- playtesting sessions
- bugs found
- fixes made
- before/after notes
- remaining limitations
