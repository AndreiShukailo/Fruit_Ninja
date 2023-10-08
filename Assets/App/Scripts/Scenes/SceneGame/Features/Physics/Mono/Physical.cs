using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics.Mono
{
    public class Physical : MonoBehaviour
    {
        public Vector3 Velocity { get; private set; }

        private GamePhysics _physics;

        public void AddVelocity(Vector3 velocity) =>
            Velocity += velocity;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Construct(GamePhysics physics)
        {
            _physics = physics;
            _physics.AddObject(this);
        }

        private void OnDestroy() =>
            _physics.RemoveObject(this);
    }
}