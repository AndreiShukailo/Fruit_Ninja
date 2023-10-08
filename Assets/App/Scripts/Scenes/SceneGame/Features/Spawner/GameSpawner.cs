using System.Collections;
using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner
{
    public class GameSpawner
    {
        private SpawnZone[] _zones;
        private readonly GameConfig _gameConfig;
        private readonly ICoroutineRunner _coroutineRunner;
        private int _nextPackFruitCount;


        public GameSpawner(GameConfig gameConfig, ICoroutineRunner coroutineRunner, SpawnZoneConfigs zoneConfigs,
            SpawnedObjectFactory spawnedObjectFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap)
        {
            InitZones(zoneConfigs, spawnedObjectFactory, physicalStartVelocityCalculator, cameraWrap);
            _gameConfig = gameConfig;
            _coroutineRunner = coroutineRunner;
            _nextPackFruitCount = GetNextPackFruitCount();
        }

        public void StartSpawn()
        {
            _coroutineRunner.StartCoroutine(Spawn());
        }

        private void InitZones(SpawnZoneConfigs config, SpawnedObjectFactory spawnedObjectFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap)
        {
            _zones = new SpawnZone[config.Zones.Count];
            for (var i = 0; i < _zones.Length; i++)
            {
                _zones[i] = new SpawnZone(spawnedObjectFactory, physicalStartVelocityCalculator, cameraWrap,
                    config.Zones[i]);
            }
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_gameConfig.PackDelay);

                for (int i = 0; i < _nextPackFruitCount; i++)
                {
                    yield return new WaitForSeconds(_gameConfig.FruitDelay);
                    var zone = GetRandomZone();
                    zone.Spawn();
                }

                _nextPackFruitCount = GetNextPackFruitCount();
            }
        }

        private int GetNextPackFruitCount() =>
            Random.Range(2, _gameConfig.MaxPackCount + 1);

        private SpawnZone GetRandomZone()
        {
            var probs = new float[_zones.Length];
            for (int i = 0; i < _zones.Length; i++)
                probs[i] = _zones[i].Priority / _zones.Length;

            return _zones[GetRandomZoneNumber(probs)];
        }

        private int GetRandomZoneNumber(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

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