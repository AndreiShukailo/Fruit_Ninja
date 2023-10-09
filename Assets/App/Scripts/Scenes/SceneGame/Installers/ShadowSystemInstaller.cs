using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Shadow;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class ShadowSystemInstaller : MonoInstaller
    {
        public ShadowConfig ShadowConfig;
        
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new ShadowSystem(ShadowConfig));
        }
    }
}