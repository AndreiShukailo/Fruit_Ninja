using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.Config;
using App.Scripts.Scenes.SceneGame.Features.ComboSystem.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.ComboSystem.Services
{
    public class ComboSystem : ITickable
    {
        public int ComboCount { get; private set; }
        private readonly ComboConfig _comboConfig;
        private readonly ComboPopupView _comboPopupView;
        private float _lastSwipeTime;

        public ComboSystem(ComboConfig comboConfig, ComboPopupView comboPopupView)
        {
            _comboConfig = comboConfig;
            _comboPopupView = comboPopupView;
        }

        public void GetCombo()
        {
            if (_lastSwipeTime <= _comboConfig.MaxTimeToCombo)
            {
                ComboCount += 1;
                _lastSwipeTime = 0;
            }
            else
            {
                ComboCount = 1;
                _lastSwipeTime = 0;
            }

            if (ComboCount > 1)
                _comboPopupView.Show(ComboCount);
        }

        public void Tick()
        {
            if (ComboCount == 0)
                _lastSwipeTime = 0;
            else
                _lastSwipeTime += Time.deltaTime;

            if (ComboCount != _comboConfig.MaxComboCount) return;
            _lastSwipeTime = 0;
            ComboCount = 0;
        }
    }
}