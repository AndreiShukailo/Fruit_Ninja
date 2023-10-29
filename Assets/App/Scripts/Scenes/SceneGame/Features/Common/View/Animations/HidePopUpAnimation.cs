using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Common.View.Animations
{
    public class HidePopUpAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        [SerializeField] private CanvasGroup _canvasGroup;
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            _animation
                .Append(_canvasGroup.DOFade(0f, _duration))
                .AppendCallback(() => gameObject.SetActive(false));
        }
    }
}