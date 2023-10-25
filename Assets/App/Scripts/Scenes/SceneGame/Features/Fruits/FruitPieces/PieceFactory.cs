using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.FruitPieces
{
    public class PieceFactory
    {
        private readonly GamePhysics _gamePhysics;
        private readonly ShadowSystem.Services.ShadowSystem _shadowSystem;
        private readonly IDestroyStrategy _iDestroyStrategy;
        private readonly CheckOnInVisible _checkOnInVisible;
        private readonly AllSpawnedObjects _allSpawnedObjects;

        public PieceFactory(GamePhysics gamePhysics, ShadowSystem.Services.ShadowSystem shadowSystem,
            IDestroyStrategy iDestroyStrategy, CheckOnInVisible checkOnInVisible, AllSpawnedObjects allSpawnedObjects)
        {
            _gamePhysics = gamePhysics;
            _shadowSystem = shadowSystem;
            _iDestroyStrategy = iDestroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _allSpawnedObjects = allSpawnedObjects;
        }

        public void Create(UnitView piecePrefab, Transform transform, Vector3 startVelocity, float mass)
        {
            var view = Object.Instantiate(piecePrefab);
            var transform1 = view.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            transform1.localScale = transform.localScale;
            var componentContainer = new ComponentContainer(view, _allSpawnedObjects);
            var physicalComponent = new PhysicalComponent(_gamePhysics, mass)
            {
                Velocity = startVelocity
            };
            componentContainer.AddComponent(physicalComponent);
            componentContainer.AddComponent(new ShadowedComponent(_shadowSystem, view));
            var checkOnInvisibleComponent = new CheckOnInvisibleComponent(_iDestroyStrategy, _checkOnInVisible, view);
            componentContainer.AddComponent(checkOnInvisibleComponent);
            checkOnInvisibleComponent.IsVisible = true;
            view.Animator.StartAnimations();
        }
    }
}