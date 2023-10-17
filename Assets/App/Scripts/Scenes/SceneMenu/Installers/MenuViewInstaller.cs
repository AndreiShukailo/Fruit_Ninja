using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.Common.Buttons;
using App.Scripts.Scenes.Common.Services.SaveLoad;
using App.Scripts.Scenes.SceneMenu.Feature.BestScore;
using App.Scripts.Scenes.SceneMenu.Feature.Buttons;
using App.Scripts.Scenes.SceneMenu.Feature.MainScreen;
using UnityEngine;

namespace App.Scripts.Scenes.SceneMenu.Installers
{
    public class MenuViewInstaller : MonoInstaller
    {
        [SerializeField] private ButtonsContainerView _buttonsContainer;
        [SerializeField] private BestScoreView _bestScoreView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var gameStateMachine = serviceContainer.Get<GameStateMachine>();
            var saveLoadService = serviceContainer.Get<ISaveLoadService>();
            
            var menuScreenController =
                new MenuScreenController(_buttonsContainer, _bestScoreView, gameStateMachine, saveLoadService);
            menuScreenController.IniView();
        }
    }
}