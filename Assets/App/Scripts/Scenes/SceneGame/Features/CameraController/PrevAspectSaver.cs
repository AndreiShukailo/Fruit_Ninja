using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.CameraController
{
    [ExecuteInEditMode]
    public class PrevAspectSaver : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public float PrevAspect;

        private void Update()
        {
            if (Application.isPlaying) return;
            {
                if (_camera == null)
                    _camera = Camera.main;
                if (_camera != null) PrevAspect = _camera.aspect;
            }
        }
    }
}