using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class Spot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ObjectAnimator _animator;

        public void Construct(Color color)
        {
            _spriteRenderer.color = color;
            _animator.StartAnimations();
        }
    }
}