using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
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