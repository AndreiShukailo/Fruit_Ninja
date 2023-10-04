using System.Collections.Generic;
using System.Linq;
using App.Scripts.Infrastructure.ServiceLocator;

namespace App.Scripts.Infrastructure.Installer
{
    public class SceneContext : MonoContext
    {
        public void Setup(ProjectContext projectContext)
        {
            var container = BuildContainer(projectContext);
            var sceneTickables = GetSceneItickables(projectContext, container);
            Tickables.AddRange(sceneTickables);
        }

        private ServiceContainer BuildContainer(ProjectContext projectContext)
        {
            var container = new ServiceContainer();
            foreach (var installer in projectContext.Installers)
                installer.InstallBindings(container);
            foreach (var installer in Installers)
                installer.InstallBindings(container);

            return container;
        }

        private IEnumerable<ITickable> GetSceneItickables(ProjectContext projectContext, ServiceContainer container)
        {
            var sceneItickables = container
                .GetServices<ITickable>()
                .Where(p => !projectContext.Tickables.Contains(p));
            return sceneItickables;
        }
    }
}