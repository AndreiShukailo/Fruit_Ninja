using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.BackGround;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Installers
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PrevAspectSaver _prevAspectSaver;
        [SerializeField] private SpriteRenderer _backGround;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            serviceContainer.SetITickableAndSelf(new CameraWrap(_camera, _prevAspectSaver));
            var cameraWrap = serviceContainer.Get<CameraWrap>();
            serviceContainer.SetServiceSelf(new FlexibleBackGround(cameraWrap, _backGround));
            serviceContainer.SetITickableAndSelf(new CheckOnInVisible(cameraWrap));
        }
    }
}