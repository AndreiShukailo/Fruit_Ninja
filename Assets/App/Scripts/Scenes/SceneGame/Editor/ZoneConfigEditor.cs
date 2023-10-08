using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEditor;

namespace App.Scripts.Scenes.SceneGame.Editor
{
    [CustomEditor(typeof(SpawnZoneConfig))]
    public class ZoneConfigEditor : UnityEditor.Editor
    {

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
    }
}