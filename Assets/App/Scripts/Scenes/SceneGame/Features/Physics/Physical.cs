using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class Physical : MonoBehaviour
    {
        public event Action<Physical> OnRemove; 
        public Vector3 Velocity { get; private set; }
        
        public void AddVelocity(Vector3 velocity) =>
            Velocity += velocity;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void Construct(GamePhysics physics)
        {
            physics.AddObject(this);
        }

        private void OnDestroy() =>
            OnRemove?.Invoke(this);
    }
}