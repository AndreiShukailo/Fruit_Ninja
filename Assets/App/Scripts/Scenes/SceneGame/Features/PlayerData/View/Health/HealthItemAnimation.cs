using System;
using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Heart.View
{
    public class HealthItemAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        
        public void StartAnimation(Vector3 endPosition, Action onAnimationEnd)
        {
            _animation = DOTween.Sequence();
            _animation.Append(transform.DOMove(endPosition, _duration))
                .AppendCallback(() => onAnimationEnd?.Invoke());
        }
        
        public override void StartAnimation()
        {
        }
    }
}