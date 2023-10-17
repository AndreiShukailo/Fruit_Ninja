using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;

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