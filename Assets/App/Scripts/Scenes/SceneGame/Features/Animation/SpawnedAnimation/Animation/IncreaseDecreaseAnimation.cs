using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class IncreaseDecreaseAnimation : AnimationTweenBase
    {
        [SerializeField] private float _increaseDecreaseDuration = 2f;

        [SerializeField] [Range(1f, 2f)] private float _maxScale = 1.2f;
        [SerializeField] [Range(0.1f, 1f)] private float _minScale = 0.9f;

        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            transform.localScale = Vector2.one * _minScale;
            _animation
                .Append(transform.DOScale(Vector2.one * _maxScale, _increaseDecreaseDuration))
                .Append(transform.DOScale(Vector2.one * _minScale, _increaseDecreaseDuration))
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}