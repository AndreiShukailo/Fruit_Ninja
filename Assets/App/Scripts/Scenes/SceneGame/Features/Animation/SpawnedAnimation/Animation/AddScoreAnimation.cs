using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class AddScoreAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        public override void StartAnimation()
        {
            transform.localScale = Vector2.zero;
            _animation = DOTween.Sequence();
            _animation
                .Append(transform.DOScale(Vector3.one * 2f, _duration))
                .Join(transform.DOMove(transform.position + Vector3.up, _duration))
                .Join(_canvasGroup.DOFade(0f, _duration))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}