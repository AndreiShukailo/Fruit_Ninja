using System;
using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Editor
{
    [CustomEditor(typeof(SpawnZoneConfig))]
    public class ZoneConfigEditor : UnityEditor.Editor
    {
        public float PrevAspect;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SpawnZoneConfig config = (SpawnZoneConfig)target;
            if (config.MaxAngle < config.MinAngle)
            {
                config.MaxAngle = config.MinAngle;
                config.MinAngle = config.MaxAngle;
            }
        }

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnZoneConfig config, GizmoType gizmoType)
        {
            if (Camera.main == null) return;
            var aspect = Camera.main.aspect;

            if (!Application.isPlaying)
            {
                if (config.PrevAspect == 0) config.PrevAspect = Camera.main.aspect;

                if (CameraChangeAspect(config.PrevAspect))
                {
                    SpawnZonePositionScaler.UpdateZonePosition(config.PrevAspect, config, aspect);
                    config.PrevAspect = aspect;
                }
            }

            Gizmos.color = Color.red;
            var transform = config.transform;
            var pos = transform.position;
            Gizmos.DrawRay(pos, SpawnZoneConfig.GetLeftVectorRotation(transform, config.Lenght));
            Gizmos.DrawRay(pos, SpawnZoneConfig.GetRightVectorRotation(transform, config.Lenght));
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(pos, config.MaxAngle.GetVectorRotation() * 3f);
            Gizmos.DrawRay(pos, config.MinAngle.GetVectorRotation() * 3f);
        }

        private static bool CameraChangeAspect(float prevAspect)
        {
            Camera main;
            return (main = Camera.main) != null && Math.Abs(prevAspect - main.aspect) != 0;
        }
    }
}