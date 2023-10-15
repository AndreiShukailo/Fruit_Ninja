using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class PieceFactory
    {
        private readonly ServiceContainer _serviceContainer;

        public PieceFactory(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void Create(UnitView piecePrefab, Transform transform, Vector3 startVelocity)
        {
            var view = Object.Instantiate(piecePrefab);
            var transform1 = view.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            transform1.localScale = transform.localScale;
            var componentContainer = new ComponentContainer(view);
            var physicalComponent = new PhysicalComponent(_serviceContainer.Get<GamePhysics>())
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new ShadowedComponent(_serviceContainer.Get<ShadowSystem>(), view));
            componentContainer.AddComponent(new CheckOnInvisibleComponent(
                _serviceContainer.Get<SimpleDestroyStrategy>(), _serviceContainer.Get<CheckOnInVisible>(), view));
            view.Animator.StartAnimations();
        }
    }
}