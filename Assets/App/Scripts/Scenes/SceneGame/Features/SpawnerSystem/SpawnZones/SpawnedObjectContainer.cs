using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public abstract class SpawnedObjectContainer : MonoBehaviour
    {
        [Range(0, 100f)] public int PriorityInPack;
        [Range(0, 100)] public int MaxCountInPack;
        [Range(0, 5)] public float Mass = 1f;
        public List<UnitView> Units;
        public ISpawnedFactory SpawnedObjectFactoryBase;

        public void Construct(ISpawnedFactory factory) =>
            SpawnedObjectFactoryBase = factory;

        public UnitView GetRandomPrefab() =>
            Units[Random.Range(0, Units.Count)];
    }
}