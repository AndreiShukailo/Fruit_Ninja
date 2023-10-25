using System;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using UnityEngine;
using Object = UnityEngine.Object;
using Task = System.Threading.Tasks.Task;

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
        private readonly AllSpawnedObjects _allSpawnedObjects;

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

    public class SpawnedObjectMagneticFactoryBase : SpawnedObjectFactoryBase
    {
        private readonly GamePhysics _gamePhysics;
        private readonly IDestroyStrategy _destroyStrategy;
        private readonly CheckOnInVisible _checkOnInVisible;
        private readonly SwipeTracker _swipeTracker;
        private readonly ISwipeStrategy _swipeStrategy;
        private readonly ShadowSystem.Services.ShadowSystem _shadowSystem;
        private readonly AllSpawnedObjects _allSpawnedObjects;
        public SpawnedObjectMagneticFactoryBase(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem, AllSpawnedObjects allSpawnedObjects) : base(gamePhysics,
            destroyStrategy, checkOnInVisible, swipeTracker, swipeStrategy, shadowSystem, allSpawnedObjects)
        {
            _gamePhysics = gamePhysics;
            _destroyStrategy = destroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _swipeTracker = swipeTracker;
            _swipeStrategy = swipeStrategy;
            _shadowSystem = shadowSystem;
            _allSpawnedObjects = allSpawnedObjects;
        }

        public override async Task Create(UnitView prefab, float mass, Vector3 position, Vector3 startVelocity, float swipeAfter = 0)
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
            componentContainer.AddComponent(new MagneticComponent(mass));
            view.Animator.StartAnimations();
            await Task.Delay(TimeSpan.FromSeconds(swipeAfter));
            componentContainer.AddComponent(new SwipableComponent(_swipeTracker, _swipeStrategy, view));
        }
    }
}