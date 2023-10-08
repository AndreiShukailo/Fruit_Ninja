using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.CameraController
{
    [ExecuteInEditMode]
    public class PrevAspectSaver : MonoBehaviour
    {
        public Camera Camera;
        public float PrevAspect;

        private void Update()
        {
            if (Application.isPlaying) return;
            {
                if (Camera == null)
                    Camera = Camera.main;
                if (Camera != null) PrevAspect = Camera.aspect;
            }
        }
    }
}