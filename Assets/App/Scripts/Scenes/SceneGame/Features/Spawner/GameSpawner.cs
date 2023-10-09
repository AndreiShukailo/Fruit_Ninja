using System.Collections;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.Spawner.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner
{
    public class GameSpawner
    {
        private const int PackToComplication = 3;
        private readonly SpawnerConfig _spawnerConfig;
        private readonly ZoneInitializer _zoneInitializer;
        private readonly ICoroutineRunner _coroutineRunner;

        private int _currenPackCount;
        private float _currentPackDelay;
        private float _currentObjectDelay;

        private int _packCounter;

        public GameSpawner(ICoroutineRunner coroutineRunner, SpawnerConfig spawnerConfig,
            ZoneInitializer zoneInitializer)
        {
            _coroutineRunner = coroutineRunner;
            _spawnerConfig = spawnerConfig;
            _zoneInitializer = zoneInitializer;
        }

        public void StartSpawn()
        {
            _currentPackDelay = _spawnerConfig.MaxPackDelay;
            _currentObjectDelay = _spawnerConfig.MaxObjectDelay;
            _currenPackCount = _spawnerConfig.MinPackCount;
            _coroutineRunner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentPackDelay);

                for (int i = 0; i < _currenPackCount; i++)
                {
                    yield return new WaitForSeconds(_currentObjectDelay);
                    var zone = _zoneInitializer.GetRandomZone();
                    zone?.Spawn();
                }

                Complication();
            }
        }

        private void Complication()
        {
            _packCounter++;
            if (_packCounter <= PackToComplication) return;
            _packCounter = 0;
            ComplicatePackCount();
            ComplicatePackDelay();
            ComplicateObjectDelay();
        }

        private void ComplicateObjectDelay()
        {
            if (_currentObjectDelay > _spawnerConfig.MinPackDelay)
                _currentObjectDelay -= _spawnerConfig.ObjectComplicationFactor;
        }

        private void ComplicatePackDelay()
        {
            if (_currentPackDelay > _spawnerConfig.MinPackDelay)
                _currentPackDelay -= _spawnerConfig.PackComplicationFactor;
        }

        private void ComplicatePackCount()
        {
            if (_currenPackCount < _spawnerConfig.MaxPackCount)
                _currenPackCount++;
        }
    }
}