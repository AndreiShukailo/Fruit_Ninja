using System.Collections.Generic;
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
        private List<ISwipeable> _swipeables;
        private Queue<Vector2> _lastPositions;
        private Vector2 _сut;

        public SwipeTracker(GameObject swipePrefab, PlayerInput input, CameraWrap cameraWrap)
        {
            _swipeables = new();
            _input = input;
            _cameraWrap = cameraWrap;
            _swipe = Object.Instantiate(swipePrefab);
            _lastPositions = new Queue<Vector2>(2);
            DisableTrack();
        }

        public void Tick()
        {
            if (_input.IsSlide)
            {
                EnableTrack();
                _swipe.transform.position = GetTrackPosition();
                UpdateLastPositions();
                foreach (var swipeableComponent in _swipeables)
                {
                    var rect = swipeableComponent.Rect;
                    if (rect.Contains(_swipe.transform.position))
                    {
                        var cutDirection = GetCutDirection();
                        swipeableComponent.Swipe(cutDirection, _swipe.transform.position);
                    }
                }
            }
            else
            {
                DisableTrack();
            }
        }


        public void Add(ISwipeable swaipable) =>
            _swipeables.Add(swaipable);

        public void Remove(ISwipeable swaipable) =>
            _swipeables.Remove(swaipable);

        private Vector2 GetCutDirection()
        {
            var last = _lastPositions.Dequeue();
            var preLast = _lastPositions.Dequeue();
            _lastPositions.Enqueue(last);
            _lastPositions.Enqueue(preLast);
            var heading = preLast - last;
            var distance = heading.magnitude;
            return heading / distance;
        }

        private void UpdateLastPositions()
        {
            _lastPositions.Enqueue(_swipe.transform.position);
            if (_lastPositions.Count > 2)
                _lastPositions.Dequeue();
        }

        private Vector3 GetTrackPosition() =>
            _cameraWrap.ScreenToWorldPoint(_input.MousePosition);

        private void EnableTrack() =>
            _swipe.gameObject.SetActive(true);

        private void DisableTrack() =>
            _swipe.gameObject.SetActive(false);
    }
}