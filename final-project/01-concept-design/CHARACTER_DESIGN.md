# Fox Dash Character Design

## Design Goal

Fox Dash uses three characters so the player can choose a play style before starting the run. The roles are intentionally simple because the game is a vertical slice: each ability should be easy to explain, easy to notice during play, and useful in the platform runner loop.

## Character Roles

| Character | Ability | Player Experience |
| --- | --- | --- |
| `PLAYER` | Faster movement and running-style animation | Higher speed, more pressure, stronger sense of momentum |
| `SOLDIER` | One automatic revive after falling or landing in water | More forgiving, safer for learning the level rhythm |
| `ADVENTURER` | Double jump | Better air control and recovery from mistakes |

## Character 1: PLAYER

The PLAYER is the speed character. This role should feel faster than the other two through both movement speed and animation style.

Design purpose:

- create a higher-risk, higher-energy option
- make the player feel momentum immediately
- reward confident reactions

Balance notes:

- faster movement makes platform gaps and hazards harder to judge
- the role should not also receive extra survival powers
- running animation should show larger strides but avoid an unnaturally fast step rate

## Character 2: SOLDIER

The SOLDIER is the survivability character. The previous `E` shield idea was removed because it added an extra active control and was less suitable for a simple runner. The revised ability is one automatic revive after falling or landing in water.

Design purpose:

- give the player one second chance
- make the character useful without adding complicated controls
- support less experienced players

Balance notes:

- revive should happen once per run
- revive should continue near the death position, where possible, so it feels like a real rescue rather than a full reset
- after the revive is used, the SOLDIER should play normally

## Character 3: ADVENTURER

The ADVENTURER is the movement-control character. The double jump stays because it is easy to understand and fits a platform game naturally.

Design purpose:

- give the player more control in the air
- allow recovery from late jumps
- create a different route and timing style from the PLAYER and SOLDIER

Balance notes:

- double jump should reset after touching the ground
- movement speed should remain normal so the role is not stronger than the PLAYER in every way

## Controls

Planned controls for the final game:

- Move: `A / D` or left/right arrow keys
- Jump: `Space`, `W`, or up arrow
- Roll: `Left Shift`, `Right Shift`, or `S`
- Select character on menu: click a character card or use `1`, `2`, `3`

## Why This Set Works

The three characters cover speed, safety, and movement flexibility. This gives meaningful variety while keeping the project realistic for the module.
