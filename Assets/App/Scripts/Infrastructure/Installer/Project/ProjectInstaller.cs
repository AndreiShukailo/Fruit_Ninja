using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.ServiceLocator;

namespace App.Scripts.Infrastructure.Installer.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public GameConfig GameConfig;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetServiceSelf(GameConfig);
        }
    }
}