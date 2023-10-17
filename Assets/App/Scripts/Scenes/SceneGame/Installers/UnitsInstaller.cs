using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
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
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class UnitsInstaller : MonoInstaller
    {
        public FruitsConfig FruitsConfig;

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
            var fruitEffectCreator = GetFruitSwipeEffectsCreator(gamePhysics, shadowSystem, checkOnInvisible, addScoreViewFactory, simpleDestroyStrategy);
            var fruitSwipeStrategy = GetFruitSwipeStrategy(fruitEffectCreator, simpleDestroyStrategy, playerDataModel, comboSystem);
            
            
            var fruitDestroyStrategy = new FruitDestroyStrategy(playerDataModel);
            serviceContainer.SetServiceSelf(new FruitFactory(gamePhysics, fruitDestroyStrategy, checkOnInvisible,
                swipeTracker, fruitSwipeStrategy, shadowSystem));
        }

        private static FruitSwipeStrategy GetFruitSwipeStrategy(FruitSwipeEffectsCreator fruitEffectCreator,
            IDestroyStrategy iDestroyStrategy, PlayerDataModel playerDataModel, ComboSystem comboSystem) =>
            new(fruitEffectCreator, iDestroyStrategy, playerDataModel, comboSystem);

        private FruitSwipeEffectsCreator GetFruitSwipeEffectsCreator(GamePhysics gamePhysics, ShadowSystem shadowSystem,
            CheckOnInVisible checkOnInvisible, AddScoreViewFactory addScoreViewFactory, IDestroyStrategy iDestroyStrategy)
        {
            var pieceFactory = new PieceFactory(gamePhysics, shadowSystem, iDestroyStrategy, checkOnInvisible);
            var spotFactory = new FruitSpotFactory();
            var fruitParticleFactory = new FruitParticleFactory();
            var fruitEffectCreator = new FruitSwipeEffectsCreator(pieceFactory, spotFactory, fruitParticleFactory,
                FruitsConfig, addScoreViewFactory);

            return fruitEffectCreator;
        }
    }
}