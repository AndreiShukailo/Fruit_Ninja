using System.Collections.Generic;
using App.Scripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace App.Scripts.Infrastructure.Installer
{
    public abstract class MonoContext : MonoBehaviour
    {
        public List<MonoInstaller> Installers = new();
        public readonly List<ITickable> Tickables = new();
        
        private void Update()
        {
            foreach (var tickable in Tickables) 
                tickable.Tick();
        }

        protected ServiceContainer BuildContainer()
        {
            var container = new ServiceContainer();
            foreach (var installer in Installers) 
                installer.InstallBindings(container);

            return container;
        }
    }
}