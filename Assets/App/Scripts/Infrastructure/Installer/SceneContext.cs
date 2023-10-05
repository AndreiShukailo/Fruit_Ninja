using System.Collections.Generic;
using System.Linq;
using App.Scripts.Infrastructure.ServiceLocator;

namespace App.Scripts.Infrastructure.Installer
{
    public class SceneContext : MonoContext
    {
        public void Setup(ServiceContainer projectContainer)
        {
            var container = BuildContainer(projectContainer);
            var sceneTickables = GetSceneTickables(projectContainer, container);
            Tickables.AddRange(sceneTickables);
        }

        private ServiceContainer BuildContainer(ServiceContainer projectContainer)
        {
            var container = new ServiceContainer();
            container.PushContainer(projectContainer);
            foreach (var installer in Installers)
                installer.InstallBindings(container);

            return container;
        }

        private IEnumerable<ITickable> GetSceneTickables(ServiceContainer projectContainer, ServiceContainer container)
        {
            var sceneTickables = container
                .GetServices<ITickable>()
                .Where(p => !projectContainer.GetServices<ITickable>().Contains(p));
            return sceneTickables;
        }
    }
}