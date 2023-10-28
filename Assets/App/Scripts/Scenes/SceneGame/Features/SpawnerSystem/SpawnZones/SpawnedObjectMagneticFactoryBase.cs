using System;
using System.Threading.Tasks;
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

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public class SpawnedObjectMagneticFactoryBase : SpawnedObjectFactoryBase
    {
        protected SpawnedObjectMagneticFactoryBase(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem, AllSpawnedObjects allSpawnedObjects) : base(gamePhysics,
            destroyStrategy, checkOnInVisible, swipeTracker, swipeStrategy, shadowSystem, allSpawnedObjects)
        {
        }

        public override async Task<ComponentContainer> Create(UnitView prefab, float mass, Vector3 position, Vector3 startVelocity, float swipeAfter = 0)
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
            componentContainer.AddComponent(new MagneticComponent());
            view.Animator.StartAnimations();
            await Task.Delay(TimeSpan.FromSeconds(swipeAfter));
            var swipeComponent = new SwipableComponent(_swipeTracker, _swipeStrategy);
            componentContainer.AddComponent(swipeComponent);
            swipeComponent.Register();
            return componentContainer;
        }
    }
}