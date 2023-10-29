using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
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