using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject
{
    public class SpawnedObjectFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly SpawnedAnimationConfig _spawnedAnimationConfig;

        public SpawnedObjectFactory(ServiceContainer serviceContainer, SpawnedAnimationConfig spawnedAnimationConfig)
        {
            _serviceContainer = serviceContainer;
            _spawnedAnimationConfig = spawnedAnimationConfig;
        }

        public void Create(Spawned prefab, Vector3 position, Vector3 startVelocity)
        {
            var spawned = Object.Instantiate(prefab);

            var physics = _serviceContainer.Get<GamePhysics>();
            var cameraWrap = _serviceContainer.Get<CameraWrap>();
            var shadowSystem = _serviceContainer.Get<ShadowSystem>();
            var swipeTracker = _serviceContainer.Get<SwipeTracker>();
            spawned.Construct(physics, cameraWrap, shadowSystem, _spawnedAnimationConfig, swipeTracker, position,
                _serviceContainer);
            spawned.transform.position = position;
            spawned.Physical.AddVelocity(startVelocity);
        }
    }
}