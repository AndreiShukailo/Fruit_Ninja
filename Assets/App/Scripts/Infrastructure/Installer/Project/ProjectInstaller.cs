using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.ServiceLocator;
using UnityEngine.Serialization;

namespace App.Scripts.Infrastructure.Installer.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [FormerlySerializedAs("GameData")] public GameConfig GameConfig;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(GameConfig);
        }
    }
}