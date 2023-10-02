using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/GameData")]
    public class GameData : ScriptableObject
    {
        [Range(1f, 20f)]
        public float Gravity = 10f;
        public List<GameObject> FruitPrefabs;
        public List<SpawnZoneData> Zones;
    }
}