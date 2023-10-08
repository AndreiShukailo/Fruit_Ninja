using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation;
using App.Scripts.Scenes.SceneGame.Features.BackGround;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Config;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject;
using App.Scripts.Scenes.SceneGame.Features.Spawner;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public SpawnedAnimationConfig SpawnedAnimationConfig;
        public ShadowConfig ShadowConfig;
        public PhysicsConfig PhysicsConfig;
        public PrevAspectSaver PrevAspectSaver;
        public SpawnZoneConfigs SpawnZoneConfigs;
        public Camera Camera;
        public SpriteRenderer BackGround;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new CameraWrap(Camera, PrevAspectSaver));
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetServiceSelf(new FlexibleBackGround(cameraWrap, BackGround));
            var config = serviceContainer.Get<GameConfig>();
            serviceContainer.SetITickableAndSelf(new GamePhysics(PhysicsConfig));
            var physics = serviceContainer.Get<GamePhysics>();
            serviceContainer.SetITickableAndSelf(new ShadowSystem(ShadowConfig));
            var shadowSystem = serviceContainer.Get<ShadowSystem>();
            serviceContainer.SetServiceSelf(new SpawnedObjectFactory(config, cameraWrap, physics, shadowSystem,
                SpawnedAnimationConfig));
            var fruitFactory = serviceContainer.Get<SpawnedObjectFactory>();
            serviceContainer.SetServiceSelf(new PhysicalStartVelocityCalculator(PhysicsConfig));
            var gameMath = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            serviceContainer.SetServiceSelf(new GameSpawner(config, serviceContainer.Get<ICoroutineRunner>(),
                SpawnZoneConfigs,
                fruitFactory, gameMath, cameraWrap));
            serviceContainer.Get<GameSpawner>().StartSpawn();
        }
    }
}