using CodeBase.Services;
using CodeBase.Services.Physics;
using CodeBase.Services.Spawner;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class MainInstaller : MonoInstaller
    {
        public Camera Camera;
        public override void InstallBindings()
        {
            BindInstanceCamera();
            BindCameraWrap();
            BindPhysics();
            BindFruitFactory();
            BindGameMath();
            BindGameSpawner();
        }

        private void BindInstanceCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(Camera)
                .AsSingle();
        }
        
        private void BindCameraWrap()
        {
            Container
                .BindInterfacesAndSelfTo<CameraWrap>()
                .AsSingle();
        }
        
        private void BindPhysics()
        {
            Container
                .BindInterfacesAndSelfTo<GamePhysics>()
                .AsSingle();
        }
        
        private void BindFruitFactory()
        {
            Container
                .Bind<FruitFactory>()
                .AsSingle();
        }
        
        private void BindGameMath()
        {
            Container
                .Bind<GameMath>()
                .AsSingle();
        }
        
        private void BindGameSpawner()
        {
            Container
                .BindInterfacesAndSelfTo<GameSpawner>()
                .AsSingle();
        }
    }
}