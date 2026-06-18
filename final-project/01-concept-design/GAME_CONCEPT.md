# Fox Dash Game Concept

## Working Title Change

The project previously used the working title `Rotating World`. The final title is `Fox Dash`.

## High Concept

Fox Dash is a 2D Unity platform runner vertical slice inspired by the
open-source RedRunner project. It is not designed as a fixed level-by-level
campaign. Instead, the player enters a repeatable runner challenge where
platform blocks, gaps, coins, and hazards are combined into a changing obstacle
route.

The player chooses one of three characters, runs across generated platform
sections, collects coins and chests, avoids hazards, and tries to survive for
as long as possible. Even though the game is single-player, the high-score
structure gives it a competitive feeling: the player is competing against their
own previous record.

The project focuses on one clear playable experience rather than a large
unfinished game. The main design goal is to make movement feel quick, readable,
and rewarding while giving the player enough role choice that each run does not
feel identical.

## Player Goal

The player must keep moving forward, react to gaps and hazards, collect rewards,
and use the chosen character ability to survive longer. The long-term goal is
not to finish a fixed level, but to beat the previous distance and coin record.

## Core Loop

1. Choose a character on the main menu.
2. Start the run.
3. Move, jump, roll, and avoid danger.
4. Decide whether to take safer routes or riskier coin-heavy routes.
5. Collect coins and chests when safe.
6. Survive longer to increase the score.
7. Restart and try a different character, better timing, or a better route.

## Intended Player Experience

Fox Dash should feel bright, fast, and easy to understand. The player should quickly know what the goal is and why each character changes the way the run feels.

The experience should be:

- simple to start
- responsive to control
- visually readable
- replayable through character choice
- competitive through personal score improvement
- strategic through coin-risk decisions
- polished enough to feel like one complete vertical slice

## Obstacle And Reward Design

The level is built from reusable platform blocks rather than hand-authored
linear stages. This supports replay because the player meets different
combinations of jumps, gaps, collectables, and hazards.

| Element | Mechanic | Player Decision |
| --- | --- | --- |
| Gaps and water | Falling or landing in water ends the run unless `SOLDIER` has a revive available. | Jump earlier, use double jump, or choose safer timing. |
| Spikes | Spikes punish careless landing/contact from dangerous angles. | Read the platform edge and avoid landing on the hazard. |
| Saw | The saw rotates and kills on contact. | Jump or roll with enough timing margin. |
| Mace | The mace moves/strikes and punishes being in the wrong place at the wrong time. | Watch movement timing before committing to the route. |
| Coins and chests | Rewards are placed along the route and encourage risk-taking. | Some coin paths are more rewarding but expose the player to harder jumps or hazards. |

This creates a small risk-reward loop inside the runner: the safest path may
keep the run alive, but the more rewarding path may require better use of the
selected character's ability.

## Main Design Principles

- Clear feedback: jumping, collecting, death, revive, and character ability should be obvious.
- Meaningful choice: each character should support a different play style.
- Realistic scope: polish one runner level loop instead of building many unfinished levels.
- Readable challenge: hazards should be visible early enough for the player to react.
- Fair restart: the game should make failure easy to understand and quick to retry.
- Skill expression: the player should feel that better timing, better route
  choice, and better character use can improve the next run.

## Vertical Slice Scope

The vertical slice will include:

- a main menu with character selection
- one playable runner scene
- generated platform sections
- three playable characters with different abilities
- collectibles and score feedback
- hazards or fall/water failure conditions
- restart/game-over flow
- basic animation and audio feedback

## Out Of Scope For This Submission

The project will not attempt multiplayer, online services, many full worlds, complex enemy AI, or a large story campaign. Those would make the scope too large for the module timeline.
