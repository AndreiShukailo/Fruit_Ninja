using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class SpotFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly AnimationConfig _animationConfig;

        public SpotFactory(ServiceContainer serviceContainer, AnimationConfig animationConfig)
        {
            _serviceContainer = serviceContainer;
            _animationConfig = animationConfig;
        }

        public void CreateSpot(Spot prefab, Color color, Transform transform)
        {
            var spot = Object.Instantiate(prefab);
            var transform1 = spot.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            spot.Construct(_animationConfig, color);
        }
    }
}