using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Mimic
{
    public class MimicObjectCreator
    {
        private readonly PackCreator _packCreator;

        public MimicObjectCreator(PackCreator packCreator)
        {
            _packCreator = packCreator;
        }

        public ComponentContainer CreateObject(Vector3 startVelocity, Vector3 position)
        {
            var spawnedObjectContainer = _packCreator.CreateMimicContainer();
            var prefab = spawnedObjectContainer.GetRandomPrefab();
            return spawnedObjectContainer.SpawnedObjectFactoryBase.Create(prefab, spawnedObjectContainer.Mass,
                position,
                startVelocity).Result;
        }
    }
}