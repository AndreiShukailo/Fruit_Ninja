using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Shadow
{
    public class ShadowSystem : ITickable
    {
        private readonly Dictionary<Shadowed, SpriteRenderer> _shadoweds;

        private readonly Transform _shadowContainer;
        private readonly SpriteRenderer _shadowPref;
        private readonly ShadowConfig _config;

        public ShadowSystem(ShadowConfig config)
        {
            _shadoweds = new();
            _config = config;
            _shadowContainer = _config.ShadowContainer;
            _shadowPref = _config.ShadowPrefab;
        }

        public void Tick()
        {
            foreach (var shadowed in _shadoweds)
                Update(shadowed.Key, shadowed.Value);
        }

        public void Add(Shadowed shadowed, Vector3 startPosition)
        {
            var newSpriteRenderer = Object.Instantiate(_shadowPref, _shadowContainer);
            newSpriteRenderer.transform.position = GetPositionByShadowOffset(startPosition);
            newSpriteRenderer.sprite = shadowed.SpriteRenderer.sprite;
            newSpriteRenderer.color = new Color(0, 0, 0, _config.Transparent);
            _shadoweds.Add(shadowed, newSpriteRenderer);
        }

        public void Remove(Shadowed shadowed)
        {
            Object.Destroy(_shadoweds[shadowed]);
            _shadoweds.Remove(shadowed);
        }


        private void Update(Shadowed shadowed, SpriteRenderer spriteRenderer)
        {
            var spriteRendererTransform = spriteRenderer.transform;
            var shadowedTransform = shadowed.transform;
            spriteRendererTransform.localScale = shadowedTransform.localScale;
            spriteRendererTransform.rotation = shadowedTransform.rotation;
            spriteRenderer.transform.position = GetTransformByScale(shadowed);
        }

        private Vector3 GetTransformByScale(Shadowed shadowed)
        {
            var transform = shadowed.transform;
            var scale = (transform.localScale.x - 1f) * _config.ScaleFactor;
            var position = transform.position;
            return new Vector3(position.x + _config.ShadowOffset + scale, position.y - _config.ShadowOffset - scale,
                0f);
        }

        private Vector3 GetPositionByShadowOffset(Vector3 position) =>
            new(position.x + _config.ShadowOffset, position.y - _config.ShadowOffset, 0f);
    }
}