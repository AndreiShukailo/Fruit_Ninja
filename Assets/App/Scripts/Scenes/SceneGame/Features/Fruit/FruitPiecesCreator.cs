using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Fruit.Pieces;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruit
{
    public class FruitPiecesCreator : MonoBehaviour
    {
        [SerializeField] private List<Piece> _pieces;
        private PieceFactory _pieceFactory;

        public void Construct(PieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
        }

        public void CreatePieces(Vector2 cut, Vector2 position)
        {
            foreach (var piece in _pieces)
            {
                _pieceFactory.Create(piece, position, cut);
            }
        }
    }
}