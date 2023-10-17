using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services
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