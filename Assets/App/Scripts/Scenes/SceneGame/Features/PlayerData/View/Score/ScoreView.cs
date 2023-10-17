using System.Collections;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private float _numberAnimationDuration = 1f;
        [SerializeField] private TMP_Text _currenScore;
        [SerializeField] private TMP_Text _bestScore;
        private PlayerDataModel _playerDataModel;

        public void Construct(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
            _playerDataModel.OnUpdateScore += OnUpdateScore;
            _playerDataModel.UpdateScore();
        }
        
        private void OnUpdateScore(int score, int bestScore)
        {
            StartCoroutine(NumberAnimation(int.Parse(_currenScore.text), score, _numberAnimationDuration,
                _currenScore));
            StartCoroutine(NumberAnimation(int.Parse(_bestScore.text), bestScore, _numberAnimationDuration,
                _bestScore));
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