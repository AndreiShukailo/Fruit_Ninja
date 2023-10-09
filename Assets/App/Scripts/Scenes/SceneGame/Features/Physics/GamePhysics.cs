using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.Physics.Config;
using App.Scripts.Scenes.SceneGame.Features.Physics.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class GamePhysics : ITickable
    {
        private PhysicsConfig _config;
        
        private readonly List<Physical> _objects = new();

        public GamePhysics(PhysicsConfig config) =>
            _config = config;

        public void AddObject(Physical physical) =>
            _objects.Add(physical);

        public void RemoveObject(Physical physical) =>
            _objects.Remove(physical);

        public void Tick()
        {
            foreach (var physical in _objects)
                UpdateObject(physical);
        }

        private void UpdateObject(Physical physical)
        {
            physical.AddVelocity(GetGravityVelocity());
            physical.Position += physical.Velocity * Time.deltaTime;
        }

        private Vector3 GetGravityVelocity() =>
            Vector3.down * (_config.Gravity * Time.deltaTime);
    }
}