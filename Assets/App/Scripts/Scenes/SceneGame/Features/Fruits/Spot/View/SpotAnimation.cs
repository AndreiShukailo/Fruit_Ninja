using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Spot.View
{
    public class SpotAnimation : AnimationTweenBase
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            _animation.Append(_spriteRenderer.DOFade(0, 3f))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}