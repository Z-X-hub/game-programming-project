# Fox Dash Third-Party Notices

This document records external code, assets, tools, and resources used or referenced by the Fox Dash final project.

## RedRunner

- Project: RedRunner
- Author: Bayat Games
- Source: https://github.com/BayatGames/RedRunner
- Licence: MIT License
- Original copyright: Copyright (c) 2017 Bayat Games

Fox Dash is based on a RedRunner-derived Unity runner project. The following areas are derived from or adapted from the RedRunner-style project structure:

- 2D runner gameplay structure
- Unity scene, prefab, and manager setup
- Character movement foundation
- Collectable, enemy, UI, camera, audio, save, and terrain generation logic
- Some original sprites, animation, audio, font, and UI/gameplay assets

The MIT License allows use, modification, and redistribution provided that the copyright and licence notice are preserved.

## Kenney Platformer Characters

- Asset pack: Kenney Platformer Characters
- Author: Kenney
- Source folder used during development: `kenney_platformer-characters`
- Project use: player character sprites for the three playable roles

The character sprites used for `PLAYER`, `SOLDIER`, and `ADVENTURER` are selected from the Kenney platformer character asset pack and copied into the Unity project under:

```text
Assets/Resources/FoxDash/KenneyCharacters
```

The Kenney licence file should be kept with the project. In the local Unity project, a copy is stored under:

```text
Assets/ThirdParty/KenneyPlatformerCharacters/License.txt
```

## Unity Standard Assets / CrossPlatformInput

The project includes Unity Standard Assets CrossPlatformInput helper scripts for input handling. These are used to support keyboard and cross-platform input naming such as `Horizontal`, `Jump`, and other mapped actions.

## Fonts

The original project includes bundled font files, including OpenSans under Unity Standard Assets. Font files should keep their original licence files where included.

## SaveGameFree

The project includes SaveGameFree / Save Game reference code from the original project structure. It is used for local save-style data such as score, coins, and preferences.

## Generated Or Modified Assets

Fox Dash also includes project-specific modified or generated assets, including:

- Fox Dash home cover art
- Project-specific menu layout and character selection UI
- Runtime character presentation using selected Kenney sprites

## Notes For Submission

When submitting or sharing the project, keep this file and any included licence files with the Unity project. This helps show legal awareness and proper credit for external resources.
