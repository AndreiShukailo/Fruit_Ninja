using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private HeartView _heartPrefab;
        [SerializeField] private RectTransform _heartContainer;

        private Queue<HeartView> _hearts;
        private PlayerDataModel _playerDataModel;
        private HeartFactory _heartFactory;

        public void Construct(PlayerDataModel playerDataModel, HeartFactory heartFactory)
        {
            _heartFactory = heartFactory;
            _playerDataModel = playerDataModel;
            _playerDataModel.OnUpdateHealth += OnUpdateHealth;
            _playerDataModel.UpdateHealth();
        }

        private void OnUpdateHealth(int healthCount)
        {
            if (_hearts == null)
                CreateHearts(healthCount);

            if (_hearts?.Count < healthCount)
                AddHeart();

            if (_hearts?.Count > healthCount)
                RemoveHeart();

            if (healthCount == 0)
                _hearts = null;
        }

        private void RemoveHeart() =>
            _hearts?.Dequeue().Deconstruct();

        private void AddHeart() => 
            _hearts?.Enqueue(_heartFactory.Create(_heartPrefab, _heartContainer));

        private void CreateHearts(int healthCount)
        {
            _hearts = new();
            for (int i = 0; i < healthCount; i++)
                _hearts?.Enqueue(_heartFactory.Create(_heartPrefab, _heartContainer));
        }
    }
}