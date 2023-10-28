using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Bomb;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Fruits;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Mimic;
using App.Scripts.Scenes.SceneGame.Features.Items.Mimic.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        public SpawnerConfig SpawnerConfig;
        public SpawnZoneConfigs SpawnZoneConfigs;
        [SerializeField] private MimicConfig _mimicConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            var iCoroutineRunner = serviceContainer.Get<ICoroutineRunner>();
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            var allSpawnedObjects = serviceContainer.Get<AllSpawnedObjects>();
            var samuraiMod = serviceContainer.Get<SamuraiMod>();

            var zoneInitializer = new ZoneInitializer(SpawnZoneConfigs, gameMath, cameraWrap);
            var packCreator = new PackCreator(SpawnerConfig, playerDataModel, allSpawnedObjects, samuraiMod);
            var mimicSystem = new MimicSystem(new MimicObjectCreator(packCreator), _mimicConfig);
            serviceContainer.SetITickableAndSelf(mimicSystem);
            serviceContainer.SetServiceSelf(new MimicFactory(allSpawnedObjects, mimicSystem));

            BindSpawnedObjects(serviceContainer);

            serviceContainer.SetServiceSelf(new GameSpawner(iCoroutineRunner, SpawnerConfig, zoneInitializer,
                packCreator, serviceContainer.Get<FruitFactory>()));
        }

        private void BindSpawnedObjects(ServiceContainer serviceContainer)
        {
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            var bombFactory = serviceContainer.Get<BombFactory>();
            var healthItemFactory = serviceContainer.Get<HealthItemFactory>();
            var fruitBoxFactory = serviceContainer.Get<FruitBoxFactory>();
            var iceFactory = serviceContainer.Get<IceFactory>();
            var magnetFactory = serviceContainer.Get<MagnetFactory>();
            var brickFactory = serviceContainer.Get<BrickFactory>();
            var samuraiFactory = serviceContainer.Get<SamuraiFactory>();
            var mimicFactory = serviceContainer.Get<MimicFactory>();
            SpawnerConfig.GetAndBind<FruitsContainer, FruitFactory>(fruitFactory);
            SpawnerConfig.GetAndBind<BombsContainer, BombFactory>(bombFactory);
            SpawnerConfig.GetAndBind<HealthItemContainer, HealthItemFactory>(healthItemFactory);
            SpawnerConfig.GetAndBind<FruitBoxContainer, FruitBoxFactory>(fruitBoxFactory);
            SpawnerConfig.GetAndBind<IceContainer, IceFactory>(iceFactory);
            SpawnerConfig.GetAndBind<MagnetObjectContainer, MagnetFactory>(magnetFactory);
            SpawnerConfig.GetAndBind<BrickContainer, BrickFactory>(brickFactory);
            SpawnerConfig.GetAndBind<SamuraiContainer, SamuraiFactory>(samuraiFactory);
            SpawnerConfig.GetAndBind<MimicContainer, MimicFactory>(mimicFactory);
        }
    }
}