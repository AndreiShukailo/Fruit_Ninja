using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    public class SwipeTracker : ITickable
    {
        private readonly PlayerInput _input;
        private readonly CameraWrap _cameraWrap;
        private readonly GameObject _swipe;

        public SwipeTracker(GameObject swipePrefab, PlayerInput input, CameraWrap cameraWrap)
        {
            _input = input;
            _cameraWrap = cameraWrap;
            _swipe = Object.Instantiate(swipePrefab);
            DisableTrack();
        }

        public void Tick()
        {
            if (_input.IsSlide)
            {
                EnableTrack();
                _swipe.transform.position = GetTrackPosition();
            }
            else
            {
                DisableTrack();
            }

        }

        private Vector3 GetTrackPosition() =>
            _cameraWrap.ScreenToWorldPoint(_input.MousePosition);

        private void EnableTrack() =>
            _swipe.gameObject.SetActive(true);

        private void DisableTrack() =>
            _swipe.gameObject.SetActive(false);
    }
}