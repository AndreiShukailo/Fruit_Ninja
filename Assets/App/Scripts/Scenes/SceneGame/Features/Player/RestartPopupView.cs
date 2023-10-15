using System;
using System.Collections;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class RestartPopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private Button _restartButton;

        [SerializeField] private ShowPopUpAnimation _showAnimarion;
        [SerializeField] private HidePopUpAnimation _hideAnimarion;

        public void Activate(int currentScore, int bestScore, Action onRestartButtonClick)
        {
            _restartButton.onClick.AddListener(() => onRestartButtonClick?.Invoke());
            _popup.SetActive(true);
            _showAnimarion.StartAnimation();
            StartCoroutine(NumberAnimation(0, currentScore, 1f, _currentScore));
            StartCoroutine(NumberAnimation(0, bestScore, 1f, _bestScore));
        }

        public void Deactivate()
        {
            _restartButton.onClick.RemoveAllListeners();
            _hideAnimarion.StartAnimation();
        }

        private IEnumerator NumberAnimation(int current, int target, float duration, TMP_Text text)
        {
            var numberDelta = target - current;
            float counterFactor = (Time.deltaTime / duration) * numberDelta;
            var counter = 0;
            if (numberDelta != 0)
                counter = numberDelta / Mathf.Abs(numberDelta);

            var start = current;
            while (current != target)
            {
                current += counter;
                if (!(current >= start + counterFactor)) continue;
                text.text = current.ToString();
                start = current;
                yield return null;
            }

            text.text = current.ToString();
        }
    }
}