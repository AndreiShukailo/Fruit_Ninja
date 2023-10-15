using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class HeartAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration = 1f;
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            transform.localScale = Vector2.zero;
            _animation
                .Append(transform.DOScale(Vector2.one, _duration));
        }
    }
}