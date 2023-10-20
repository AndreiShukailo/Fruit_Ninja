using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public class PackCreator
    {
        private readonly List<SpawnedObjectContainer> _spawnedObjectContainers;

        public PackCreator(SpawnerConfig spawnerConfig)
        {
            _spawnedObjectContainers = spawnerConfig.SpawnedObjectContainers;
        }

        public Queue<SpawnedObjectContainer> CreatePack(int packCount)
        {
            var pack = new Queue<SpawnedObjectContainer>();
            for (int i = 0; i < packCount; i++)
            {
                var container = GetRandomContainer();
                while (!(pack.Count(p => p == container) < container.MaxCountInPack))
                {
                    container = GetRandomContainer();
                }

                pack.Enqueue(container);
            }

            return pack;
        }

        private SpawnedObjectContainer GetRandomContainer()
        {
            if (_spawnedObjectContainers.Count == 0) return null;
            var probs = new float[_spawnedObjectContainers.Count];
            for (int i = 0; i < _spawnedObjectContainers.Count; i++)
                if (_spawnedObjectContainers != null)
                    probs[i] = (float)_spawnedObjectContainers[i].PriorityInPack / _spawnedObjectContainers.Count;

            return _spawnedObjectContainers[GetRandomNumber(probs)];
        }

        private int GetRandomNumber(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
                total += elem;

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return i;

                randomPoint -= probs[i];
            }

            return probs.Length - 1;
        }
    }
}