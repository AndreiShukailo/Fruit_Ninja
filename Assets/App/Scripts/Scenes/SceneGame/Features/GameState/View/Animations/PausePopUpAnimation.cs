using System;
using System.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.View.Animations
{
    public class PausePopUpAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private CanvasGroup _canvasGroup;

        public async void StartAnimation(Action onAnimationEnd)
        {
            await ShowAnimationAsync(onAnimationEnd);
        }

        private async Task ShowAnimationAsync(Action onAnimationEnd)
        {
            _canvasGroup.alpha = 0f;
            float elapsedTime = 0f;
            while (elapsedTime < _duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / _duration);
                _canvasGroup.alpha = alpha;
                elapsedTime += 0.02f;
                await Task.Delay(TimeSpan.FromSeconds(0.02f));
            }
            _canvasGroup.alpha = 1f;
            onAnimationEnd?.Invoke();
        }
    }
}