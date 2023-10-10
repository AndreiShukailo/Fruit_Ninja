using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    public abstract class SwipeableComponent : MonoBehaviour
    {
        public Rect Rect { get; protected set; }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        protected SwipeTracker _swipeTracker;

        public virtual void Construct(SwipeTracker swipeTracker)
        {
            Rect = _spriteRenderer.sprite.rect;
            _swipeTracker = swipeTracker;
        }

        public abstract void OnSwipe();
    }
}