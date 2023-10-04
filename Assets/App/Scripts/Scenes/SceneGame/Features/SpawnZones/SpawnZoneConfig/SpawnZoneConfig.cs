using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones.SpawnZoneConfig
{
    [CreateAssetMenu(fileName = "SpawnZoneConfig", menuName = "StaticData/SpawnZoneConfig")]
    public class SpawnZoneConfig : ScriptableObject
    {
        [Range(0, 1f)] public float Priority = 1f;
        public SpawnZoneType Type;
    }
}