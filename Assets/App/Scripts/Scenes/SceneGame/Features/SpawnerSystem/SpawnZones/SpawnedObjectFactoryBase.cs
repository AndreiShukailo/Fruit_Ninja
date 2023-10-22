using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public abstract class SpawnedObjectFactoryBase
    {
        private readonly GamePhysics _gamePhysics;
        private readonly IDestroyStrategy _destroyStrategy;
        private readonly CheckOnInVisible _checkOnInVisible;
        private readonly SwipeTracker _swipeTracker;
        private readonly ISwipeStrategy _swipeStrategy;
        private readonly ShadowSystem.Services.ShadowSystem _shadowSystem;

        protected SpawnedObjectFactoryBase(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem)
        {
            _gamePhysics = gamePhysics;
            _destroyStrategy = destroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _swipeTracker = swipeTracker;
            _swipeStrategy = swipeStrategy;
            _shadowSystem = shadowSystem;
        }

        public void Create(UnitView prefab, float mass, Vector3 position, Vector3 startVelocity)
        {
            var view = Object.Instantiate(prefab);
            view.transform.position = position;
            var componentContainer = new ComponentContainer(view);
            var physicalComponent = new PhysicalComponent(_gamePhysics, mass)
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new CheckOnInvisibleComponent(_destroyStrategy, _checkOnInVisible, view));
            componentContainer.AddComponent(new SwipableComponent(_swipeTracker, _swipeStrategy, view));
            componentContainer.AddComponent(new ShadowedComponent(_shadowSystem, view));
            view.Animator.StartAnimations();
        }
    }
}