using System.Collections.Generic;
using System.Reflection;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public abstract class SpawnedObjectContainer : MonoBehaviour
    {
        [Range(0, 100f)] public int PriorityInPack;
        [Range(0, 6)] public int MaxCountInPack;
        public List<UnitView> Units;
        public SpawnedObjectFactoryBase SpawnedObjectFactoryBase;

        public void Construct(SpawnedObjectFactoryBase factory) =>
            SpawnedObjectFactoryBase = factory;
    }
}