using System.Collections.Generic;
using CodeBase.Services.Physics;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class FruitFactory : PrefabFactory<GameObject>
    {
        private readonly List<GameObject> _prefabs;
        private readonly GamePhysics _physics;

        public FruitFactory(GamePhysics physics, GameData gameData)
        {
            _physics = physics;
            _prefabs = gameData.FruitPrefabs;
        }

        public GameObject Create(Vector3 position, Vector3 startVelocity)
        {
            var prefab = GetRandomPrefab();
            var temp = Container.InstantiatePrefab(prefab);
            temp.transform.position = position;
            var physical = temp.GetComponent<Physical>();
            _physics.AddObject(physical);
            physical.AddVelocity(startVelocity);

            return temp;
        }

        private GameObject GetRandomPrefab() =>
            _prefabs[Random.Range(0, _prefabs.Count)];
    }
}