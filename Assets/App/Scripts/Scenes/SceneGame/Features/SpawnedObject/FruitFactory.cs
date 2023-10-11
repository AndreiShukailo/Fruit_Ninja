using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject
{
    public class FruitFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly AnimationConfig _animationConfig;

        public FruitFactory(ServiceContainer serviceContainer, AnimationConfig animationConfig)
        {
            _serviceContainer = serviceContainer;
            _animationConfig = animationConfig;
        }

        public void Create(Fruit prefab, Vector3 position, Vector3 startVelocity)
        {
            var fruit = Object.Instantiate(prefab);
            fruit.Transform.position = position;
            var physics = _serviceContainer.Get<GamePhysics>();
            var checkOnInvisible = _serviceContainer.Get<CheckOnInVisible>();
            var shadowSystem = _serviceContainer.Get<ShadowSystem>();
            var swipeTracker = _serviceContainer.Get<SwipeTracker>();
            var spotFactory = _serviceContainer.Get<SpotFactory>();
            var pieceFactory = _serviceContainer.Get<PieceFactory>();
            fruit.Construct(physics, checkOnInvisible, swipeTracker, shadowSystem, _animationConfig, pieceFactory,
                spotFactory);
            fruit.Velocity = startVelocity;
        }
    }
}