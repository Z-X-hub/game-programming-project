# Fox Dash Report Draft

## 1. Introduction

Fox Dash is a 2D Unity platform runner created for the Game Programming module. The project was developed as a vertical slice: a small but complete playable section that demonstrates the intended final experience.

The game originally had the working title **旋转世界 / Rotating World**, but the final direction was changed to **Fox Dash**. This change helped keep the scope realistic and focused. Instead of building a large unfinished game, the final project focuses on a polished runner loop with character choice, hazards, collectibles, UI, audio, animation, and testing evidence.

## 2. Game Concept And Design

The core idea is a side-scrolling platform runner where the player tries to survive for as long as possible while travelling through generated platform sections. The player collects coins and chests, avoids hazards, and tries to improve their score by moving farther.

The main design goal is clarity. The player should quickly understand:

- how to move
- how to jump and roll
- what hazards are dangerous
- how score increases
- why each character is different

The three-character system adds replay value without making the game too large. Each character supports a different play style:

- `PLAYER` is fast and suits players who want speed and risk.
- `SOLDIER` can revive once after falling or landing in water, making the game more forgiving.
- `ADVENTURER` can double jump, giving more control over gaps and recovery.

This supports meaningful choice while keeping the game simple enough for a vertical slice.

## 3. Technical Implementation

The project is built in Unity 2022 LTS as a 2D game. The main scene is `Assets/Scenes/Play.unity`.

Important systems include:

### Game Flow

`GameManager` controls starting, stopping, scoring, death handling, reset, and screen transitions. This centralises the main game state so UI and character systems can respond consistently.

### Character Controller

`RedCharacter` handles player movement, jump, roll, death, revive, and role-specific abilities. The role-specific logic is kept in one place so that each character can share the same core movement while still having different strengths.

### Character Selection

`PlayerCharacterSelection` stores the selected character and provides display labels for the menu. The start screen applies the selected role before gameplay starts.

### Character Visuals

`KenneyCharacterVisual` loads character sprites at runtime from the `Resources` folder. It also handles simple movement animation timing. Because the source sprites have a limited number of walk frames, animation polish is achieved through careful frame timing, vertical motion, and scale changes rather than complex frame animation.

### Terrain Generation

`TerrainGenerator` creates platform and background blocks as the player moves forward. This supports a runner structure without needing to hand-build a long level.

### UI

The UI includes a start screen, character selector, score HUD, pause screen, and end screen. The start screen was redesigned so that buttons do not cover too much of the new cover art.

## 4. Testing And Iteration

Testing was a major part of improving the project. Some important testing outcomes were:

- The game became unstable after early character additions, so character changes were simplified and rebuilt more carefully.
- Null reference errors were fixed after checking the Unity Console.
- The old `Arial.ttf` built-in font caused problems in Unity 2022, so it was replaced with a safer `LegacyRuntime.ttf` fallback.
- The main menu initially had overlapping character cards and title text, so the layout was redesigned.
- Character walking animation was too fast and stiff, so frame timing and body motion were adjusted.
- A fake arm-swing overlay was tested but removed because it looked unnatural.
- The `SOLDIER` revive ability did not initially continue from the death point, so it was changed to revive at the same horizontal position with a short safety grace period.

Testing evidence is recorded in `TESTING.md`.

## 5. Legal, Ethical, Social, Accessibility, And Security Considerations

### Legal

The project uses RedRunner-derived code and assets under the MIT License. It also uses Kenney Platformer Characters. Third-party credits are recorded in `THIRD_PARTY_NOTICES.md`.

### Ethical And Social

The game does not include gambling, realistic violence, online chat, or personal data collection. The design is suitable for a broad audience because it uses cartoon-style hazards and simple platforming feedback.

### Accessibility

The game uses simple controls and clear character abilities. The `SOLDIER` revive ability makes the game more forgiving for less experienced players. Future accessibility improvements could include remappable controls, colour-blind-safe UI indicators, and adjustable game speed.

### Security

The game is offline and does not require network accounts or user data. Local save data is limited to score, coins, and preferences.

## 6. Problems And Limitations

The main limitation is animation. The available character sprites have limited movement frames, so movement cannot look as detailed as a fully rigged or fully animated character. Some attempted visual effects, such as fake arm swinging, were removed because they reduced clarity.

Another limitation is procedural level fairness. Generated runner sections can create difficulty spikes, so final playtesting should continue checking whether gaps and hazards feel fair.

The project is also a vertical slice rather than a complete commercial game. It demonstrates the main experience, but it would need more levels, balancing, accessibility settings, and polish to become a complete release.

## 7. Reflection

Fox Dash improved through testing and iteration. The project became stronger when the scope was narrowed from a broad idea into a clear runner vertical slice. The character system also improved when abilities were made easier to understand: speed, revive, and double jump are clear player choices.

A key lesson was that adding features is not always the best improvement. Some ideas, like fake arm swinging, made the game look worse and were removed. This showed the importance of judging features by how they feel in play, not just whether they are technically possible.

The final project demonstrates game programming through movement, physics, collisions, UI, audio, animation, procedural generation, character abilities, and debugging. The documentation and GitHub records support the development process required by the module.

## 8. Future Work

If development continued, the next improvements would be:

- Add more polished animation frames or use a character rig.
- Add difficulty balancing for generated level sections.
- Add remappable controls and more accessibility options.
- Add clearer tutorial prompts for each character ability.
- Add more enemy variety and reward pacing.
- Produce a final standalone build and demo video.
