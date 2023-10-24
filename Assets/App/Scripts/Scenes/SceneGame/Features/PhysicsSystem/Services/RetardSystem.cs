using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.View;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Ice
{
    public class RetardSystem : ITickable, IStatable
    {
        private const float NewGravity = 1f;
        private readonly GamePhysics _gamePhysics;
        private readonly FrostEffectView _frostEffectView;

        private bool _isRetard;
        private float _retardTimer;
        private GameState.Services.GameState _gameState;

        public RetardSystem(GamePhysics gamePhysics, FrostEffectView frostEffectView)
        {
            _gamePhysics = gamePhysics;
            _frostEffectView = frostEffectView;
        }

        public void Tick()
        {
            if (_isRetard)
                _retardTimer -= Time.deltaTime;

            if (_retardTimer <= 0 && _isRetard || !_gameState.IsPlaying)
                CancelRetard();
        }

        public void ApplyRetard(float retardTime)
        {
            if (_isRetard)
            {
                _retardTimer = retardTime;
                return;
            }
            _gamePhysics.SetRetardVelocity(NewGravity);
            _isRetard = true;
            _retardTimer = retardTime;
            _frostEffectView.Activate();
        }

        private void CancelRetard()
        {
            _isRetard = false;
            _gamePhysics.CancelRetardVelocity();
            _frostEffectView.Deactivate();
        }

        public void SetGameState(GameState.Services.GameState gameState)
        {
            _gameState = gameState;
        }
    }
}