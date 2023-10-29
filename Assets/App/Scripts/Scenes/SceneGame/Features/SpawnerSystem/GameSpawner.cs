using System.Collections;
using System.Collections.Generic;
using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.Fruits;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Factory;
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
        private readonly FruitFactory _fruitFactory;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _spawning;

        private int _currenPackCount;
        private float _currentPackDelay;
        private float _currentObjectDelay;

        private int _packCounter;
        private Queue<SpawnedObjectContainer> _currentPack;

        public GameSpawner(ICoroutineRunner coroutineRunner, SpawnerConfig spawnerConfig,
            ZoneInitializer zoneInitializer, PackCreator packCreator, FruitFactory fruitFactory)
        {
            _coroutineRunner = coroutineRunner;
            _spawnerConfig = spawnerConfig;
            _zoneInitializer = zoneInitializer;
            _packCreator = packCreator;
            _fruitFactory = fruitFactory;
        }

        private void StartSpawn()
        {
            _currentPackDelay = _spawnerConfig.MaxPackDelay;
            _currentObjectDelay = _spawnerConfig.MaxObjectDelay;
            _currenPackCount = _spawnerConfig.MinPackCount;
            StartSpawning();
        }

        public void StartSpawning()
        {
            _currentPack = CreatePack();
            _spawning = _coroutineRunner.StartCoroutine(Spawn());
        }

        public void Reset() =>
            StartSpawn();

        public void SetGameState(GameState.Services.GameState gameState)
        {
            GameState = gameState;
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
                _currentPack = CreatePack();
            }
        }

        public void StopSpawn()
        {
            if(_spawning == null) return;
            _coroutineRunner.StopCoroutine(_spawning);
        }


        public void StartSamuraiMode(int packDelayFactor, int packCountFactor, SamuraiFruitFactory samuraiFruitFactory)
        {
            _currentPackDelay /= packDelayFactor;
            _currenPackCount *= packCountFactor;
            _spawnerConfig.GetAndBind<FruitsContainer, SamuraiFruitFactory>(samuraiFruitFactory);
        }

        public void StopSamuraiMode(int packDelayFactor, int packCountFactor)
        {
            _currentPackDelay *= packDelayFactor;
            _currenPackCount /= packCountFactor;
            _spawnerConfig.GetAndBind<FruitsContainer, FruitFactory>(_fruitFactory);
        }

        private void Complication()
        {
            _packCounter++;
            if (_packCounter < PackToComplication) return;
            _packCounter = 0;
            ComplicatePackCount();
            ComplicatePackDelay();
            ComplicateObjectDelay();
        }

        private Queue<SpawnedObjectContainer> CreatePack()
        {
            return GameState.IsSamuraiMode
                ? _packCreator.CreateFruitOnlyPack(_currenPackCount)
                : _packCreator.CreatePack(_currenPackCount);
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
    }
}