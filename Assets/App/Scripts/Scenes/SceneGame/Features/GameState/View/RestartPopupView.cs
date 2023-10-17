using System.Collections;
using App.Scripts.Scenes.SceneGame.Features.Common.View.Animations;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.View
{
    public class RestartPopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _bestScore;

        [SerializeField] private ShowPopUpAnimation _showAnimarion;
        [SerializeField] private HidePopUpAnimation _hideAnimarion;

        public void Activate(int currentScore, int bestScore)
        {
            _popup.SetActive(true);
            _showAnimarion.StartAnimation();
            StartCoroutine(NumberAnimation(0, currentScore, 1f, _currentScore));
            StartCoroutine(NumberAnimation(0, bestScore, 1f, _bestScore));
        }

        public void Deactivate()
        {
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