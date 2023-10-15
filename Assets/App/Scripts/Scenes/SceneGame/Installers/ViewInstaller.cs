using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Player;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDataView _playerDataView;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(new AddScoreViewFactory(serviceContainer.Get<CameraWrap>(), _playerDataView));
            serviceContainer.SetServiceSelf(new HeartFactory());
            serviceContainer.SetServiceSelf(new PlayerDataModel(serviceContainer.Get<PlayerData>()));
            _playerDataView.Construct(serviceContainer.Get<PlayerDataModel>(),
                serviceContainer.Get<HeartFactory>());
        }
    }
}