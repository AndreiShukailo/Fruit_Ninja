using System;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.Services
{
    public class GameState
    {
        private readonly GameSpawner _gameSpawner;
        private readonly PlayerDataModel _playerDataModel;
        private readonly SwipeTracker _swipeTracker;

        public event Action<int, int> OnLoose;

        public bool IsPlaying { get; private set; }
        private bool _isPaused;

        public GameState(GameSpawner gameSpawner, PlayerDataModel playerDataModel, PlayerInput playerInput,
            SwipeTracker swipeTracker)
        {
            _gameSpawner = gameSpawner;
            _playerDataModel = playerDataModel;
            _swipeTracker = swipeTracker;
            playerInput.GameState = this;
            _gameSpawner.GameState = this;
            _playerDataModel.GameState = this;
        }

        private void ShowLoosePopup()
        {
            if (IsPlaying || _isPaused) return;
            _swipeTracker.OnEmty -= ShowLoosePopup;
            OnLoose?.Invoke(_playerDataModel.CurrentScore, _playerDataModel.BestScore);
        }

        public void SetLoose()
        {
            IsPlaying = false;
            _gameSpawner.StopSpawn();
        }

        public void StartPlay()
        {
            IsPlaying = true;
            _playerDataModel.Reset();
            _gameSpawner.Reset();
            _swipeTracker.OnEmty += ShowLoosePopup;
        }

        public void SetPause()
        {
            _isPaused = true;
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            _isPaused = false;
            Time.timeScale = 1f;
        }
    }
}