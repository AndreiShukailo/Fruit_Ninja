using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Items.Mimic.View;
using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Mimic
{
    public class MimicComponent : Component
    {
        private readonly MimicObjectCreator _mimicObjectCreator;
        private readonly ICoroutineRunner _coroutineRunner;

        private ComponentContainer _objectComponentContainer;
        private Vector3 _velocity;
        private Vector3 _position;
        private readonly MimicSystem _mimicSystem;

        public Vector3 StartVelocity { get; }

        public float TimeToSwitch { get; set; }
        public ComponentContainer ObjectComponentContainer { get; set; }

        public Vector3 Position
        {
            get => ComponentContainer.UnitView.transform.position;
            set => ComponentContainer.UnitView.transform.position = value;
        }

        public MimicComponent(MimicSystem mimicSystem, Vector3 startVelocity)
        {
            _mimicSystem = mimicSystem;
            StartVelocity = startVelocity;
        }

        public void Register() =>
            _mimicSystem.Add(this);

        public void PlayChangeMimicEffect() =>
            (ComponentContainer.UnitView as MimicView)?.PlayChangeMimicEffect();
        
        public void PlayMimicEffect() =>
            (ComponentContainer.UnitView as MimicView)?.PlayMimicEffect();
    }
}