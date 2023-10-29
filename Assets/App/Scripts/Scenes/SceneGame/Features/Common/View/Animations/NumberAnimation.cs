using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Common.View.Animations
{
    public class NumberAnimation : AnimationTweenBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;

        public void StartAnimation(int endValue)
        {
            _animation.Kill();
            _animation = DOTween.Sequence();
            var startValue = int.Parse(_text.text);
            _animation.Append(_text.DOCounter(startValue, endValue, _duration, false));
        }

        public override void StartAnimation()
        {
        }
    }
}