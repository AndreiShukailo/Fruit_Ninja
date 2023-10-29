using System;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.Common.Services.SaveLoad;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.Model
{
    public class PlayerDataModel
    {
        public GameState.Services.GameState GameState { get; set; }
        public int BestScore => _playerProgress.BestScore;
        public bool AllowSpawnHealth { get; private set; }
        public int CurrentScore { get; private set; }
        private int _currentHealthCount;

        private readonly StartPlayerDataConfig _startPlayerDataConfig;
        private readonly ISaveLoadService _saveLoadService;
        private readonly PlayerProgress _playerProgress;

        public event Action<int> OnUpdateScore;
        public event Action<int> OnUpdateBestScore;
        public event Action<int> OnUpdateHealth;

        public event Action<Vector3> OnAddHealth; 


        public PlayerDataModel(StartPlayerDataConfig startPlayerDataConfig, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _startPlayerDataConfig = startPlayerDataConfig;
            _currentHealthCount = _startPlayerDataConfig.StartHealth;
            CurrentScore = _startPlayerDataConfig.StartScore;
            _playerProgress = _saveLoadService.Load() ?? new PlayerProgress();
        }

        public void UpdateScore() =>
            OnUpdateScore?.Invoke(CurrentScore);

        public void UpdateBestScore() =>
            OnUpdateBestScore?.Invoke(BestScore);

        public void UpdateHealth()
        {
            OnUpdateHealth?.Invoke(_currentHealthCount);
            UpdateAllowSpawnHealth();
        }

        public void AddHealth(Vector3 startPosition)
        {
            if (!GameState.IsPlaying) return;
            _currentHealthCount += 1;
            UpdateAllowSpawnHealth();
            OnAddHealth?.Invoke(startPosition);
        }

        public void RemoveHealth()
        {
            if (!GameState.IsPlaying) return;
            _currentHealthCount -= 1;
            if (_currentHealthCount <= 0)
                GameState.SetLoose();

            UpdateHealth();
        }

        public void AddScore(int score)
        {
            CurrentScore += score;
            UpdateScore();

            if (CurrentScore <= BestScore) return;
            _playerProgress.BestScore = CurrentScore;
            _saveLoadService.Save(_playerProgress);
            UpdateBestScore();
        }

        public void Reset()
        {
            _currentHealthCount = _startPlayerDataConfig.StartHealth;
            CurrentScore = _startPlayerDataConfig.StartScore;
            UpdateHealth();
            UpdateScore();
        }

        private void UpdateAllowSpawnHealth() =>
            AllowSpawnHealth = _currentHealthCount < _startPlayerDataConfig.StartHealth;
    }
}