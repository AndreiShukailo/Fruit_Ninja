using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private HeartView _heartPrefab;
        [SerializeField] private RectTransform _heartContainer;

        private Stack<HeartView> _hearts;
        private PlayerDataModel _playerDataModel;
        private HeartFactory _heartFactory;

        public void Construct(PlayerDataModel playerDataModel, HeartFactory heartFactory)
        {
            _heartFactory = heartFactory;
            _playerDataModel = playerDataModel;
            _playerDataModel.OnUpdateHealth += OnUpdateHealth;
            _playerDataModel.OnAddHealth += OnAddHealth;
            _playerDataModel.UpdateHealth();
        }

        private void OnAddHealth(Vector3 startPosition) => 
            AddHeart(startPosition);

        private void OnUpdateHealth(int healthCount)
        {
            if (_hearts == null)
                CreateHearts(healthCount);

            if (_hearts?.Count > healthCount)
                RemoveHeart();

            if (healthCount == 0)
                _hearts = null;
        }

        private void RemoveHeart() =>
            _hearts?.Pop().Deconstruct();

        private void AddHeart(Vector3 startPosition) => 
            _hearts?.Push(_heartFactory.CreateWhenAdd(_heartPrefab, _heartContainer, startPosition, GetContainerPosition()));

        private void CreateHearts(int healthCount)
        {
            _hearts = new();
            for (int i = 0; i < healthCount; i++)
                _hearts?.Push(_heartFactory.Create(_heartPrefab, _heartContainer));
        }

        private Vector3 GetContainerPosition()
        {
            var lastHeartPosition = _hearts.First().transform.position;
           return new Vector3(lastHeartPosition.x - 1, lastHeartPosition.y, lastHeartPosition.z);
        }
    }
}