using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.Common.Buttons.Animation
{
    public class ButtonAnimation : ButtonAnimationBase
    {
        private const float Duration = 0.2f;
        private const float MinHeight = 104f;
        private const float MaxHeight = 110f;
        [SerializeField] private RectTransform _rectTransform;
        private Sequence _animation;
        
        protected override void OnButtonDown()
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            _animation.Append(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, MinHeight), Duration))
                .SetUpdate(true);
        }

        protected override void OnButtonUp()
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            _animation.Append(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, MaxHeight), Duration))
                .SetUpdate(true);
        }
    }
}