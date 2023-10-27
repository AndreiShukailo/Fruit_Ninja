using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Fruits.FruitPieces;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Config
{
    public class SamuraiEffectCreator
    {
        private const float PieceVelocityFactor = 2f;
        
        private readonly SamuraiConfig _samuraiConfig;
        private readonly PieceFactory _pieceFactory;

        public SamuraiEffectCreator(SamuraiConfig samuraiConfig, PieceFactory pieceFactory)
        {
            _samuraiConfig = samuraiConfig;
            _pieceFactory = pieceFactory;
        }

        public void Create(Vector2 cut, ComponentContainer componentContainer)
        {
            var unitView = componentContainer.UnitView;
            var physical = componentContainer.GetComponent<PhysicalComponent>();
            _pieceFactory.Create(_samuraiConfig.LeftPiece, unitView.transform, physical.Velocity + GetLeftVelocity(cut), physical.Mass);
            _pieceFactory.Create(_samuraiConfig.RightPiece, unitView.transform, physical.Velocity + GetRightVelocity(cut), physical.Mass);
        }
        
        private Vector3 GetLeftVelocity(Vector2 cut) =>
            new Vector3(-cut.y, cut.x, 0f) * PieceVelocityFactor;

        private Vector3 GetRightVelocity(Vector2 cut) =>
            new Vector3(cut.y, -cut.x) * PieceVelocityFactor;
    }
}