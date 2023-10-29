using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common.Buttons;
using App.Scripts.Scenes.SceneGame.Features.GameState.Services;
using App.Scripts.Scenes.SceneGame.Features.GameState.View;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class EntryPointInstaller : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            var playerInput = serviceContainer.Get<PlayerInput>();
            var swipeTracker = serviceContainer.Get<SwipeTracker>();
            var spawner = serviceContainer.Get<GameSpawner>();
            var gameStateMachine = serviceContainer.Get<GameStateMachine>();
            var retardSystem = serviceContainer.Get<RetardSystem>();
            var magnetSystem = serviceContainer.Get<MagnetSystem>();

            var restartPopupView = serviceContainer.Get<RestartPopupView>();
            var pausePopupView = serviceContainer.Get<PausePopupView>();
            var buttonContainer = serviceContainer.Get<ButtonsContainerView>();
            var samuraiMode = serviceContainer.Get<SamuraiMod>();

            
            var statableContainer = new StatableContainer();
            statableContainer.Add(retardSystem);
            statableContainer.Add(playerInput);
            statableContainer.Add(magnetSystem);
            statableContainer.Add(samuraiMode);
            
            
            GameState gameState = new GameState(spawner, playerDataModel, statableContainer, swipeTracker, gameStateMachine);
            GameStateManager gameStateManager =
                new GameStateManager(gameState, restartPopupView, pausePopupView, buttonContainer);

            gameStateManager.InitView();
            gameState.StartPlay();
        }
    }
}