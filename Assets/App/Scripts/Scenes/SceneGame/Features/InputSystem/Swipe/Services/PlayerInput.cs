using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
{
    public class PlayerInput : ITickable
    {
        public GameState.Services.GameState GameState { get; set; }
        public Vector3 MousePosition { get; private set; }
        public bool IsSlide { get; private set; }

        private readonly InputConfig _inputConfig;
        private Vector3 _startMousePosition;

        public PlayerInput(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }

        public void Tick()
        {
            IsSlide = false;
            if(!GameState.IsPlaying) return;
            if (UnityEngine.Input.GetMouseButtonDown(0))
                _startMousePosition = UnityEngine.Input.mousePosition;

            if (!UnityEngine.Input.GetMouseButton(0)) return;
            MousePosition = UnityEngine.Input.mousePosition;
            var moveMouseDistance = Vector3.Distance(MousePosition, _startMousePosition);
            if (moveMouseDistance > _inputConfig.MinDistanceToSwipe)
                IsSlide = true;
            _startMousePosition = MousePosition;
        }
    }
}