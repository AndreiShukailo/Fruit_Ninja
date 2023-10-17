using System;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.Common.Services.SaveLoad;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Config;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.Model
{
    public class PlayerDataModel
    {
        public GameState.Services.GameState GameState { get; set; }
        public int BestScore => _playerProgress.BestScore;
        public int CurrentScore { get; private set; }
        private int _currentHealthCount;

        private readonly StartPlayerDataConfig _startPlayerDataConfig;
        private readonly ISaveLoadService _saveLoadService;
        private readonly PlayerProgress _playerProgress;

        public event Action<int, int> OnUpdateScore;
        public event Action<int> OnUpdateHealth;


        public PlayerDataModel(StartPlayerDataConfig startPlayerDataConfig, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _startPlayerDataConfig = startPlayerDataConfig;
            _currentHealthCount = _startPlayerDataConfig.StartHealth;
            CurrentScore = _startPlayerDataConfig.StartScore;
            _playerProgress = _saveLoadService.Load() ?? new PlayerProgress();
        }

        public void UpdateScore() =>
            OnUpdateScore?.Invoke(CurrentScore, BestScore);

        public void UpdateHealth() =>
            OnUpdateHealth?.Invoke(_currentHealthCount);

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
            if (CurrentScore > BestScore)
            {
                _playerProgress.BestScore = CurrentScore;
                _saveLoadService.Save(_playerProgress);
            }

            UpdateScore();
        }

        public void Reset()
        {
            _currentHealthCount = _startPlayerDataConfig.StartHealth;
            CurrentScore = _startPlayerDataConfig.StartScore;
            UpdateHealth();
            UpdateScore();
        }
    }
}