using System.Collections.Generic;
using System.Linq;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services
{
    public class GamePhysics : ITickable
    {
        private readonly PhysicsConfig _config;
        private readonly List<IPhysical> _objects = new();

        private float _gravity;
        private bool _isRetard;
        private float _velocityFactor;

        public GamePhysics(PhysicsConfig config)
        {
            _config = config;
            _gravity = _config.Gravity;
        }


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
            if (_isRetard && !physical.IsRetarding)
            {
                physical.Velocity /= _velocityFactor;
                physical.IsRetarding = true;
            }
            
            physical.AddVelocity(GetGravityVelocity(physical));
            physical.Transform.position += physical.Velocity * Time.deltaTime;
            physical.OnUpdatePosition?.Invoke();
        }

        private Vector3 GetGravityVelocity(IPhysical physical) =>
            Vector3.down * (_gravity * physical.Mass * Time.deltaTime);

        public void SetRetardVelocity(float newGravity)
        {
            _gravity = newGravity;
            _velocityFactor = Mathf.Sqrt(_config.Gravity / _gravity);
            _isRetard = true;
        }
        
        public void CancelRetardVelocity()
        {
            foreach (var physical in _objects.Where(physical => physical.IsRetarding))
            {
                physical.Velocity *= _velocityFactor;
                physical.IsRetarding = false;
            }

            _isRetard = false;
            _gravity = _config.Gravity;
        }
    }
}