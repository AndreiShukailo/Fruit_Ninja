using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private GameObject _trackPrefab;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new PlayerInput(_inputConfig));
            
            var playerInput = serviceContainer.Get<PlayerInput>();
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetITickableAndSelf(new SwipeTracker(_trackPrefab, playerInput, cameraWrap));
        }
    }
}