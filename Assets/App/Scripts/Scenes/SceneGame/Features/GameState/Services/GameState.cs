using System;
using App.Scripts.Infrastructure.GameState;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Factory;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.Services
{
    public class GameState
    {
        private const string MenuSceneName = "Menu";
        private readonly GameSpawner _gameSpawner;
        private readonly PlayerDataModel _playerDataModel;
        private readonly SwipeTracker _swipeTracker;
        private readonly GameStateMachine _gameStateMachine;

        public event Action<int, int> OnLoose;

        public bool IsPlaying { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsSamuraiMode { get; private set; }

        private int _packDelayFactor;
        private int _packCountFactor;
        private SamuraiFruitFactory _samuraiFruitFactory;

        public GameState(GameSpawner gameSpawner, PlayerDataModel playerDataModel, StatableContainer statableContainer,
            SwipeTracker swipeTracker, GameStateMachine gameStateMachine)
        {
            _gameSpawner = gameSpawner;
            _playerDataModel = playerDataModel;
            _swipeTracker = swipeTracker;
            _gameStateMachine = gameStateMachine;
            statableContainer.SetState(this);
            _gameSpawner.GameState = this;
            _playerDataModel.GameState = this;
        }

        public void ExitToMenu()
        {
            _gameSpawner.StopSpawn();
            _gameStateMachine.Enter<LoadLevelState, string>(MenuSceneName);
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
            IsPaused = true;
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }

        public void StartSamuraiMode(int packDelayFactor,
            int packCountFactor,
            SamuraiFruitFactory samuraiFruitFactory)
        {
            _packCountFactor = packCountFactor;
            _packDelayFactor = packDelayFactor;
            _samuraiFruitFactory = samuraiFruitFactory;
            _gameSpawner.StopSpawn();
            _swipeTracker.OnEmty += OnStartSamuraiMod;
        }

        private void OnStartSamuraiMod()
        {
            _swipeTracker.OnEmty -= OnStartSamuraiMod;
            if (!IsPlaying) return;
            IsSamuraiMode = true;
            _gameSpawner.StartSamuraiMode(_packDelayFactor, _packCountFactor, _samuraiFruitFactory);
            _gameSpawner.StartSpawning();
        }

        public void StopSamuraiMod()
        {
            IsSamuraiMode = false;
            _gameSpawner.StopSpawn();
            if (IsPlaying)
                _swipeTracker.OnEmty += OnStopSamuraiMod;
        }

        private void OnStopSamuraiMod()
        {
            _gameSpawner.StopSamuraiMode(_packDelayFactor, _packCountFactor);
            _gameSpawner.StartSpawning();
            _swipeTracker.OnEmty -= OnStopSamuraiMod;
        }

        private void ShowLoosePopup()
        {
            if (IsPlaying || IsPaused) return;
            _swipeTracker.OnEmty -= ShowLoosePopup;
            OnLoose?.Invoke(_playerDataModel.CurrentScore, _playerDataModel.BestScore);
        }
    }
}