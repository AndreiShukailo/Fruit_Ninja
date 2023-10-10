using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Shadow.Mono
{
    public class Shadowed : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }
        private ShadowSystem _shadowSystem;

        public void Construct(ShadowSystem shadowSystem, SpriteRenderer spriteRenderer, Vector3 startPosition)
        {
            SpriteRenderer = spriteRenderer;
            _shadowSystem = shadowSystem;
            _shadowSystem.Add(this, startPosition);
        }

        private void OnDestroy() =>
            _shadowSystem.Remove(this);
    }
}