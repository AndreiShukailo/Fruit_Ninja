using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services
{
    public class CheckOnInVisible : ITickable
    {
        private readonly CameraWrap _cameraWrap;
        private readonly List<ICheckVisible> _objects;
        private readonly List<ICheckVisible> _toRemove;

        public CheckOnInVisible(CameraWrap cameraWrap)
        {
            _cameraWrap = cameraWrap;
            _objects = new();
            _toRemove = new();
        }

        public void Tick()
        {
            foreach (var checkVisible in _objects)
                Update(checkVisible);
            foreach (var checkVisible in _toRemove)
                _objects.Remove(checkVisible);
            _toRemove.Clear();
        }

        public void AddObject(ICheckVisible checkVisible) =>
            _objects.Add(checkVisible);

        public void RemoveObject(ICheckVisible checkVisible) =>
            _toRemove.Add(checkVisible);

        private void Update(ICheckVisible checkVisible)
        {
            if (checkVisible.SpriteRenderer == null) return;
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
            return rect.xMin > _cameraWrap.LeftBoarder && rect.xMax < _cameraWrap.RightBoarder &&
                   rect.yMin > _cameraWrap.BottomBoarder;
        }
    }
}