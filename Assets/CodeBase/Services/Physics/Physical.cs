using System;
using UnityEngine;

namespace CodeBase.Services.Physics
{
    public class Physical : MonoBehaviour
    {
        public event Action<Physical> OnRemove; 
        public Vector3 Velocity { get; private set; }

        public void AddVelocity(Vector3 velocity) =>
            Velocity += velocity;

        private void OnDestroy() =>
            OnRemove?.Invoke(this);
    }
}