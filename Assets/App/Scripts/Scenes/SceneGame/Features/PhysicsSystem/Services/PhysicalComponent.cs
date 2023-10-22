using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;
using Component = App.Scripts.Scenes.SceneGame.Features.Common.Components.Component;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services
{
    public class PhysicalComponent : Component, IPhysical
    {
        public Transform Transform => ComponentContainer.UnitView.transform;
        public Vector3 Velocity { get; set; }
        public float Mass { get; set; }
        private readonly GamePhysics _gamePhysics;
        private readonly UnitView _unitView;

        public PhysicalComponent(GamePhysics gamePhysics, float mass)
        {
            Mass = mass;
            _gamePhysics = gamePhysics;
            _gamePhysics.AddObject(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DisablePhysics();
        }

        public void DisablePhysics() =>
            _gamePhysics.RemoveObject(this);
    }
}