using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Common.View.Animations
{
    public class ShowPopUpAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        public override void StartAnimation()
        {
            _canvasGroup.alpha = 0;
            _animation = DOTween.Sequence();
            _animation
                .Append(_canvasGroup.DOFade(1f, _duration));
        }
    }
}