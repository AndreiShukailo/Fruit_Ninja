using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Shadow.Mono
{
    public class Shadowed : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;

        private ShadowSystem _shadowSystem;

        public void Construct(ShadowSystem shadowSystem, Vector3 startPosition)
        {
            _shadowSystem = shadowSystem;
            _shadowSystem.Add(this, startPosition);
        }

        private void OnDestroy() =>
            _shadowSystem.Remove(this);
    }
}