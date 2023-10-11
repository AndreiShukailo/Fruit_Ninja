using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public interface IPhysical : ITransformable
    {
        public Vector3 Velocity { get; set; }

        public void AddVelocity(Vector3 velocity) =>
            Velocity += velocity;
    }
}