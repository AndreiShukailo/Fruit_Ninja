using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject
{
    public class FruitFactory
    {
        private readonly ServiceContainer _serviceContainer;

        public FruitFactory(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void Create(UnitView prefab, Vector3 position, Vector3 startVelocity)
        {
            var view = Object.Instantiate(prefab);
            view.transform.position = position;
            var componentContainer = new ComponentContainer(view);
            var physicalComponent = new PhysicalComponent(_serviceContainer.Get<GamePhysics>())
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new CheckOnInvisibleComponent(
                _serviceContainer.Get<FruitDestroyStrategy>(), _serviceContainer.Get<CheckOnInVisible>(), view));
            componentContainer.AddComponent(new SwipableComponent(_serviceContainer.Get<SwipeTracker>(),
                _serviceContainer.Get<FruitSwipeStrategy>(), view));
            componentContainer.AddComponent(new ShadowedComponent(_serviceContainer.Get<ShadowSystem>(), view));
            view.Animator.StartAnimations();
        }
    }
}