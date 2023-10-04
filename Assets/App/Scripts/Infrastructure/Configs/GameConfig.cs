using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.SpawnZoneConfig;
using UnityEngine;

namespace App.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Range(1f, 20f)]
        public float Gravity = 10f;
        public List<GameObject> FruitPrefabs;
        public List<SpawnZoneConfig> Zones;
    }
}