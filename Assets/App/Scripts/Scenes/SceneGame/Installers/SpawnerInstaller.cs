using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Input;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Player;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject;
using App.Scripts.Scenes.SceneGame.Features.Spawner;
using App.Scripts.Scenes.SceneGame.Features.Spawner.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        public SpawnerConfig SpawnerConfig;
        public SpawnZoneConfigs SpawnZoneConfigs;
        public FruitsConfig FruitsConfig;
        [SerializeField] private RestartPopupView _restartPopupView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetServiceSelf(new FruitFactory(serviceContainer));
            serviceContainer.SetServiceSelf(new SpotFactory(serviceContainer));
            serviceContainer.SetServiceSelf(new PieceFactory(serviceContainer));
            serviceContainer.SetServiceSelf(new FruitParticleFactory());
            serviceContainer.SetServiceSelf(new FruitDestroyStrategy(playerDataModel));
            serviceContainer.SetServiceSelf(new SimpleDestroyStrategy());
            var simpleDestroyStrategy = serviceContainer.Get<SimpleDestroyStrategy>();
            var fruitParticleFactory = serviceContainer.Get<FruitParticleFactory>();
            var pieceFactory = serviceContainer.Get<PieceFactory>();
            var spotFactory = serviceContainer.Get<SpotFactory>();
            var addScoreViewFactory = serviceContainer.Get<AddScoreViewFactory>();
            serviceContainer.SetServiceSelf(new FruitSwipeEffectsCreator(pieceFactory, spotFactory,
                fruitParticleFactory, FruitsConfig, addScoreViewFactory));
            var fruitEffectCreator = serviceContainer.Get<FruitSwipeEffectsCreator>();
            serviceContainer.SetServiceSelf(new FruitSwipeStrategy(fruitEffectCreator, simpleDestroyStrategy,
                playerDataModel));
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            serviceContainer.SetServiceSelf(new ZoneInitializer(SpawnZoneConfigs, fruitFactory, gameMath, cameraWrap,
                SpawnerConfig));
            var zoneInitializer = serviceContainer.Get<ZoneInitializer>();

            serviceContainer.SetServiceSelf(new GameSpawner(serviceContainer.Get<ICoroutineRunner>(), SpawnerConfig,
                zoneInitializer));
            var spawner = serviceContainer.Get<GameSpawner>();
            serviceContainer.SetServiceSelf(new GameState(spawner, playerDataModel, serviceContainer.Get<PlayerInput>(),
                serviceContainer.Get<SwipeTracker>(), _restartPopupView));
            GameState gameState = serviceContainer.Get<GameState>();
            gameState.StartPlay();
        }
    }
}