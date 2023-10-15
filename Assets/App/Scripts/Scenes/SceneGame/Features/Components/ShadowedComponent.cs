using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
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