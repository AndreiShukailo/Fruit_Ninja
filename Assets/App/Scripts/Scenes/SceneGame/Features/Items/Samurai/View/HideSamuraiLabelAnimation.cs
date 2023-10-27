using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.View
{
    public class HideSamuraiLabelAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;

        public void StartAnimation(SpriteRenderer spriteRenderer, TMP_Text _text)
        {
            _animation = DOTween.Sequence();
            _animation
                .Append(spriteRenderer.DOFade(0, _duration))
                .Join(_text.DOFade(0, _duration))
                .AppendCallback(() =>
                {
                    _text.gameObject.SetActive(false);
                    spriteRenderer.gameObject.SetActive(false);
                });
        }

        public override void StartAnimation()
        {
        }
    }
}