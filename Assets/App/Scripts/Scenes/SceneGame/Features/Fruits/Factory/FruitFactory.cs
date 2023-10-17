using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Strategies;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Factory
{
    public class FruitFactory
    {
        private readonly GamePhysics _gamePhysics;
        private readonly FruitDestroyStrategy _fruitDestroyStrategy;
        private readonly CheckOnInVisible _checkOnInVisible;
        private readonly SwipeTracker _swipeTracker;
        private readonly FruitSwipeStrategy _fruitSwipeStrategy;
        private readonly ShadowSystem.Services.ShadowSystem _shadowSystem;

        public FruitFactory(GamePhysics gamePhysics, FruitDestroyStrategy fruitDestroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, FruitSwipeStrategy fruitSwipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem)
        {
            _gamePhysics = gamePhysics;
            _fruitDestroyStrategy = fruitDestroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _swipeTracker = swipeTracker;
            _fruitSwipeStrategy = fruitSwipeStrategy;
            _shadowSystem = shadowSystem;
        }

        public void Create(UnitView prefab, Vector3 position, Vector3 startVelocity)
        {
            var view = Object.Instantiate(prefab);
            view.transform.position = position;
            var componentContainer = new ComponentContainer(view);
            var physicalComponent = new PhysicalComponent(_gamePhysics)
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new CheckOnInvisibleComponent(_fruitDestroyStrategy, _checkOnInVisible, view));
            componentContainer.AddComponent(new SwipableComponent(_swipeTracker, _fruitSwipeStrategy, view));
            componentContainer.AddComponent(new ShadowedComponent(_shadowSystem, view));
            view.Animator.StartAnimations();
        }
    }
}