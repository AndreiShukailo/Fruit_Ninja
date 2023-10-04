using App.Scripts.Infrastructure;
using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.BackGround;
using App.Scripts.Scenes.SceneGame.Features.Camera;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Spawner;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public Camera Camera;
        public SpriteRenderer BackGround;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new CameraWrap(Camera));
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetServiceSelf(new FlexibleBackGround(cameraWrap, BackGround));
            var gameData = serviceContainer.Get<GameConfig>();
            serviceContainer.SetITickableAndSelf(new GamePhysics(gameData));
            var physics = serviceContainer.Get<GamePhysics>();
            serviceContainer.SetServiceSelf(new FruitFactory(gameData, cameraWrap, physics));
            var fruitFactory = serviceContainer.Get<FruitFactory>();
            serviceContainer.SetServiceSelf(new PhysicalStartVelocityCalculator(gameData));
            var gameMath =  serviceContainer.Get<PhysicalStartVelocityCalculator>();
            serviceContainer.SetITickableAndSelf(new GameSpawner(gameData, fruitFactory, gameMath, cameraWrap));
        }
    }
}