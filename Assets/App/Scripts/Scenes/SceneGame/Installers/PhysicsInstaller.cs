using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Items.Ice;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class PhysicsInstaller : MonoInstaller
    {
        [SerializeField] private PhysicsConfig _physicsConfig;
        [SerializeField] private FrostEffectView _frostEffectView;
        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var gamePhysics = new GamePhysics(_physicsConfig); 
            serviceContainer.SetITickableAndSelf(gamePhysics);
            serviceContainer.SetServiceSelf(new PhysicalStartVelocityCalculator(_physicsConfig));
            serviceContainer.SetITickableAndSelf(new RetardSystem(gamePhysics, _frostEffectView));
        }
    }
}