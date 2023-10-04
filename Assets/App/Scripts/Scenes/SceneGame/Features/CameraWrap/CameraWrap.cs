using System;
using App.Scripts.Infrastructure.Installer;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.CameraWrap
{
    public class CameraWrap : ITickable
    {
        public Vector3 Position =>
            _camera.transform.position;

        public float PixelsHeight =>
            _camera.pixelHeight;

        public float PixelsWidth =>
            _camera.pixelWidth;

        public float PixelsPerUnit =>
            PixelsHeight / Height;

        public float Aspect =>
            _camera.aspect;

        public float OrthographicSize =>
            _camera.orthographicSize;

        public float Height =>
            OrthographicSize * 2f;

        public float Width =>
            OrthographicSize * Aspect * 2f;

        public float HalfWidth =>
            OrthographicSize * Aspect;

        public float RightBoarder =>
            _camera.transform.position.x + OrthographicSize * _camera.aspect;

        public float LeftBoarder =>
            _camera.transform.position.x - OrthographicSize * _camera.aspect;

        public float BottomBoarder =>
            _camera.transform.position.y - OrthographicSize;

        private readonly UnityEngine.Camera _camera;
        private float _prevAspect;


        public CameraWrap(UnityEngine.Camera camera)
        {
            _camera = camera;
            _prevAspect = Aspect;
        }

        public event Action OnAspectChanged;
        
        public void Tick()
        {
            if (!(Math.Abs(_prevAspect - Aspect) > 0f)) return;
            _prevAspect = Aspect;
            OnAspectChanged?.Invoke();
        }
    }
}