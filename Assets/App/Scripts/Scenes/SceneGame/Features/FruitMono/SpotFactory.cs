using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class SpotFactory
    {
        private readonly ServiceContainer _serviceContainer;

        public SpotFactory(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void CreateSpot(UnitView prefab, Color color, Transform transform)
        {
            var view = Object.Instantiate(prefab);
            view.SpriteRenderer.color = color;
            var transform1 = view.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            view.Animator.StartAnimations();
        }
    }
}