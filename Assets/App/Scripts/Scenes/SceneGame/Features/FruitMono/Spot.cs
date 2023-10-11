using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class Spot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ObjectdAnimator _animator;

        public void Construct(AnimationConfig animationConfig, Color color)
        {
            _spriteRenderer.color = color;
            _animator.StartAnimations(animationConfig);
        }
    }
}