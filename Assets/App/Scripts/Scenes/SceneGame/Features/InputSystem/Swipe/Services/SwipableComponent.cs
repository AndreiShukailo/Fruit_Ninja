using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
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
            RemoveComponent();
        }

        public void RemoveComponent()
        {
            _swipeTracker.Remove(this);
        }
    }
}