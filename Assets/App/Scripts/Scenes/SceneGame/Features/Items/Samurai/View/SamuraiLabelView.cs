using System;
using System.Globalization;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.View;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services
{
    public class SamuraiLabelView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TMP_Text _timer;
        [SerializeField] private TimerAnimation _timerAnimation;
        [SerializeField] private ShowSamuraiLabelAnimation _showSamuraiLabel;
        [SerializeField] private HideSamuraiLabelAnimation _hideSamuraiLabel;

        public void ShowLabel(int leftTime)
        {
            _showSamuraiLabel.StartAnimation(_spriteRenderer, _timer);
            _timer.text = leftTime.ToString();
        }

        public void UpdateTimer(int value) =>
            _timerAnimation.StartAnimation(_timer, value);

        public void HideLabel() =>
            _hideSamuraiLabel.StartAnimation(_spriteRenderer, _timer);
    }
}