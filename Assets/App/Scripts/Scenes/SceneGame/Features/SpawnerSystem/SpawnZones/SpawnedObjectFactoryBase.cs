using System;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Task = System.Threading.Tasks.Task;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public abstract class SpawnedObjectFactoryBase
    {
        protected readonly GamePhysics _gamePhysics;
        protected readonly IDestroyStrategy _destroyStrategy;
        protected readonly CheckOnInVisible _checkOnInVisible;
        protected readonly SwipeTracker _swipeTracker;
        protected readonly ISwipeStrategy _swipeStrategy;
        protected readonly ShadowSystem.Services.ShadowSystem _shadowSystem;
        protected readonly AllSpawnedObjects _allSpawnedObjects;

        protected SpawnedObjectFactoryBase(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem, AllSpawnedObjects allSpawnedObjects)
        {
            _gamePhysics = gamePhysics;
            _destroyStrategy = destroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _swipeTracker = swipeTracker;
            _swipeStrategy = swipeStrategy;
            _shadowSystem = shadowSystem;
            _allSpawnedObjects = allSpawnedObjects;
        }

        public virtual async Task Create(UnitView prefab, float mass, Vector3 position, Vector3 startVelocity,
            float swipeAfter = 0f)
        {
            var view = Object.Instantiate(prefab);
            view.transform.position = position;
            var componentContainer = new ComponentContainer(view, _allSpawnedObjects);
            var physicalComponent = new PhysicalComponent(_gamePhysics, mass)
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new CheckOnInvisibleComponent(_destroyStrategy, _checkOnInVisible, view));
            componentContainer.AddComponent(new ShadowedComponent(_shadowSystem, view));
            view.Animator.StartAnimations();
            await Task.Delay(TimeSpan.FromSeconds(swipeAfter));
            componentContainer.AddComponent(new SwipableComponent(_swipeTracker, _swipeStrategy, view));
        }
    }
}