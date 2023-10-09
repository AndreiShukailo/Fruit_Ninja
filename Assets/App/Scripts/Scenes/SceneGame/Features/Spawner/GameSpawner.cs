using System.Collections;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.Spawner.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner
{
    public class GameSpawner
    {
        private readonly SpawnerConfig _spawnerConfig;
        private readonly ZoneInitializer _zoneInitializer;
        private readonly ICoroutineRunner _coroutineRunner;

        private int _nextPackFruitCount;


        public GameSpawner(ICoroutineRunner coroutineRunner, SpawnerConfig spawnerConfig,
            ZoneInitializer zoneInitializer)
        {
            _coroutineRunner = coroutineRunner;
            _spawnerConfig = spawnerConfig;
            _zoneInitializer = zoneInitializer;
        }

        public void StartSpawn()
        {
            _nextPackFruitCount = GetNextPackFruitCount();
            _coroutineRunner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnerConfig.PackDelay);

                for (int i = 0; i < _nextPackFruitCount; i++)
                {
                    yield return new WaitForSeconds(_spawnerConfig.ObjectDelay);
                    var zone = _zoneInitializer.GetRandomZone();
                    zone?.Spawn();
                }

                _nextPackFruitCount = GetNextPackFruitCount();
            }
        }

        private int GetNextPackFruitCount() =>
            Random.Range(_spawnerConfig.MinPackCount, _spawnerConfig.MaxPackCount + 1);
    }
}