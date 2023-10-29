using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Mimic
{
    public class MimicSystem : ITickable
    {
        private readonly List<MimicComponent> _mimics = new();
        private readonly List<MimicComponent> _toRemove = new();
        private readonly MimicObjectCreator _mimicObjectCreator;
        private readonly MimicConfig _mimicConfig;

        public MimicSystem(MimicObjectCreator mimicObjectCreator, MimicConfig mimicConfig)
        {
            _mimicObjectCreator = mimicObjectCreator;
            _mimicConfig = mimicConfig;
        }

        public void Add(MimicComponent mimicComponent)
        {
            mimicComponent.TimeToSwitch = _mimicConfig.TimeToSwitch;
            var componentContainer =
                _mimicObjectCreator.CreateObject(mimicComponent.StartVelocity, mimicComponent.Position);
            mimicComponent.ObjectComponentContainer = componentContainer;
            _mimics.Add(mimicComponent);
        }

        private void Remove(MimicComponent mimicComponent)
        {
            _toRemove.Add(mimicComponent);
            mimicComponent.ComponentContainer.Destroy();
        }

        public void Tick()
        {
            foreach (var mimic in _mimics)
            {
                var componentContainer = mimic.ObjectComponentContainer;
                if (componentContainer.UnitView is null)
                {
                    Remove(mimic);
                    continue;
                }

                mimic.Position = componentContainer.UnitView.transform.position;
                mimic.TimeToSwitch -= Time.deltaTime;
                if (mimic.TimeToSwitch > 0) continue;
                mimic.TimeToSwitch = _mimicConfig.TimeToSwitch;
                var physicalComponent = componentContainer.GetComponent<PhysicalComponent>();
                var velocity = physicalComponent.Velocity;
                var isRetarding = physicalComponent.IsRetarding;
                mimic.ObjectComponentContainer = _mimicObjectCreator.CreateObject(velocity, mimic.Position);
                var objectPhysics = mimic.ObjectComponentContainer.GetComponent<PhysicalComponent>();
                objectPhysics.IsRetarding = isRetarding;
                var checkOnInvisible = mimic.ObjectComponentContainer.GetComponent<CheckOnInvisibleComponent>();
                checkOnInvisible.IsVisible = true;
                componentContainer.Destroy();
            }

            foreach (var mimic in _toRemove)
                _mimics.Remove(mimic);

            _toRemove.Clear();
        }
    }
}