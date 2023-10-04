using System.Collections;
using App.Scripts.Infrastructure.Configs;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.SpawnZoneConfig;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner
{
    public class GameSpawner
    {
        private SpawnZone[] _zones;
        private readonly SpawnerCoroutineRunner _coroutineRunner;
        private GameConfig _config;
        private int _nextPackFruitCount;
        
        
        public GameSpawner(SpawnerCoroutineRunner coroutineRunner, GameConfig config, PhysicalFactory physicalFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap.CameraWrap cameraWrap)
        {
            InitZones(config, physicalFactory, physicalStartVelocityCalculator, cameraWrap);
            _coroutineRunner = coroutineRunner;
            _config = config;
            _nextPackFruitCount = GetNextPackFruitCount();
        }

        public void StartSpawn()
        {
            _coroutineRunner.StartCoroutine(Spawn());
        }


        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_config.PackDelay);
            
                for (int i = 0; i < _nextPackFruitCount; i++)
                {
                    yield return new WaitForSeconds(_config.FruitDelay);
                    var zone = GetRandomZone();
                    zone.Spawn();
                }
            
                _nextPackFruitCount = GetNextPackFruitCount();
            }
        }

        private int GetNextPackFruitCount() =>
            Random.Range(2, _config.MaxPackCount + 1);

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

        private void InitZones(GameConfig config, PhysicalFactory physicalFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap.CameraWrap cameraWrap)
        {
            _zones = new SpawnZone[config.Zones.Count];
            for (var i = 0; i < _zones.Length; i++)
            {
                var zoneData = config.Zones[i];
                var type = config.Zones[i].Type;
                _zones[i] = GetSpawnZone(type, physicalFactory, physicalStartVelocityCalculator, cameraWrap, zoneData);
            }
        }

        private SpawnZone GetSpawnZone(SpawnZoneType type, PhysicalFactory physicalFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator,
            CameraWrap.CameraWrap cameraWrap, SpawnZoneConfig zoneConfig)
        {
            return type switch
            {
                SpawnZoneType.Left => new LeftSpawnZone(physicalFactory, physicalStartVelocityCalculator, cameraWrap,
                    zoneConfig),
                SpawnZoneType.Right => new RightSpawnZone(physicalFactory, physicalStartVelocityCalculator, cameraWrap,
                    zoneConfig),
                SpawnZoneType.Bottom => new BottomSpawnZone(physicalFactory, physicalStartVelocityCalculator,
                    cameraWrap,
                    zoneConfig),
                _ => new LeftSpawnZone(physicalFactory, physicalStartVelocityCalculator, cameraWrap, zoneConfig)
            };
        }
    }
}