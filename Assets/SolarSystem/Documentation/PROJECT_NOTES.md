# Interactive Solar System for Kids

## How to run

1. Open this folder in Unity 2022.3 LTS: `UnitySolarSystemKids`.
2. Open `Assets/SolarSystem/Scenes/MainScene.unity`.
3. Press Play.
4. Click Earth, Moon, Mars, or the Sun.
5. Use the Return Home button, right mouse button, or Escape to return to the main view.

## Assignment checklist

- Sun, Earth, Moon, and Mars are included.
- Earth and Moon are clickable.
- Clicks move the camera toward the selected object.
- A child friendly fact appears after each click.
- Clicks trigger audio and a short visual pulse.
- A glowing selection ring and sparkle burst make the selected object clear.
- Floating labels help children identify each object.
- Mission Control shows progress for Earth, Moon, close-up view, feedback, and a bonus visit.
- The scene uses materials, lighting, behavior scripts, audio, camera motion, UI, and a comet prefab.
- The project includes a return-to-main-view control.

## Main scripts

- `SolarBody.cs`: stores clickable object info and plays visual feedback.
- `OrbitMotion.cs`: handles planet orbit and self rotation.
- `SolarSystemController.cs`: handles selection, camera movement, text, sound, and return home.
- `CometTrail.cs`: adds bonus movement to the comet prefab.
- `BillboardLabel.cs`: keeps world-space labels facing the camera.
- `StaticStarfield.cs`: builds a procedural star field around the scene.
- `SolarSystemSceneBuilder.cs`: editor-only tool that generated the scene.

## Notes for demo

This is a small polished prototype, matching the assignment note that a clear finished idea is better than a large unfinished idea. The interaction is designed for children: simple clicks, short facts, bright feedback, and one obvious way back.
