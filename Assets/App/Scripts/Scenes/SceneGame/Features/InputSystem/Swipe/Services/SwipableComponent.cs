using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
{
    public class SwipableComponent : Component, ISwipeable
    {
        private ISwipeStrategy _swipeStrategy;
        private readonly SwipeTracker _swipeTracker;
        public SpriteRenderer SpriteRenderer => ComponentContainer.UnitView.SpriteRenderer;

        public SwipableComponent(SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy)
        {
            _swipeStrategy = swipeStrategy;
            _swipeTracker = swipeTracker;
        }

        public void Register() =>
            _swipeTracker.Add(this);

        public void Swipe(Vector2 cut)
        {
            _swipeStrategy.Swipe(cut, ComponentContainer);
        }

        public override void OnDestroy()
        {
            RemoveComponent();
            base.OnDestroy();
        }

        public void RemoveComponent()
        {
            _swipeTracker.Remove(this);
        }
    }
}