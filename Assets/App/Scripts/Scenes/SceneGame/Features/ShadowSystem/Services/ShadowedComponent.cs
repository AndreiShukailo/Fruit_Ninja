using App.Scripts.Scenes.SceneGame.Features.Common;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services
{
    public class ShadowedComponent : Component, ISpriteRenderer
    {
        public SpriteRenderer SpriteRenderer => _unitView.SpriteRenderer;
        private readonly ShadowSystem _shadowSystem;
        private readonly UnitView _unitView;

        public ShadowedComponent(ShadowSystem shadowSystem, UnitView unitView)
        {
            _shadowSystem = shadowSystem;
            _unitView = unitView;
            _shadowSystem.Add(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _shadowSystem.Remove(this);
        }
    }
}