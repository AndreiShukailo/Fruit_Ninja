using System;
using App.Scripts.Scenes.Common;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class PlayerDataModel
    {
        public GameState GameState { get; set; }
        public int BestScore => _playerData.BestScore;
        public int CurrentScore { get; private set; }
        private int _currentHealthCount;
        private readonly PlayerData _playerData;

        public event Action<int, int> OnUpdateScore;
        public event Action<int> OnUpdateHealth;


        public PlayerDataModel(PlayerData playerData)
        {
            _playerData = playerData;
            _currentHealthCount = _playerData.StartHealth;
            CurrentScore = _playerData.StartScore;
        }

        public void UpdateScore() =>
            OnUpdateScore?.Invoke(CurrentScore, _playerData.BestScore);

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
            if (CurrentScore > _playerData.BestScore)
                _playerData.BestScore = CurrentScore;

            UpdateScore();
        }

        public void Reset()
        {
            _currentHealthCount = _playerData.StartHealth;
            CurrentScore = _playerData.StartScore;
            UpdateHealth();
            UpdateScore();
        }
    }
}