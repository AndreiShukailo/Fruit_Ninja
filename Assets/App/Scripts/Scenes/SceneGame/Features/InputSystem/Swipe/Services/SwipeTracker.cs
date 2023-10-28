using System;
using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
{
    public class SwipeTracker : ITickable
    {
        public event Action OnEmty;
        
        private readonly PlayerInput _input;
        private readonly CameraWrap _cameraWrap;
        private readonly TrailRenderer _swipe;
        private readonly List<ISwipeable> _swipeables;
        private readonly List<ISwipeable> _toRemove;
        private Vector3 _lastPositions;
        private Vector2 _сut;

        public SwipeTracker(TrailRenderer swipePrefab, PlayerInput input, CameraWrap cameraWrap)
        {
            _swipeables = new();
            _toRemove = new();
            _input = input;
            _cameraWrap = cameraWrap;
            _swipe = Object.Instantiate(swipePrefab);
            _swipe.emitting = false;
        }

        public void Tick()
        {
            _swipe.transform.position = GetTrackPosition();
            
            foreach (var swipeable in _toRemove)
                _swipeables.Remove(swipeable);

            _toRemove.Clear();
            
            if (_input.IsSlide)
            {
                _swipe.emitting = true;
                foreach (var swipeableComponent in _swipeables)
                {
                    var rect = swipeableComponent.SpriteRenderer.GetRect();
                    if (rect.Contains(_swipe.transform.position))
                    {
                        var cutDirection = GetCutDirection();
                        swipeableComponent.Swipe(cutDirection);
                    }
                }

                _lastPositions = _swipe.transform.position;
            }

            if (!_input.IsButtonDown)
                _swipe.emitting = false;
            
            if (_swipeables.Count <= 0)
                OnEmty?.Invoke();
        }


        public void Add(ISwipeable swaipable) =>
            _swipeables.Add(swaipable);

        public void Remove(ISwipeable swaipable) =>
            _toRemove.Add(swaipable);

        private Vector2 GetCutDirection()
        {
            var heading = _lastPositions - _swipe.transform.position;
            var distance = heading.magnitude;
            if (distance == 0f)
                return Vector2.up;
            return (heading / distance).normalized;
        }

        private Vector3 GetTrackPosition() =>
            _cameraWrap.ScreenToWorldPoint(_input.MousePosition);
    }
}