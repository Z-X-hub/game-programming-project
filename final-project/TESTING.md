# Fox Dash Testing Record

This document records testing, bugs, and improvements for the Fox Dash final project. It is intended to show how the game was tested and improved during development.

## Testing Approach

Testing focused on whether the vertical slice was playable, stable, and understandable. The main areas tested were:

- Game starts correctly from the main menu
- Character selection works
- Character abilities are different and useful
- Movement, jump, roll, collision, and death behaviour work reliably
- UI does not block the game view
- Character animations communicate movement clearly
- Build/compile errors are removed
- External assets are credited

## Technical Verification

The project was repeatedly checked with Unity C# compilation through:

```text
dotnet build Assembly-CSharp.csproj --no-restore
```

This helped catch script errors before testing in the Unity Editor.

## Test Log

| Area | Test | Result | Change Made |
| --- | --- | --- | --- |
| Game startup | Enter Play mode from `Play.unity` | Game initially had runtime errors after early character changes | Removed incorrect new-character setup and restored stable player references |
| Runtime errors | Checked Console errors during gameplay | Null reference errors appeared in character update logic | Reconnected character references and moved custom visuals into a safer runtime visual layer |
| Unity 2022 compatibility | Checked menu font loading | `Arial.ttf` built-in font error appeared | Replaced old built-in font reference with `LegacyRuntime.ttf` fallback |
| Character selection | Tested selecting characters from home screen | Selection existed but UI overlapped the title and background | Rebuilt character selector layout and moved menu buttons to reduce screen blocking |
| Character abilities | Tested three characters in gameplay | First version used an `E` shield, but the revised design needed automatic revive | Removed `E` shield input and changed `SOLDIER` to one automatic revive after falling/water |
| Soldier revive | Tested falling with `SOLDIER` | Revive did not reliably continue from the death point | Changed revive to use the death position's X coordinate and raise the character vertically with a short grace period |
| Adventurer double jump | Pressed jump twice in the air | Double jump works only for `ADVENTURER` | Kept role-specific jump count logic |
| Runner speed | Compared `PLAYER` with other characters | Fast role worked but walking animation looked too fast | Tuned animation rate and movement transform so `PLAYER` feels like running while others feel like walking |
| Character scale | Tested visibility in gameplay | Character appeared too small | Increased Kenney character visual scale to improve readability |
| Main menu | Checked home screen after adding cover art | Buttons covered too much of the background | Moved buttons to bottom right and compacted the character selector |
| Third-party assets | Checked external character pack usage | Kenney assets needed clear credit | Added licence notes and copied the Kenney licence into the project documentation |

## Gameplay Tests To Repeat Before Submission

- Open `Assets/Scenes/Play.unity` in Unity.
- Start the game from the home screen.
- Select `PLAYER` and confirm it moves faster than the other characters.
- Select `SOLDIER`, fall once, and confirm it revives once at the death position.
- Confirm `SOLDIER` dies normally on the second fall.
- Select `ADVENTURER` and confirm double jump works.
- Test collision with spikes, saws, mace, and water.
- Confirm score increases while moving forward.
- Confirm coins and chests can be collected.
- Confirm pause and end screens still work.
- Check the Console for new errors or warnings that affect gameplay.

## Remaining Risks

- The source character sprites have limited walking frames, so animation quality depends on timing and transform polish.
- The generated level can still produce difficult platform combinations; final playtesting should check fairness and pacing.
- The project should be tested from a clean Unity open before submission to make sure generated files or local cache are not hiding missing assets.
