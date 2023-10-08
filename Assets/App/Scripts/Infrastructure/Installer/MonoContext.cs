using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Infrastructure.Installer
{
    public abstract class MonoContext : MonoBehaviour
    {
        public List<MonoInstaller> Installers = new();
        protected readonly List<ITickable> Tickables = new();
        
        private void Update()
        {
            foreach (var tickable in Tickables) 
                tickable.Tick();
        }
    }
}