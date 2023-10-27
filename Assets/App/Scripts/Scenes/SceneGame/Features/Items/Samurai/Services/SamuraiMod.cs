using System.Collections.Generic;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Factory;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services
{
    public class SamuraiMod : ITickable, IStatable
    {
        private readonly SamuraiModeConfig _samuraiModeConfig;
        private readonly SamuraiFruitFactory _samuraiFruitFactory;
        private GameState.Services.GameState _gameState;
        private List<float> _savedPriority = new();
        private int _leftTime;
        private float _timer;
        private bool _isStart;

        public SamuraiMod(SamuraiModeConfig samuraiModeConfig, SamuraiFruitFactory samuraiFruitFactory)
        {
            _samuraiModeConfig = samuraiModeConfig;
            _samuraiFruitFactory = samuraiFruitFactory;
        }

        public void Tick()
        {
            if(!_isStart) return;
            if (!_gameState.IsPlaying)
                StopMode();
            if (!_gameState.IsSamuraiMode) return;
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _leftTime -= 1;
                _samuraiModeConfig.SamuraiLabelView.UpdateTimer(_leftTime);
                _timer = 0;
            }
            if(_leftTime <= 0)
                StopMode();
            
        }

        public void StartMode()
        {
            _isStart = true;
            _leftTime = _samuraiModeConfig.TimeOfAction;
            _samuraiModeConfig.SamuraiLabelView.ShowLabel(_leftTime);
            _gameState.StartSamuraiMode(_samuraiModeConfig.PackDelayFactor, _samuraiModeConfig.PackCountFactor,
                _samuraiFruitFactory);

            foreach (var spawnZoneConfig in _samuraiModeConfig.DisabledSpawnZones)
            {
                _savedPriority.Add(spawnZoneConfig.Priority);
                spawnZoneConfig.Priority = 0;
            }
        }

        private void StopMode()
        {
            _isStart = false;
            _gameState.StopSamuraiMod();
            _samuraiModeConfig.SamuraiLabelView.HideLabel();
            for (int i = 0; i < _samuraiModeConfig.DisabledSpawnZones.Count; i++)
                _samuraiModeConfig.DisabledSpawnZones[i].Priority = _savedPriority[i];
        }


        public void SetGameState(GameState.Services.GameState gameState)
        {
            _gameState = gameState;
        }
    }
}