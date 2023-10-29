using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.Common;
using App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Services
{
    public class ShadowSystem : ITickable
    {
        private readonly Dictionary<ISpriteRenderer, SpriteRenderer> _shadows;

        private readonly Transform _shadowContainer;
        private readonly SpriteRenderer _shadowPref;
        private readonly ShadowConfig _config;

        public ShadowSystem(ShadowConfig config)
        {
            _shadows = new();
            _config = config;
            _shadowContainer = _config.ShadowContainer;
            _shadowPref = _config.ShadowPrefab;
        }

        public void Tick()
        {
            foreach (var shadowed in _shadows)
                Update(shadowed.Key, shadowed.Value);
        }

        public void Add(ISpriteRenderer iSpriteRenderer)
        {
            _shadows.Add(iSpriteRenderer, CreateShadow(iSpriteRenderer));
        }

        public void Remove(ISpriteRenderer spriteRenderer)
        {
            var removed = _shadows[spriteRenderer];
            _shadows.Remove(spriteRenderer);
            Object.Destroy(removed.gameObject);
        }


        private void Update(ISpriteRenderer iSpriteRenderer, SpriteRenderer spriteRenderer)
        {
            var spriteRendererTransform = spriteRenderer.transform;
            var shadowedTransform = iSpriteRenderer.SpriteRenderer.transform;
            spriteRendererTransform.localScale = shadowedTransform.localScale;
            spriteRendererTransform.rotation = shadowedTransform.rotation;
            spriteRenderer.transform.position = GetTransformByScale(iSpriteRenderer);
        }

        public SpriteRenderer CreateShadow(ISpriteRenderer iSpriteRenderer)
        {
            var newSpriteRenderer = Object.Instantiate(_shadowPref, _shadowContainer);
            newSpriteRenderer.transform.position =
                GetPositionByShadowOffset(iSpriteRenderer.SpriteRenderer.transform.position);
            newSpriteRenderer.sprite = iSpriteRenderer.SpriteRenderer.sprite;
            newSpriteRenderer.color = new Color(0, 0, 0, _config.Transparent);
            return newSpriteRenderer;
        }

        private Vector3 GetTransformByScale(ISpriteRenderer iSpriteRenderer)
        {
            var transform = iSpriteRenderer.SpriteRenderer.transform;
            var scale = (transform.localScale.x - 1f) * _config.ScaleFactor;
            var position = transform.position;
            return new Vector3(position.x + _config.ShadowOffset + scale, position.y - _config.ShadowOffset - scale,
                0f);
        }

        private Vector3 GetPositionByShadowOffset(Vector3 position) =>
            new(position.x + _config.ShadowOffset, position.y - _config.ShadowOffset, 0f);
    }
}