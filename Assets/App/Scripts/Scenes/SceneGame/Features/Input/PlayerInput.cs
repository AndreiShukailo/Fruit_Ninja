using App.Scripts.Infrastructure.Installer;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input
{
    public class PlayerInput : ITickable
    {
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
            if (UnityEngine.Input.GetMouseButtonDown(0))
                _startMousePosition = UnityEngine.Input.mousePosition;

            if (UnityEngine.Input.GetMouseButton(0))
            {
                MousePosition = UnityEngine.Input.mousePosition;
                var moveMouseDistance = Vector3.Distance(MousePosition, _startMousePosition);
                if (moveMouseDistance > _inputConfig.MinDistanceToSlide)
                    IsSlide = true;
            }
        }
    }
}