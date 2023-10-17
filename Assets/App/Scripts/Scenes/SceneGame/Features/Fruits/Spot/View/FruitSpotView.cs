using App.Scripts.Scenes.SceneGame.Features.Animation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Spot.View
{
    public class FruitSpotView : MonoBehaviour
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