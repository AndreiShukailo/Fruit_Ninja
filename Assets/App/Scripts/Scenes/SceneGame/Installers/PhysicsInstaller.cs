using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class PhysicsInstaller : MonoInstaller
    {
        [SerializeField] private PhysicsConfig PhysicsConfig;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new GamePhysics(PhysicsConfig));
            serviceContainer.SetServiceSelf(new PhysicalStartVelocityCalculator(PhysicsConfig));
        }
    }
}