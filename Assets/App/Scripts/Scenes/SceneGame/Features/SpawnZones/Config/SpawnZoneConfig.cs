using System;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config
{
    public class SpawnZoneConfig : MonoBehaviour
    {
        [Range(0f, 20f)] public float Lenght;
        public float Priority = 1f;

        [Range(1, 179)] public int MaxAngle;
        [Range(1, 179)] public int MinAngle;

        private Camera _camera;
        public float PrevAspect;

        public Vector2 GetEndPosition()
        {
            var position = transform.position;
            return new Vector2(position.x, position.y) + GetRightVectorRotation();
        }

        public Vector2 GetStartPosition()
        {
            var position = transform.position;
            return new Vector2(position.x, position.y) + GetLeftVectorRotation();
        }

        private void OnDrawGizmos()
        {
            if (!InitCam()) return;
            var aspect = _camera.aspect;

            if (!Application.isPlaying)
            {
                if (PrevAspect == 0) PrevAspect = _camera.aspect;

                if (CameraChangeAspect())
                {
                    SpawnZonePositionScaler.UpdateZonePosition(PrevAspect, this, aspect);
                    PrevAspect = aspect;
                }
            }

            Gizmos.color = Color.red;

            var pos = transform.position;
            Gizmos.DrawRay(pos, GetLeftVectorRotation());
            Gizmos.DrawRay(pos, GetRightVectorRotation());
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(pos, GetVectorRotation(MinAngle) * 3f);
            Gizmos.DrawRay(pos, GetVectorRotation(MaxAngle) * 3f);
        }

        private Vector2 GetRightVectorRotation()
        {
            var angle = transform.rotation.eulerAngles.z;
            return GetVectorRotation(angle) * Lenght * 0.5f;
        }

        private Vector2 GetLeftVectorRotation()
        {
            var transform1 = transform;
            var angle = transform1.rotation.eulerAngles.z + 180f;
            return GetVectorRotation(angle) * Lenght * 0.5f;
        }

        private bool InitCam()
        {
            if (_camera == null) _camera = Camera.main;
            return _camera != null;
        }

        private bool CameraChangeAspect() =>
            _camera != null && Math.Abs(PrevAspect - _camera.aspect) != 0;

        private Vector2 GetVectorRotation(float angle)
        {
            return new Vector3(Mathf.Cos(angle * Mathf.PI / 180f),
                Mathf.Sin(angle * Mathf.PI / 180f));
        }
    }
}