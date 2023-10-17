using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.Fruits.FruitPieces;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Services;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Spot;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Strategies;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        public SpawnerConfig SpawnerConfig;
        public SpawnZoneConfigs SpawnZoneConfigs;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            var iCoroutineRunner = serviceContainer.Get<ICoroutineRunner>();


            var zoneInitializer = new ZoneInitializer(SpawnZoneConfigs, fruitFactory, gameMath, cameraWrap,
                SpawnerConfig);
            serviceContainer.SetServiceSelf(new GameSpawner(iCoroutineRunner, SpawnerConfig, zoneInitializer));
        }
    }
}