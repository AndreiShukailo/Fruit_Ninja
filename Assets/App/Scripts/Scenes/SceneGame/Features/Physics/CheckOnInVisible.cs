using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class CheckOnInVisible : ITickable
    {
        private readonly CameraWrap _cameraWrap;
        private readonly List<ICheckVisible> _objects;

        public CheckOnInVisible(CameraWrap cameraWrap)
        {
            _cameraWrap = cameraWrap;
            _objects = new();
        }

        public void Tick()
        {
            foreach (var checkVisible in _objects)
                Update(checkVisible);
        }

        public void AddObject(ICheckVisible checkVisible) =>
            _objects.Add(checkVisible);

        public void RemoveObject(ICheckVisible checkVisible) =>
            _objects.Remove(checkVisible);

        private void Update(ICheckVisible checkVisible)
        {
            var rect = checkVisible.SpriteRenderer.GetRect();
            if (checkVisible.IsVisible == false)
                CheckOnVisible(rect, checkVisible);
            else
                CheckOnInvisible(rect, checkVisible);
        }

        private void CheckOnInvisible(Rect rect, ICheckVisible checkVisible)
        {
            if (!OutOfCamera(rect)) return;
            checkVisible.OnInvisible();
        }

        private bool OutOfCamera(Rect rect)
        {
            return rect.xMin > _cameraWrap.RightBoarder || rect.xMax < _cameraWrap.LeftBoarder ||
                   rect.yMax < _cameraWrap.BottomBoarder;
        }

        private void CheckOnVisible(Rect rect, ICheckVisible checkVisible)
        {
            if (InCameraVisible(rect))
                checkVisible.IsVisible = true;
        }

        private bool InCameraVisible(Rect rect)
        {
            return rect.xMax > _cameraWrap.LeftBoarder && rect.xMin < _cameraWrap.RightBoarder &&
                   rect.yMax > _cameraWrap.BottomBoarder;
        }
    }
}