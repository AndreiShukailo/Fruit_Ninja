using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject;
using App.Scripts.Scenes.SceneGame.Features.Spawner;
using App.Scripts.Scenes.SceneGame.Features.Spawner.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        public AnimationConfig AnimationConfig;
        public SpawnerConfig SpawnerConfig;
        public SpawnZoneConfigs SpawnZoneConfigs;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetServiceSelf(new FruitFactory(serviceContainer, AnimationConfig));
            serviceContainer.SetServiceSelf(new SpotFactory(serviceContainer, AnimationConfig));
            serviceContainer.SetServiceSelf(new PieceFactory(serviceContainer, AnimationConfig));
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            serviceContainer.SetServiceSelf(new ZoneInitializer(SpawnZoneConfigs, fruitFactory, gameMath, cameraWrap,
                SpawnerConfig));
            var zoneInitializer = serviceContainer.Get<ZoneInitializer>();
            serviceContainer.SetServiceSelf(new GameSpawner(serviceContainer.Get<ICoroutineRunner>(), SpawnerConfig,
                zoneInitializer));
            serviceContainer.Get<GameSpawner>().StartSpawn();
        }
    }
}