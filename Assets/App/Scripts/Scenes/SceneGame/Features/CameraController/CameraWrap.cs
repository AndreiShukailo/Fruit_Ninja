using System;
using App.Scripts.Infrastructure.Installer;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.CameraController
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
        
        public float HalfWidth =>
            OrthographicSize * Aspect;

        public float RightBoarder =>
            _camera.transform.position.x + OrthographicSize * _camera.aspect;

        public float LeftBoarder =>
            _camera.transform.position.x - OrthographicSize * _camera.aspect;

        public float BottomBoarder =>
            _camera.transform.position.y - OrthographicSize;

        private readonly Camera _camera;
        public float PrevAspect;


        public CameraWrap(Camera camera, PrevAspectSaver prevAspectSaver)
        {
            _camera = camera;
            PrevAspect = prevAspectSaver.PrevAspect;
        }

        public event Action OnAspectChanged;
        
        public void Tick()
        {
            if (!(Math.Abs(PrevAspect - Aspect) > 0f)) return;
            OnAspectChanged?.Invoke();
            PrevAspect = Aspect;
        }
    }
}