using System.Collections.Generic;
using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.Installer;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class GamePhysics : ITickable
    {
        private readonly float _gravity;
        
        private readonly List<Physical> _objects = new();

        public GamePhysics(GameConfig config) =>
            _gravity = config.Gravity;

        public void AddObject(Physical physical)
        {
            _objects.Add(physical);
            physical.OnRemove += RemoveObject;
        }

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
            Vector3.down * (_gravity * Time.deltaTime);

        private void RemoveObject(Physical physical)
        {
            physical.OnRemove -= RemoveObject;
            _objects.Remove(physical);
        }
    }
}