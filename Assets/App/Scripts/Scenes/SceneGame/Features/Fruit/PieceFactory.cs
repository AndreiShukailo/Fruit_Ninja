using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Fruit.Pieces;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruit
{
    public class PieceFactory
    {
        private readonly ServiceContainer _serviceContainer;

        public PieceFactory(ServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void Create(Piece piecePrefab, Vector3 startPosition, Vector3 startVelocity)
        {
            var piece = Object.Instantiate(piecePrefab);
            var physics = _serviceContainer.Get<GamePhysics>();
            var cameraWrap = _serviceContainer.Get<CameraWrap>();
            var shadowSystem = _serviceContainer.Get<ShadowSystem>();
            piece.Construct(physics, cameraWrap, shadowSystem, startPosition);
            piece.transform.position = startPosition;
            piece.Physical.AddVelocity(startVelocity);
        }
    }
}