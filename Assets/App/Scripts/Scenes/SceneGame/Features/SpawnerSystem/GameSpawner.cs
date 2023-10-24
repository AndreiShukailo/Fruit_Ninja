using System.Collections;
using System.Collections.Generic;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem
{
    public class GameSpawner : IStatable
    {
        public GameState.Services.GameState GameState { get; set; }
        private const int PackToComplication = 1;
        private readonly SpawnerConfig _spawnerConfig;
        private readonly ZoneInitializer _zoneInitializer;
        private readonly PackCreator _packCreator;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _spawning;

        private int _currenPackCount;
        private float _currentPackDelay;
        private float _currentObjectDelay;

        private int _packCounter;
        private Queue<SpawnedObjectContainer> _currentPack;

        public GameSpawner(ICoroutineRunner coroutineRunner, SpawnerConfig spawnerConfig,
            ZoneInitializer zoneInitializer, PackCreator packCreator)
        {
            _coroutineRunner = coroutineRunner;
            _spawnerConfig = spawnerConfig;
            _zoneInitializer = zoneInitializer;
            _packCreator = packCreator;
        }

        private void StartSpawn()
        {
            _currentPackDelay = _spawnerConfig.MaxPackDelay;
            _currentObjectDelay = _spawnerConfig.MaxObjectDelay;
            _currenPackCount = _spawnerConfig.MinPackCount;
            _currentPack = _packCreator.CreatePack(_currenPackCount);
            _spawning = _coroutineRunner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (GameState.IsPlaying)
            {
                yield return new WaitForSeconds(_currentPackDelay);

                for (int i = 0; i < _currenPackCount; i++)
                {
                    yield return new WaitForSeconds(_currentObjectDelay);
                    var zone = _zoneInitializer.GetRandomZone();
                    var spawnedObjectContainer = _currentPack.Dequeue();
                    zone?.Spawn(spawnedObjectContainer, 0f);
                }

                Complication();
                _currentPack = _packCreator.CreatePack(_currenPackCount);
            }
        }

        public void StopSpawn() =>
            _coroutineRunner.StopCoroutine(_spawning);

        private void Complication()
        {
            _packCounter++;
            if (_packCounter < PackToComplication) return;
            _packCounter = 0;
            ComplicatePackCount();
            ComplicatePackDelay();
            ComplicateObjectDelay();
        }

        private void ComplicateObjectDelay()
        {
            if (_currentObjectDelay > _spawnerConfig.MinObjectDelay)
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

        public void Reset() =>
            StartSpawn();

        public void SetGameState(GameState.Services.GameState gameState)
        {
            GameState = gameState;
        }
    }
}