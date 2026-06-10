# Fox Dash

Fox Dash is a Unity 2D platform runner packaged as a clean standalone project.

The game focuses on a 2D runner gameplay loop:

- run forward through generated platform sections
- jump over gaps and traps
- collect coins and chests
- avoid enemies and hazards
- score by moving farther through the level
- choose between three characters with different play styles
- pause, restart the current run, or return to the home menu
- review the end screen for death reason, score, high score, new-record status, and coins collected
- track coins collected during the current run while playing

## Controls

- Move: `A / D` or left/right arrows
- Jump: `Space`, `W`, or up arrow
- Roll: `Shift` or `S`
- Pause: `Esc` or the in-game pause button

## Characters

- `PLAYER`: faster run speed
- `SOLDIER`: one automatic revive after falling or landing in water
- `ADVENTURER`: double jump

## Interface Updates

- The home menu includes a short quick guide explaining the goal, controls, and character differences.
- The in-game HUD shows current-run coins and total coins.
- The pause screen includes `Resume`, `Restart`, and `Home`.
- The end screen explains why the run ended, the score, the high score, whether it is a new record, coins collected in the run, and total coins.

## Open In Unity

Open this folder in Unity:

```text
/Users/zhuxuan/Downloads/FoxDash
```

Use Unity `2022.3.62f3c1` or a compatible 2022 LTS editor. The main scene is:

```text
Assets/Scenes/Play.unity
```

## Final Build Export

The app icon and Unity PlayerSettings branding use the Fox Dash assets under:

```text
Assets/Sprites/FoxDash/Icons/
```

After opening the project in Unity, run:

```text
Tools > Fox Dash > Apply Project Branding
```

Then export the final builds:

- macOS: `Tools > Fox Dash > Build > macOS Standalone`
- Windows: `Tools > Fox Dash > Build > Windows Standalone`

If the teacher may mark the project on Windows, submit a Windows zip as well as the macOS zip. A macOS `.app` is not directly runnable on Windows.

## Ownership And Reference

Project-specific naming, packaging, comments, folder cleanup, and Unity branding are prepared for Zhu Xuan Studio.

This project uses RedRunner as an open-source reference and retains or adapts selected MIT-licensed material where appropriate. It is not a direct copy of RedRunner, and the Fox Dash-specific changes are documented in `THIRD_PARTY_NOTICES.md`.

AI assistance was used only for selected difficult implementation and debugging areas, not for the whole project. See `AI_DECLARATION.md` for the limited AI-use statement.
