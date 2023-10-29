using System;
using System.Threading.Tasks;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Mimic.Factory
{
    public class MimicFactory : ISpawnedFactory
    {
        private readonly AllSpawnedObjects _allSpawnedObjects;
        private readonly MimicSystem _mimicSystem;

        public MimicFactory(AllSpawnedObjects allSpawnedObjects, MimicSystem mimicSystem)
        {
            _allSpawnedObjects = allSpawnedObjects;
            _mimicSystem = mimicSystem;
        }

        public async Task<ComponentContainer> Create(UnitView prefab, float mass, Vector3 position,
            Vector3 startVelocity, float swipeAfter = 0)
        {
            var view = Object.Instantiate(prefab);
            view.transform.position = position;
            var componentContainer = new ComponentContainer(view, _allSpawnedObjects);
            var mimicComponent = new MimicComponent(_mimicSystem, startVelocity);
            componentContainer.AddComponent(mimicComponent);
            mimicComponent.Register();
            await Task.Delay(TimeSpan.FromSeconds(swipeAfter));
            return componentContainer;
        }
    }
}