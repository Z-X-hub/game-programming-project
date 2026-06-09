# AI Assistance Declaration

This project was not created entirely by AI. AI assistance was used selectively for difficult implementation and debugging points, while the final game idea, feature choices, Unity integration, asset selection, testing, and acceptance of changes remained under the student's control.

## Student-Led Work

- Decided the final game direction, title, character roles, and gameplay goals.
- Chose the RedRunner base project and Kenney character assets.
- Opened and tested the game in Unity.
- Checked whether suggested code changes worked in the actual scene.
- Accepted, rejected, or adjusted changes based on gameplay feel.
- Organised the project files and maintained the final submission materials.

## AI-Assisted Code Areas

AI assistance was used only for selected complex or error-prone areas of the codebase:

| Area | Files | Nature of AI Assistance |
| --- | --- | --- |
| Character selection state | `Assets/Scripts/FoxDash/Characters/PlayerCharacterSelection.cs`, `Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs` | Helped structure a simple role-selection system, persist the chosen role with `PlayerPrefs`, and connect the menu selection to the playable character. |
| Three-character ability logic | `Assets/Scripts/FoxDash/Characters/RedCharacter.cs` | Helped reason through the tricky parts of separating the fast runner, one-time SOLDIER revive, and ADVENTURER double jump without breaking the original RedRunner movement loop. |
| SOLDIER one-time revive | `Assets/Scripts/FoxDash/Characters/RedCharacter.cs` | Helped debug edge cases around fall/water death, revive height, revive grace time, Rigidbody2D reset, and making the revive continue close to the death position instead of restarting the whole run. |
| ADVENTURER double jump | `Assets/Scripts/FoxDash/Characters/RedCharacter.cs` | Helped refine the jump-count checks so the character can jump twice but other characters remain limited to one jump. |
| Kenney character visual adaptation | `Assets/Scripts/FoxDash/Characters/KenneyCharacterVisual.cs`, `Assets/Scripts/FoxDash/Characters/RedCharacter.cs` | Helped tune sprite fallback, character scaling, movement poses, speed trails, and arm-swing animation so imported character assets fit the runner game better. |
| Main menu layout and cleanup | `Assets/Scripts/FoxDash/UI/UIScreen/StartScreen.cs` | Helped reorganise the character cards and remove unused social/share-style buttons so the home screen supports the new character-selection feature. |
| Runtime stability debugging | `Assets/Scripts/FoxDash/GameManager.cs`, `Assets/Scripts/FoxDash/UIManager.cs`, `Assets/Scripts/FoxDash/Characters/RedCharacter.cs` | Helped identify likely causes of null-reference and Unity-version compatibility issues, then add defensive checks where appropriate. |

## Non-AI / External Sources

- The original runner foundation is derived from the RedRunner open-source project under the MIT License.
- Character graphics are based on Kenney Platformer Characters.
- Other bundled assets and systems are credited in `THIRD_PARTY_NOTICES.md`.
- AI assistance did not replace third-party attribution, licensing, or the student's responsibility to understand and explain the project.

## Final Responsibility Statement

AI was used as a development assistant for specific difficult sections, mainly to support debugging, structure suggestions, and local code refinement. The final submitted project remains the student's responsibility, and the student should be able to explain the listed assisted areas during the demo or report discussion.
