using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.SceneGame.Features.Common.Components
{
    public class ComponentContainer
    {
        private readonly AllSpawnedObjects _allSpawnedObjects;
        public UnitView UnitView { get; private set; }
        private readonly List<Component> _components;

        public ComponentContainer(UnitView unitView, AllSpawnedObjects allSpawnedObjects)
        {
            _allSpawnedObjects = allSpawnedObjects;
            UnitView = unitView;
            _components = new List<Component>();

            allSpawnedObjects.Add(this);
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            var findComponent = _components.Find(p => p is TComponent);
            return (TComponent)findComponent;
        }

        public void AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            component.ComponentContainer = this;
            _components.Add(component);
        }

        public void Destroy()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].OnDestroy();
                _components[i] = null;
            }
            
            _allSpawnedObjects.Remove(this);
            Object.Destroy(UnitView.gameObject);
            UnitView = null;
        }
    }
}