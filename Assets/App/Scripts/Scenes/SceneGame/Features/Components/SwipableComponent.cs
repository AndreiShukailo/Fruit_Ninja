using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class SwipableComponent : Component, ISwipeable
    {
        private ISwipeStrategy _swipeStrategy;
        private readonly UnitView _unitView;
        private readonly SwipeTracker _swipeTracker;
        public SpriteRenderer SpriteRenderer => _unitView.SpriteRenderer;

        public SwipableComponent(SwipeTracker swipeTracker, ISwipeStrategy swipeStrategy, UnitView unitView)
        {
            _swipeStrategy = swipeStrategy;
            _unitView = unitView;
            _swipeTracker = swipeTracker;
            _swipeTracker.Add(this);
        }

        public void Swipe(Vector2 cut)
        {
            _swipeStrategy.Swipe(cut, ComponentContainer);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _swipeTracker.Remove(this);
        }
    }
}