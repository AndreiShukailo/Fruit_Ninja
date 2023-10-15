using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common;
using UnityEngine;

namespace App.Scripts.Infrastructure.Installer.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public GameConfig GameConfig;
        public PlayerData PlayerData;
        public int TargetFrameRate = 60;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(GameConfig);
            serviceContainer.SetServiceSelf(PlayerData);
            Application.targetFrameRate = TargetFrameRate;
        }
    }
}