using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.SpawnZoneConfig;
using UnityEngine;

namespace App.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Physics")]
        [Range(1f, 20f)]
        public float Gravity = 10f;

        [Header("Spawner")]
        [Range(0.1f, 1f)]
        public float FruitDelay = 0.5f;
        [Range(2, 5)]
        public int MaxPackCount = 5;
        [Range(1f, 5f)]
        public float PackDelay = 3f;
        public List<GameObject> FruitPrefabs;
        public List<SpawnZoneConfig> Zones;
    }
}