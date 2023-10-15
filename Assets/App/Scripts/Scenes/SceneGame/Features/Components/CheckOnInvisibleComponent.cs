using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class CheckOnInvisibleComponent : Component, ICheckVisible
    {
        private IDestroyStrategy _destroyStrategy;
        private readonly CheckOnInVisible _checkOnInVisible;
        private readonly UnitView _unitView;
        public SpriteRenderer SpriteRenderer => _unitView.SpriteRenderer;
        public bool IsVisible { get; set; }

        public CheckOnInvisibleComponent(IDestroyStrategy destroyStrategy, CheckOnInVisible checkOnInVisible, UnitView unitView)
        {
            _destroyStrategy = destroyStrategy;
            _checkOnInVisible = checkOnInVisible;
            _unitView = unitView;
            _checkOnInVisible.AddObject(this);
        }

        public void OnInvisible()
        {
            _destroyStrategy.OnDestroy(ComponentContainer);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _checkOnInVisible.RemoveObject(this);
        }
    }
}