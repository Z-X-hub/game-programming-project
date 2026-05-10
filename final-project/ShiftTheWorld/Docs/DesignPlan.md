# Shift the World - Design Plan

## Concept

`Shift the World` is a 2.5D cartoon puzzle platformer where the hero walks automatically and the player controls the environment. Instead of jumping, running, or fighting, the player selects platforms, rotating blocks, switches, and doors to create a safe route to the exit.

## Target Player

The target player is someone who enjoys short puzzle games with clear rules and low input pressure. The prototype should be understandable within a few seconds, suitable for a coursework demo, and accessible to players who may not be expert platform-game players.

## Core Mechanic

The core mechanic is world manipulation:

- The character automatically walks along the X axis.
- The player selects environmental objects with `A/D` or arrow keys.
- The player rotates or activates those objects with `Q/E` and `Space`.
- The level is solved by changing the world at the right time, not by controlling the character directly.

This creates a reversed platformer structure: the hero is simple, and the level is the player's tool.

## Why 2.5D Instead of Full 3D

2.5D was chosen because it supports a strong visual presentation without making the scope too large. Full 3D movement would require camera control, navigation, pathfinding, depth readability, and more complex level design. A fixed side-view 2.5D layout keeps the game readable and stable while still allowing rounded 3D platforms, stylized lighting, and cartoon materials.

This choice also supports the core mechanic. The player can focus on puzzle timing and object selection instead of struggling with movement or camera controls.

## Intended Player Experience

The player should feel like they are operating a small toy-like machine. The walker is predictable, and the player changes the environment to help it. The experience should be playful, readable, and slightly mechanical:

- "I can see what will happen."
- "I understand what I can control."
- "I solved the puzzle by changing the world."

## Scope Justification

The vertical slice is intentionally small:

- One main menu
- One level select screen
- One playable level
- One auto-walking character
- One moving platform mechanic
- One rotating platform mechanic
- One switch-door puzzle
- One hazard/fail state
- One exit/win state

This scope is realistic for a university coursework project and allows time for polish, testing, documentation, and a stable playable build.

## Tools and Assets Plan

- Engine: Unity
- Language: C#
- Visuals: simple Unity primitives, cartoon materials, rounded-looking platform proportions
- UI: Unity Canvas with large readable text and simple panels
- Audio: optional placeholder sounds if time allows
- External assets: avoid unless properly credited

## Accessibility Considerations

- Simple keyboard controls with few buttons
- Clear objective text
- Selected object name shown on screen
- Bright highlight for selected objects
- Restart key available at all times
- Fixed camera to reduce motion confusion
- No combat, time pressure, or precision jumping required from the player

## Legal, Ethical, and Social Considerations

- Use original placeholder assets or properly credited external assets.
- Avoid unlicensed fonts, sounds, or art.
- Include an AI assistance disclosure in the credits because AI helped draft code and documentation.
- Keep the design non-violent and accessible.
- Avoid manipulative monetisation or online data collection; neither is relevant to this coursework prototype.

## Connection to Game Design Principles

- Clarity: objects that can be controlled are highlighted and named.
- Consistency: the walker always moves automatically along the side-view axis.
- Feedback: switches change colour, selected objects highlight, and win/fail panels appear.
- Constraint: limiting movement to 2.5D makes the puzzle easier to understand.
- Meaningful choice: the player's main decision is which world object to manipulate and when.
- Scope control: the prototype focuses on one complete puzzle loop instead of many incomplete features.
