using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common.Buttons;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.GameState.View;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class GameViewInstaller : MonoInstaller
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private ButtonsContainerView _buttonsContainerView;
        [SerializeField] private PausePopupView _pausePopupView;
        [SerializeField] private RestartPopupView _restartPopupView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(new AddScoreViewFactory(_scoreView));
            serviceContainer.SetServiceSelf(_buttonsContainerView);
            serviceContainer.SetServiceSelf(_pausePopupView);
            serviceContainer.SetServiceSelf(_restartPopupView);
            
            
            var playerDataModel = serviceContainer.Get<PlayerDataModel>();
            _healthView.Construct(playerDataModel, new HeartFactory());
            _scoreView.Construct(playerDataModel);
        }
    }
}