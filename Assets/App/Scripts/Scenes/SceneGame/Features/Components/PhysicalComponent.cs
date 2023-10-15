using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class PhysicalComponent : Component, IPhysical
    {
        public Transform Transform => ComponentContainer.UnitView.transform;
        public Vector3 Velocity { get; set; }
        private readonly GamePhysics _gamePhysics;
        private readonly UnitView _unitView;

        public PhysicalComponent(GamePhysics gamePhysics)
        {
            _gamePhysics = gamePhysics;
            _gamePhysics.AddObject(this);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _gamePhysics.RemoveObject(this);
        }
    }
}