using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class PlayerDataView : MonoBehaviour
    {
        [SerializeField] private float _numberAnimationDuration = 1f;
        [SerializeField] private TMP_Text _currenScore;
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private HeartView _heartPrefab;
        [SerializeField] private RectTransform _heartContainer;

        private Queue<HeartView> _hearts;
        private PlayerDataModel _playerDataModel;
        private HeartFactory _heartFactory;

        public void Construct(PlayerDataModel playerDataModel, HeartFactory heartFactory)
        {
            _heartFactory = heartFactory;
            _playerDataModel = playerDataModel;
            _playerDataModel.OnUpdateScore += OnUpdateScore;
            _playerDataModel.OnUpdateHealth += OnUpdateHealth;
            _playerDataModel.UpdateHealth();
            _playerDataModel.UpdateScore();
        }

        private void OnUpdateHealth(int healthCount)
        {
            if (_hearts == null)
            {
                _hearts = new();
                for (int i = 0; i < healthCount; i++)
                    _hearts?.Enqueue(_heartFactory.Create(_heartPrefab, _heartContainer));
            }

            if (_hearts?.Count < healthCount)
                _hearts?.Enqueue(_heartFactory.Create(_heartPrefab, _heartContainer));

            if (_hearts?.Count > healthCount)
                _hearts?.Dequeue().Deconstruct();

            if (healthCount == 0)
                _hearts = null;
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