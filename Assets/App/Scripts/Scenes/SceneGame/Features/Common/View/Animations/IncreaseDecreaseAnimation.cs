using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Common.View.Animations
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
                .Append(transform.DOScale(Vector2.one * _minScale, _increaseDecreaseDuration));
        }
    }
}