using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common.Services.SaveLoad;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Config;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        [SerializeField] private StartPlayerDataConfig _startPlayerDataConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var saveLoadService = serviceContainer.Get<ISaveLoadService>();
            serviceContainer.SetServiceSelf(new PlayerDataModel(_startPlayerDataConfig, saveLoadService));
        }
    }
}