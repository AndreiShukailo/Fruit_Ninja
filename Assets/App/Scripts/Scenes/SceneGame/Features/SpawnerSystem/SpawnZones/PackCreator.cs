using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.SceneGame.Features.Fruits;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick.Factory;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart;
using App.Scripts.Scenes.SceneGame.Features.Items.Mimic;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public class PackCreator
    {
        private readonly PlayerDataModel _playerDataModel;
        private readonly AllSpawnedObjects _allSpawnedObjects;
        private readonly SamuraiMod _samuraiMod;
        private readonly List<SpawnedObjectContainer> _spawnedObjectContainers;

        private SpawnedObjectContainer _currentContainer;

        public PackCreator(SpawnerConfig spawnerConfig, PlayerDataModel playerDataModel,
            AllSpawnedObjects allSpawnedObjects, SamuraiMod samuraiMod)
        {
            _playerDataModel = playerDataModel;
            _allSpawnedObjects = allSpawnedObjects;
            _samuraiMod = samuraiMod;
            _spawnedObjectContainers = spawnerConfig.SpawnedObjectContainers;
        }

        public Queue<SpawnedObjectContainer> CreatePack(int packCount)
        {
            var pack = new Queue<SpawnedObjectContainer>();
            for (int i = 0; i < packCount; i++)
            {
                var container = GetRandomContainer();
                while (ImpossibleToSpawn(pack, container))
                {
                    container = GetRandomContainer();
                }

                pack.Enqueue(container);
            }

            return pack;
        }

        public Queue<SpawnedObjectContainer> CreateFruitOnlyPack(int packCount)
        {
            var pack = new Queue<SpawnedObjectContainer>();
            var container = _spawnedObjectContainers.Find((p) => p is FruitsContainer);
            for (int i = 0; i < packCount; i++)
                pack.Enqueue(container);

            return pack;
        }

        public SpawnedObjectContainer CreateMimicContainer()
        {
            var container = GetRandomContainer();
            while (ImpossibleToSpawnMimicContainer(container))
                container = GetRandomContainer();

            _currentContainer = container;
            return container;
        }

        private bool ImpossibleToSpawnMimicContainer(SpawnedObjectContainer container)
        {
            if (_currentContainer == container)
                return true;

            switch (container)
            {
                case HealthItemContainer when !_playerDataModel.AllowSpawnHealth:
                case BrickContainer when BrickOnScene():
                case SamuraiContainer when SamuraiOnScene():
                case MimicContainer:
                    return true;
                default:
                    return false;
            }
        }

        private bool ImpossibleToSpawn(Queue<SpawnedObjectContainer> pack, SpawnedObjectContainer container)
        {
            var isImpossible = !(pack.Count(p => p == container) < container.MaxCountInPack);
            switch (container)
            {
                case HealthItemContainer when !_playerDataModel.AllowSpawnHealth:
                case BrickContainer when BrickOnScene():
                case SamuraiContainer when SamuraiOnScene():
                    return true;
                default:
                    return isImpossible;
            }
        }

        private bool BrickOnScene()
        {
            foreach (var componentContainer in _allSpawnedObjects.Objects)
            {
                var brick = componentContainer.GetComponent<BrickComponent>();
                if (brick != null)
                    return true;
            }

            return false;
        }

        private bool SamuraiOnScene()
        {
            if (_samuraiMod.IsStart)
                return true;

            foreach (var componentContainer in _allSpawnedObjects.Objects)
            {
                var samuraiComponent = componentContainer.GetComponent<SamuraiComponent>();
                if (samuraiComponent != null)
                    return true;
            }

            return false;
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