# Fruit Ninja

A fruit slicing game with custom mechanics, built in Unity 2021.3 (C#).

![Gameplay](docs/gameplay.gif)

## Gameplay

- **Swipe to slice** fruits: they split into pieces with juice splashes and spots
- **Custom physics** for flying objects: own gravity and start velocity calculation,
  no Unity Rigidbody
- **Special items**:
  - **Bomb**: explodes and ends a life
  - **Ice**: slows down time for all objects
  - **Magnet**: pulls nearby objects
  - **Mimic**: an object that changes its form while flying
  - **Samurai**: activates the samurai mode
  - **Fruit box**: releases a group of fruits
  - **Brick**: cannot be sliced
  - **Heart**: gives an extra life
- **Combo system** with popups for slicing several fruits in one swipe
- **Spawner** with configurable zones (custom editor for zone setup)
- **Shadows** for flying objects
- Score and best score saving, adaptive background and camera for different aspect ratios

## Architecture

- **Custom DI**: project and scene contexts, installers, service container
- **Game state machine**: bootstrap, load level, game loop
- **Strategy pattern** for swipe behavior: each item type has its own swipe strategy
- **Factories and configs** (ScriptableObject) for every item type
- **Save / Load service** for player progress
- **Editor tools**: spawn zone config editor, background editor
- **DOTween** animations

## Project structure

```
Assets/App/Scripts/
├── Infrastructure/   # bootstrap, DI, game state machine, scene loading
└── Scenes/
    ├── Common/       # buttons, save/load, player progress
    ├── SceneMenu/    # main menu
    └── SceneGame/
        └── Features/ # physics, fruits, items, combo, spawner, input, shadows
```

## How to run

1. Open the project in **Unity 2021.3.28f1**
2. Open `Assets/App/Scenes/Menu.unity`
3. Press Play
