using System;
using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb.Animation
{
    public class ExplosionAnimation : AnimationTweenBase
    {
        [SerializeField] private float _shakeDuration = 1f;
        [SerializeField] private float _strength = 0.5f;
        [SerializeField] public int _vibrato = 10;
        [SerializeField] public float _randomness = 90f;
        
        public void StartAnimation(Transform bombTransform, Action onAnimationEnd)
        {
            _animation = DOTween.Sequence();
            _animation.Append(bombTransform.DOShakePosition(_shakeDuration, _strength, _vibrato, _randomness))
                .AppendCallback(() => onAnimationEnd?.Invoke());
        }

        public override void StartAnimation()
        {
        }
    }
}