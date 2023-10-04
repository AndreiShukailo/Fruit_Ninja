using App.Scripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace App.Scripts.Infrastructure.Installer
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(ServiceContainer serviceContainer);
    }
}