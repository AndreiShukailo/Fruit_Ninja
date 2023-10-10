using App.Scripts.Scenes.SceneGame.Extentions;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config
{
    public class SpawnZoneConfig : MonoBehaviour
    {
        [Range(0f, 20f)] public float Lenght;
        [Range(0f, 1f)] public float Priority = 1f;
        [Range(0, 360f)] public float MaxAngle;
        [Range(0, 360f)] public float MinAngle;
        [HideInInspector] public float PrevAspect;

        public Vector2 GetEndPosition()
        {
            var transform1 = transform;
            var position = transform1.position;
            return new Vector2(position.x, position.y) + GetRightVectorRotation(transform1, Lenght);
        }

        public Vector2 GetStartPosition()
        {
            var transform1 = transform;
            var position = transform1.position;
            return new Vector2(position.x, position.y) + GetLeftVectorRotation(transform1, Lenght);
        }

        public static Vector2 GetRightVectorRotation(Transform transform, float lenght)
        {
            var angle = transform.rotation.eulerAngles.z;
            return angle.GetVectorRotation() * lenght * 0.5f;
        }

        public static Vector2 GetLeftVectorRotation(Transform transform, float lenght)
        {
            var angle = transform.rotation.eulerAngles.z + 180f;
            return angle.GetVectorRotation() * lenght * 0.5f;
        }
    }
}