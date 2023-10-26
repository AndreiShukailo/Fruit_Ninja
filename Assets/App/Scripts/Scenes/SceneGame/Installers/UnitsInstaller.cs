using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Bomb;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.Fruits.FruitPieces;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Services;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Spot;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Spray;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Strategies;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick.Config;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox;
using App.Scripts.Scenes.SceneGame.Features.Items.FruitBox.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class UnitsInstaller : MonoInstaller
    {
        [SerializeField] private FruitsConfig _fruitsConfig;
        [SerializeField] private BombConfig _bombConfig;
        [SerializeField] private HealthItemConfig _healthItemConfig;
        [SerializeField] private FruitBoxConfig _fruitBoxConfig;
        [SerializeField] private IceConfig _iceConfig;
        [SerializeField] private MagnetConfig _magnetConfig;
        [SerializeField] private BrickConfig _brickConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var gamePhysics = serviceContainer.Get<GamePhysics>();
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            var checkOnInvisible = serviceContainer.Get<CheckOnInVisible>();
            var swipeTracker = serviceContainer.Get<SwipeTracker>();
            var shadowSystem = serviceContainer.Get<ShadowSystem>();
            var physicalStartVelocityCalculator = serviceContainer.Get<PhysicalStartVelocityCalculator>();
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            var retardSystem = serviceContainer.Get<RetardSystem>();
            var comboSystem = serviceContainer.Get<ComboSystem>();
            var playerInput = serviceContainer.Get<PlayerInput>();
            
            var allSpawnedObject = new AllSpawnedObjects();
            serviceContainer.SetServiceSelf(allSpawnedObject);
            serviceContainer.SetITickableAndSelf(new MagnetSystem(allSpawnedObject, _magnetConfig, new MagnetEffectFactory()));

            var addScoreViewFactory = serviceContainer.Get<AddScoreViewFactory>();
            var simpleDestroyStrategy = new SimpleDestroyStrategy();
            var fruitEffectCreator = GetFruitSwipeEffectsCreator(gamePhysics, shadowSystem, checkOnInvisible,
                addScoreViewFactory, simpleDestroyStrategy, allSpawnedObject);
            
            var fruitSwipeStrategy =
                new FruitSwipeStrategy(fruitEffectCreator, simpleDestroyStrategy, playerDataModel, comboSystem);
            var fruitDestroyStrategy = new FruitDestroyStrategy(playerDataModel);

            var bombEffectFactory =
                new BombEffectsFactory(_bombConfig.BombEffectContainer, serviceContainer.Get<CameraWrap>());
            var bombSwipeStrategy =
                new BombSwipeStrategy(_bombConfig, allSpawnedObject, bombEffectFactory, playerDataModel);
            var healthItemSwipeStrategy = new HealthItemSwipeStrategy(playerDataModel, _healthItemConfig);
            
            var fruitBoxSwipeStrategy =
                new FruitBoxSwipeStrategy(_fruitBoxConfig, physicalStartVelocityCalculator, cameraWrap);
            var iceSwipeStrategy = new IceSwipeStrategy(_iceConfig, retardSystem);

            var magnetSystem = serviceContainer.Get<MagnetSystem>();
            var magnetSwipeStrategy = new MagnetSwipeStrategy(magnetSystem);

            var brickSwipeStrategy = new BrickSwipeStrategy(playerInput, _brickConfig);
            
            serviceContainer.SetServiceSelf(new FruitFactory(gamePhysics, fruitDestroyStrategy, checkOnInvisible,
                swipeTracker, fruitSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new BombFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, bombSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new HealthItemFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, healthItemSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new FruitBoxFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, fruitBoxSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new IceFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, iceSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new MagnetFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, magnetSwipeStrategy, shadowSystem, allSpawnedObject));
            serviceContainer.SetServiceSelf(new BrickFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, brickSwipeStrategy, shadowSystem, allSpawnedObject));
        }

        private FruitSwipeEffectsCreator GetFruitSwipeEffectsCreator(GamePhysics gamePhysics, ShadowSystem shadowSystem,
            CheckOnInVisible checkOnInvisible, AddScoreViewFactory addScoreViewFactory,
            IDestroyStrategy iDestroyStrategy, AllSpawnedObjects allSpawnedObjects)
        {
            var pieceFactory = new PieceFactory(gamePhysics, shadowSystem, iDestroyStrategy, checkOnInvisible, allSpawnedObjects);
            var spotFactory = new FruitSpotFactory();
            var fruitParticleFactory = new FruitParticleFactory();
            var fruitEffectCreator = new FruitSwipeEffectsCreator(pieceFactory, spotFactory, fruitParticleFactory,
                _fruitsConfig, addScoreViewFactory);

            return fruitEffectCreator;
        }
    }
}