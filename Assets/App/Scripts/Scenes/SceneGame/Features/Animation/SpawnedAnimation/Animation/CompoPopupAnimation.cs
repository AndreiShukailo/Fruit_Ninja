using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class CompoPopupAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        public override void StartAnimation()
        {
            _animation.Kill();
            _canvasGroup.alpha = 1f;
            _animation = DOTween.Sequence();
            _animation
                .Append(_canvasGroup.DOFade(0f, _duration))
                .AppendCallback(() => gameObject.SetActive(false));
        }
    }
}