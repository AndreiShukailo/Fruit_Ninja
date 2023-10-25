using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem
{
    public class AllSpawnedObjects
    {
        public readonly List<ComponentContainer> Objects = new();

        public void Add(ComponentContainer componentContainer) =>
            Objects.Add(componentContainer);

        public void Remove(ComponentContainer componentContainer) =>
            Objects.Remove(componentContainer);
    }
}