using App.Scripts.Scenes.SceneGame.Features.Common;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services
{
    public interface IPhysical : ITransformable
    {
        public Vector3 Velocity { get; set; }
        public float Mass { get; set; }
        bool IsRetarding { get; set; }

        public void AddVelocity(Vector3 velocity) =>
            Velocity += velocity;
    }
}