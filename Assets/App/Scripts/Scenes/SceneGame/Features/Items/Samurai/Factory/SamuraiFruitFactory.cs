using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Factory
{
    public class SamuraiFruitFactory : FruitFactory
    {
        public SamuraiFruitFactory(GamePhysics gamePhysics, IDestroyStrategy destroyStrategy,
            CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy,
            ShadowSystem.Services.ShadowSystem shadowSystem, AllSpawnedObjects allSpawnedObjects) : base(gamePhysics,
            destroyStrategy, checkOnInVisible, swipeTracker, swipeStrategy, shadowSystem, allSpawnedObjects)
        {
        }
    }
}