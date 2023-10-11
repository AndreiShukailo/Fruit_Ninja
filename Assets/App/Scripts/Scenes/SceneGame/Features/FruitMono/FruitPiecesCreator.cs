using App.Scripts.Scenes.SceneGame.Features.FruitMono.Pieces;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class FruitPiecesCreator : MonoBehaviour
    {
        [SerializeField] private Piece _leftPieces;
        [SerializeField] private Spray _sprayPrefab;
        [SerializeField] private Piece _rightPieces;
        [SerializeField] private Spot _spotPrefab;
        [SerializeField] private Color _fruitColor;
        private PieceFactory _pieceFactory;
        private SpotFactory _spotFactory;

        public void Construct(PieceFactory pieceFactory, SpotFactory spotFactory)
        {
            _pieceFactory = pieceFactory;
            _spotFactory = spotFactory;
        }

        public void CreatePieces(Vector2 cut, Transform transform)
        {
            var spray = Instantiate(_sprayPrefab);
            spray.Construct(_fruitColor, transform.position);
            _pieceFactory.Create(_leftPieces, transform, GetLeftVelocity(cut) * 3f);
            _pieceFactory.Create(_rightPieces, transform, GetRightVelocity(cut) * 3f);
            _spotFactory.CreateSpot(_spotPrefab, _fruitColor, transform);
        }

        private Vector2 GetLeftVelocity(Vector2 cut) =>
            new(-cut.y, cut.x);

        private Vector2 GetRightVelocity(Vector2 cut) =>
            new(cut.y, -cut.x);
    }
}