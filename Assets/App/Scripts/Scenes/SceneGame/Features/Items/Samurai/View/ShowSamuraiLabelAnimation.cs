using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.View
{
    public class ShowSamuraiLabelAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration;
        public void StartAnimation(SpriteRenderer spriteRenderer, TMP_Text text)
        {
            spriteRenderer.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            _animation = DOTween.Sequence();
            _animation
                .Append(spriteRenderer.DOFade(1, _duration))
                .Join(text.DOFade(1, _duration));
        }

        public override void StartAnimation()
        {
        }
    }
}