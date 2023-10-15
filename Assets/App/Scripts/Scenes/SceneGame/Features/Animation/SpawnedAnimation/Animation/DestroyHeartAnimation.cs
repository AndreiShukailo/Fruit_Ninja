using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class DestroyHeartAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            _animation
                .Append(transform.DOScale(Vector3.zero, _duration))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}