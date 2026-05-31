# Scope, Tools, and Assets

## Project Scope

Fox Dash is planned as a focused 2D platform runner vertical slice. The goal is not to build a large commercial game, but to create a small, coherent, playable piece that shows strong design, programming, testing, and improvement.

## Included In Scope

- Main menu with a clear Fox Dash identity
- Character selection for three playable roles
- One main runner gameplay scene
- Platform generation or repeated platform sections
- Jumping, rolling, collision, collectibles, and score feedback
- Character-specific abilities
- Basic animation and audio feedback
- Game over and restart flow
- Testing notes and improvement record in later stages

## Out Of Scope

- Multiplayer
- Online leaderboard or account system
- Large story campaign
- Multiple worlds with many levels
- Complex enemy AI systems
- Advanced save-game systems
- Large custom animation rigs

Keeping these features out of scope helps make the project achievable and more polished.

## Tools

| Tool | Purpose |
| --- | --- |
| Unity 2022.3.62f3c1 | Main game engine |
| C# | Gameplay programming |
| GitHub | Version control, project process, Kanban, and evidence |
| Kenney Platformer Characters | Character visual assets |
| RedRunner-derived project material | Starting reference/base for the runner structure |

## Planned Unity Project Location

The active Unity project is developed locally as `FoxDash`.

Main scene planned for the playable vertical slice:

```text
Assets/Scenes/Play.unity
```

## Asset Plan

The project uses external and modified assets carefully. Any final submission should keep credits and licences visible in the repository.

Planned asset sources:

- RedRunner-derived open-source runner code/assets used as a reference/base
- Kenney Platformer Characters for player character visuals
- Unity project assets already included in the local game project
- Generated or custom menu/cover image assets created specifically for Fox Dash

## Repository Upload Plan

The repository is organised by assessment stage:

- Stage 1: concept and design documents
- Stage 2: Unity project source and gameplay development evidence
- Stage 3: testing and bug-fix evidence
- Stage 4: report
- Stage 5: demo and presentation material

Unity generated cache folders should not be committed. The final repository should include source files, scenes, prefabs, scripts, assets, documentation, and build/run instructions, but not local editor caches.
