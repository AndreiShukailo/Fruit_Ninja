using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.FruitMono.Pieces;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class PieceFactory
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly AnimationConfig _animationConfig;

        public PieceFactory(ServiceContainer serviceContainer, AnimationConfig animationConfig)
        {
            _serviceContainer = serviceContainer;
            _animationConfig = animationConfig;
        }

        public void Create(Piece piecePrefab, Transform transform, Vector3 startVelocity)
        {
            var piece = Object.Instantiate(piecePrefab);
            piece.Transform.position = transform.position;
            piece.Transform.rotation = transform.rotation;
            var physics = _serviceContainer.Get<GamePhysics>();
            var shadowSystem = _serviceContainer.Get<ShadowSystem>();
            var checkOnInvizible = _serviceContainer.Get<CheckOnInVisible>();
            piece.Construct(physics, checkOnInvizible, shadowSystem, _animationConfig);
            piece.Velocity = startVelocity;
        }
    }
}