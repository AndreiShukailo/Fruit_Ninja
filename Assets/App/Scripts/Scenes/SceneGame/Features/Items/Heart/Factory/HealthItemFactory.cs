using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Heart.Factory
{
    public class HealthItemFactory : SpawnedObjectFactoryBase
    {
        public HealthItemFactory(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy, CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy, ShadowSystem.Services.ShadowSystem shadowSystem) : base(gamePhysics, destroyStrategy, checkOnInVisible, swipeTracker, swipeStrategy, shadowSystem)
        {
        }
    }
}