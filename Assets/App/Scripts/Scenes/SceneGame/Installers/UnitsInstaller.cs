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
using App.Scripts.Scenes.SceneGame.Features.Items.Heart;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.Factory;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class UnitsInstaller : MonoInstaller
    {
        [SerializeField] private FruitsConfig _fruitsConfig;
        [SerializeField] private BombConfig _bombConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var gamePhysics = serviceContainer.Get<GamePhysics>();
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            var checkOnInvisible = serviceContainer.Get<CheckOnInVisible>();
            var swipeTracker = serviceContainer.Get<SwipeTracker>();
            var shadowSystem = serviceContainer.Get<ShadowSystem>();

            var comboSystem = serviceContainer.Get<ComboSystem>();

            var addScoreViewFactory = serviceContainer.Get<AddScoreViewFactory>();
            var simpleDestroyStrategy = new SimpleDestroyStrategy();
            var fruitEffectCreator = GetFruitSwipeEffectsCreator(gamePhysics, shadowSystem, checkOnInvisible,
                addScoreViewFactory, simpleDestroyStrategy);
            var fruitSwipeStrategy =
                new FruitSwipeStrategy(fruitEffectCreator, simpleDestroyStrategy, playerDataModel, comboSystem);
            var fruitDestroyStrategy = new FruitDestroyStrategy(playerDataModel);

            var bombEffectFactory =
                new BombEffectsFactory(_bombConfig.BombEffectContainer, serviceContainer.Get<CameraWrap>());
            var bombSwipeStrategy = new BombSwipeStrategy(_bombConfig, checkOnInvisible, bombEffectFactory);
            var healthItemSwipeStrategy = new HealthItemSwipeStrategy(playerDataModel);

            serviceContainer.SetServiceSelf(new FruitFactory(gamePhysics, fruitDestroyStrategy, checkOnInvisible,
                swipeTracker, fruitSwipeStrategy, shadowSystem));
            serviceContainer.SetServiceSelf(new BombFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, bombSwipeStrategy, shadowSystem));
            serviceContainer.SetServiceSelf(new HealthItemFactory(gamePhysics, simpleDestroyStrategy, checkOnInvisible,
                swipeTracker, healthItemSwipeStrategy, shadowSystem));
        }

        private FruitSwipeEffectsCreator GetFruitSwipeEffectsCreator(GamePhysics gamePhysics, ShadowSystem shadowSystem,
            CheckOnInVisible checkOnInvisible, AddScoreViewFactory addScoreViewFactory,
            IDestroyStrategy iDestroyStrategy)
        {
            var pieceFactory = new PieceFactory(gamePhysics, shadowSystem, iDestroyStrategy, checkOnInvisible);
            var spotFactory = new FruitSpotFactory();
            var fruitParticleFactory = new FruitParticleFactory();
            var fruitEffectCreator = new FruitSwipeEffectsCreator(pieceFactory, spotFactory, fruitParticleFactory,
                _fruitsConfig, addScoreViewFactory);

            return fruitEffectCreator;
        }
    }
}