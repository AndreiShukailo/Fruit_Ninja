using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Player;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class GameViewInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDataView _playerDataView;
        [SerializeField] private StartPlayerDataConfig _startPlayerDataConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(new AddScoreViewFactory(serviceContainer.Get<CameraWrap>(), _playerDataView));
            serviceContainer.SetServiceSelf(new HeartFactory());
            serviceContainer.SetServiceSelf(new PlayerDataModel(_startPlayerDataConfig, serviceContainer.Get<ISaveLoadService>()));
            _playerDataView.Construct(serviceContainer.Get<PlayerDataModel>(),
                serviceContainer.Get<HeartFactory>());
        }
    }
}