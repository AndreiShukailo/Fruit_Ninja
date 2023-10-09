using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class IncreaseDecreaseAnimation : SpawnedAnimationTweenBase
    {
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            transform.localScale = Vector2.one * _config.MinScale;
            _animation
                .Append(transform.DOScale(Vector2.one * _config.MaxScale, _config.IncreaseDecreaseDuration))
                .Append(transform.DOScale(Vector2.one * _config.MinScale, _config.IncreaseDecreaseDuration))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}