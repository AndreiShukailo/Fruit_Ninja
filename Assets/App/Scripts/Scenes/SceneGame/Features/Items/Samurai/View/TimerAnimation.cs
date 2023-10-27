using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.View
{
    public class TimerAnimation : AnimationTweenBase
    {
        public void StartAnimation(TMP_Text _timer, int value)
        {
            _animation = DOTween.Sequence();
            _animation
                .Append(_timer.transform.DOScale(Vector3.one * 1.5f, 0.3f))
                .AppendCallback(() => _timer.text = value.ToString())
                .Append(_timer.transform.DOScale(Vector3.one, 0.3f));
        }

        public override void StartAnimation()
        {
        }
    }
}