using App.Scripts.Scenes.SceneGame.Features.Input;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Player;
using App.Scripts.Scenes.SceneGame.Features.Spawner;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class GameState
    {
        private readonly GameSpawner _gameSpawner;
        private readonly PlayerDataModel _playerDataModel;
        private readonly PlayerInput _playerInput;
        private readonly SwipeTracker _swipeTracker;
        private readonly RestartPopupView _restartPopupView;

        public bool IsPlaying { get; private set; }

        public GameState(GameSpawner gameSpawner, PlayerDataModel playerDataModel, PlayerInput playerInput,
            SwipeTracker swipeTracker, RestartPopupView restartPopupView)
        {
            _gameSpawner = gameSpawner;
            _playerDataModel = playerDataModel;
            _playerInput = playerInput;
            _swipeTracker = swipeTracker;
            _restartPopupView = restartPopupView;
            _playerInput.GameState = this;
            _gameSpawner.GameState = this;
            _playerDataModel.GameState = this;
        }

        private void ShowLoosePopup()
        {
            if (IsPlaying) return;
            _swipeTracker.OnEmty -= ShowLoosePopup;
            _restartPopupView.Activate(_playerDataModel.CurrentScore, _playerDataModel.BestScore, StartPlay);
        }

        public void SetLoose()
        {
            IsPlaying = false;
            _gameSpawner.StopSpawn();
        }


        public void StartPlay()
        {
            _restartPopupView.Deactivate();
            IsPlaying = true;
            _playerDataModel.Reset();
            _gameSpawner.Reset();
            _swipeTracker.OnEmty += ShowLoosePopup;
        }
    }
}