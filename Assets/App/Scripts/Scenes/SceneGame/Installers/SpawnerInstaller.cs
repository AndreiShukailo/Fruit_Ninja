using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Bomb;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Fruits;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice.Factory;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
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
            BindSpawnedObjects(serviceContainer);
            
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            var iCoroutineRunner = serviceContainer.Get<ICoroutineRunner>();
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();


            var zoneInitializer = new ZoneInitializer(SpawnZoneConfigs, gameMath, cameraWrap);
            var packCreator = new PackCreator(SpawnerConfig, playerDataModel);
            serviceContainer.SetServiceSelf(new GameSpawner(iCoroutineRunner, SpawnerConfig, zoneInitializer, packCreator));
        }

        private void BindSpawnedObjects(ServiceContainer serviceContainer)
        {
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            var bombFactory = serviceContainer.Get<BombFactory>();
            var healthItemFactory = serviceContainer.Get<HealthItemFactory>();
            var fruitBoxFactory = serviceContainer.Get<FruitBoxFactory>();
            var iceFactory = serviceContainer.Get<IceFactory>();
            SpawnerConfig.GetAndBind<FruitsContainer, FruitFactory>(fruitFactory);
            SpawnerConfig.GetAndBind<BombsContainer, BombFactory>(bombFactory);
            SpawnerConfig.GetAndBind<HealthItemContainer, HealthItemFactory>(healthItemFactory);
            SpawnerConfig.GetAndBind<FruitBoxContainer, FruitBoxFactory>(fruitBoxFactory);
            SpawnerConfig.GetAndBind<IceContainer, IceFactory>(iceFactory);
        }
    }
}