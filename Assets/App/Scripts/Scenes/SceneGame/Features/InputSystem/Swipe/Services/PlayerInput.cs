using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
{
    public class PlayerInput : ITickable, IStatable
    {
        private GameState.Services.GameState _gameState;
        public Vector3 MousePosition { get; private set; }
        public bool IsSlide { get; private set; }
        public bool IsButtonDown { get; private set; }
        private bool _isBlocked;

        private readonly InputConfig _inputConfig;
        private Vector3 _startMousePosition;

        public PlayerInput(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }

        public void Tick()
        {
            IsSlide = false;
            IsButtonDown = false;
            MousePosition = Input.mousePosition;
            if (!_gameState.IsPlaying || _gameState.IsPaused) return;
            if (Input.GetMouseButtonDown(0))
            {
                _isBlocked = false;
                _startMousePosition = Input.mousePosition;
            }


            if (!Input.GetMouseButton(0)) return;
            var moveMouseDistance = Vector3.Distance(MousePosition, _startMousePosition);
            if (moveMouseDistance >= _inputConfig.MinDistanceToSwipe)
                if(!_isBlocked)
                    IsSlide = true;
            _startMousePosition = MousePosition;
            if(!_isBlocked)
                IsButtonDown = true;
        }

        public void SetBlocked() =>
            _isBlocked = true;

        public void SetGameState(GameState.Services.GameState gameState) =>
            _gameState = gameState;
    }
}