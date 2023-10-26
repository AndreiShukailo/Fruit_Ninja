using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Factory;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Services
{
    public class MagnetSystem : ITickable, IStatable
    {
        private readonly AllSpawnedObjects _allSpawnedObjects;
        private readonly MagnetConfig _magnetConfig;
        private readonly MagnetEffectFactory _magnetEffectFactory;
        private readonly List<MagnetAction> _magnets = new();
        private readonly List<MagnetAction> _toRemove = new();
        private GameState.Services.GameState _gameState;

        private bool _isMagnet;
        private float _magnetRadius;
        private float _force;

        public MagnetSystem(AllSpawnedObjects allSpawnedObjects, MagnetConfig magnetConfig,
            MagnetEffectFactory magnetEffectFactory)
        {
            _allSpawnedObjects = allSpawnedObjects;
            _magnetConfig = magnetConfig;
            _magnetEffectFactory = magnetEffectFactory;
        }

        public void Tick()
        {
            if (!_gameState.IsPlaying) return;

            foreach (var magnet in _magnets)
            {
                foreach (var componentContainer in _allSpawnedObjects.Objects)
                {
                    var magnetComponent = componentContainer.GetComponent<MagneticComponent>();

                    if (magnetComponent is not { IsMagnetic: false }) continue;
                    if (!componentContainer.GetComponent<CheckOnInvisibleComponent>().IsVisible) continue;
                    var itemPosition = componentContainer.UnitView.transform.position;
                    var distance = Vector2.Distance(itemPosition, magnet.MagnetPosition);

                    if (!(distance <= _magnetConfig.MagnetRadius)) continue;
                    magnetComponent.SetMagnetic(magnet.MagnetPosition, magnet.MagnetTimer);
                }

                magnet.MagnetTimer -= Time.deltaTime;
                if (magnet.MagnetTimer <= 0)
                    _toRemove.Add(magnet);
            }

            foreach (var componentContainer in _allSpawnedObjects.Objects)
            {
                var magnetComponent = componentContainer.GetComponent<MagneticComponent>();

                if (magnetComponent is not { IsMagnetic: true }) continue;

                if (magnetComponent.MagneticTimer <= 0)
                {
                    magnetComponent.SetUnMagnetic();
                    continue;
                }

                magnetComponent.MagneticTimer -= Time.deltaTime;

                var physicalComponent = componentContainer.GetComponent<PhysicalComponent>();
                var itemPosition = componentContainer.UnitView.transform.position;
                var heading = magnetComponent.EndPoint - itemPosition;
                physicalComponent.SetVelocity(heading * _magnetConfig.MagnetForce);
            }

            foreach (var magnet in _toRemove)
                _magnets.Remove(magnet);

            _toRemove.Clear();
        }

        public void ApplyMagnet(Vector3 magnetPosition)
        {
            var magnet = new MagnetAction(_magnetConfig.MagnetTime, magnetPosition);
            _magnets.Add(magnet);
            _magnetEffectFactory.Create(_magnetConfig.MagneticEffectView, magnetPosition, _magnetConfig.MagnetTime,
                _magnetConfig.MagnetRadius);
        }

        public void SetGameState(GameState.Services.GameState gameState)
        {
            _gameState = gameState;
        }
    }

    public class MagnetAction
    {
        public float MagnetTimer;
        public Vector3 MagnetPosition;

        public MagnetAction(float magnetTimer, Vector3 magnetPosition)
        {
            MagnetTimer = magnetTimer;
            MagnetPosition = magnetPosition;
        }
    }
}