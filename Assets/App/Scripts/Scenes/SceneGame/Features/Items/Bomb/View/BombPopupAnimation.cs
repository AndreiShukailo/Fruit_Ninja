using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb.View
{
    public class BombPopupAnimation : AnimationTweenBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 1f;
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            transform.localScale = Vector2.zero;
            _animation.Append(transform.DOScale(Vector2.one, _duration))
                .Append(_canvasGroup.DOFade(0, _duration))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}