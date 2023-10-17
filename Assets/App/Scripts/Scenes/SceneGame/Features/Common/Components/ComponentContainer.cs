using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Common.Components
{
    public class ComponentContainer
    {
        public UnitView UnitView { get; private set; }
        private readonly List<Component> _components;
        
        public ComponentContainer(UnitView unitView)
        {
            UnitView = unitView;
            _components = new List<Component>();
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            var findComponent =  _components.Find(p => p is TComponent);
            return (TComponent)findComponent;
        }

        public void AddComponent<TComponent>(TComponent component) where TComponent : Component
        {
            component.ComponentContainer = this;
            _components.Add(component);
        }

        public void Destroy()
        {
            foreach (var component in _components)
            {
                component.OnDestroy();
            }

            Object.Destroy(UnitView.gameObject);
        }
    }
}