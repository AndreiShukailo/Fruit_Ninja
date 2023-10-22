using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services
{
    public class GamePhysics : ITickable
    {
        private readonly PhysicsConfig _config;

        private readonly List<IPhysical> _objects = new();

        public GamePhysics(PhysicsConfig config) =>
            _config = config;

        public void AddObject(IPhysical physical) =>
            _objects.Add(physical);

        public void RemoveObject(IPhysical physical) =>
            _objects.Remove(physical);

        public void Tick()
        {
            foreach (var physical in _objects)
                UpdateObject(physical);
        }

        private void UpdateObject(IPhysical physical)
        {
            physical.AddVelocity(GetGravityVelocity(physical));
            physical.Transform.position += physical.Velocity * Time.deltaTime;
        }

        private Vector3 GetGravityVelocity(IPhysical physical) =>
            Vector3.down * (_config.Gravity * physical.Mass * Time.deltaTime);
    }
}