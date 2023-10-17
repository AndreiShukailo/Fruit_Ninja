using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Config;
using App.Scripts.Scenes.SceneGame.Features.Fruits.FruitPieces;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Spot;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Spray;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Services
{
    public class FruitSwipeEffectsCreator
    {
        private const float PieceVelocityFactor = 2f;
        private readonly PieceFactory _pieceFactory;
        private readonly FruitSpotFactory _fruitSpotFactory;
        private readonly FruitParticleFactory _fruitParticleFactory;
        private readonly FruitsConfig _fruitsConfig;
        private readonly AddScoreViewFactory _addScoreViewFactory;

        public FruitSwipeEffectsCreator(PieceFactory pieceFactory, FruitSpotFactory fruitSpotFactory,
            FruitParticleFactory fruitParticleFactory, FruitsConfig fruitsConfig, AddScoreViewFactory addScoreViewFactory)
        {
            _pieceFactory = pieceFactory;
            _fruitSpotFactory = fruitSpotFactory;
            _fruitParticleFactory = fruitParticleFactory;
            _fruitsConfig = fruitsConfig;
            _addScoreViewFactory = addScoreViewFactory;
        }

        public void CreateEffect(Vector2 cut, ComponentContainer componentContainer, FruitConfig fruitConfig)
        {
            var unitView = componentContainer.UnitView;
            var fruitVelocity = componentContainer.GetComponent<PhysicalComponent>().Velocity;
            _pieceFactory.Create(fruitConfig.LeftPiece, unitView.transform, fruitVelocity + GetLeftVelocity(cut));
            _pieceFactory.Create(fruitConfig.RightPiece, unitView.transform, fruitVelocity + GetRightVelocity(cut));
            _fruitSpotFactory.CreateSpot(_fruitsConfig.Spot, fruitConfig.UnitColor, unitView.transform);
            var position = unitView.transform.position;
            _fruitParticleFactory.Create(_fruitsConfig.SprayViewEffect, position, fruitConfig.UnitColor);
            _addScoreViewFactory.Create(fruitConfig.AddScoreView, position, fruitConfig.AddScore);
        }

        private Vector3 GetLeftVelocity(Vector2 cut) =>
            new Vector3(-cut.y, cut.x, 0f) * PieceVelocityFactor;

        private Vector3 GetRightVelocity(Vector2 cut) =>
            new Vector3(cut.y, -cut.x) * PieceVelocityFactor;
    }
}