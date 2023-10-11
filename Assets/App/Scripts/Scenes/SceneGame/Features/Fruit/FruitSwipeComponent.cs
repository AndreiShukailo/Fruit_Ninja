using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruit
{
    public class FruitSwipeComponent : MonoBehaviour, ISwipeable
    {
        public Rect Rect => _spriteRenderer.GetRect();

        private SwipeTracker _swipeTracker;
        private SpriteRenderer _spriteRenderer;
        private FruitPiecesCreator _fruitPiecesCreator;

        public virtual void Construct(SwipeTracker swipeTracker, SpriteRenderer spriteRenderer,
            FruitPiecesCreator fruitPiecesCreator)
        {
            _fruitPiecesCreator = fruitPiecesCreator;
            _spriteRenderer = spriteRenderer;
            _swipeTracker = swipeTracker;
            _swipeTracker.Add(this);
        }

        public void Swipe(Vector2 cut, Vector2 position)
        {
            _fruitPiecesCreator.CreatePieces(cut, position);
            Destroy(gameObject);
        }

        private void OnDestroy() =>
            _swipeTracker.Remove(this);
    }
}