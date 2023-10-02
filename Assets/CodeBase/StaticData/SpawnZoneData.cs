using CodeBase.Services.Spawner.Zone;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "SpawnZoneData", menuName = "StaticData/SpawnZoneData")]
    public class SpawnZoneData : ScriptableObject
    {
        [Range(0, 1f)] public float Priority = 1f;
        public SpawnZoneType Type;
    }
}