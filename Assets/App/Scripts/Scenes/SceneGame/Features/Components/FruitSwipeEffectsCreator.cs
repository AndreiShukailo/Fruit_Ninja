using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Player;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class FruitSwipeEffectsCreator
    {
        private const float PieceVelocityFactor = 2f;
        private readonly PieceFactory _pieceFactory;
        private readonly SpotFactory _spotFactory;
        private readonly FruitParticleFactory _fruitParticleFactory;
        private readonly FruitsConfig _fruitsConfig;
        private readonly AddScoreViewFactory _addScoreViewFactory;

        public FruitSwipeEffectsCreator(PieceFactory pieceFactory, SpotFactory spotFactory,
            FruitParticleFactory fruitParticleFactory, FruitsConfig fruitsConfig, AddScoreViewFactory addScoreViewFactory)
        {
            _pieceFactory = pieceFactory;
            _spotFactory = spotFactory;
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
            _spotFactory.CreateSpot(_fruitsConfig.Spot, fruitConfig.UnitColor, unitView.transform);
            var position = unitView.transform.position;
            _fruitParticleFactory.Create(_fruitsConfig.SprayEffect, position, fruitConfig.UnitColor);
            _addScoreViewFactory.Create(fruitConfig.AddScoreView, position, fruitConfig.AddScore);
        }

        private Vector3 GetLeftVelocity(Vector2 cut) =>
            new Vector3(-cut.y, cut.x, 0f) * PieceVelocityFactor;

        private Vector3 GetRightVelocity(Vector2 cut) =>
            new Vector3(cut.y, -cut.x) * PieceVelocityFactor;
    }
}