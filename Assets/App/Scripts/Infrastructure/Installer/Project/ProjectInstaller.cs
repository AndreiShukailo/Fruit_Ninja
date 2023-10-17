using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Infrastructure.Installer.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public int TargetFrameRate = 60;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetService<ISaveLoadService, SaveLoadService>(new SaveLoadService());
            Application.targetFrameRate = TargetFrameRate;
        }
    }
}