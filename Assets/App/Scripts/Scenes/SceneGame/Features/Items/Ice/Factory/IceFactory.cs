using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Ice.Factory
{
    public class IceFactory : SpawnedObjectMagneticFactoryBase
    {
        public IceFactory(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy, CheckOnInVisible checkOnInVisible,
            SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy, ShadowSystem.Services.ShadowSystem shadowSystem,
            AllSpawnedObjects allSpawnedObjects) : base(gamePhysics, destroyStrategy, checkOnInVisible, swipeTracker,
            swipeStrategy, shadowSystem, allSpawnedObjects)
        {
        }
    }
}