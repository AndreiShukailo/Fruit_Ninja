using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Heart
{
    public class HealthItemSwipeStrategy : ISwipeStrategy
    {
        private readonly PlayerDataModel _playerDataModel;
        private readonly HealthItemConfig _healthItemConfig;

        public HealthItemSwipeStrategy(PlayerDataModel playerDataModel, HealthItemConfig healthItemConfig)
        {
            _playerDataModel = playerDataModel;
            _healthItemConfig = healthItemConfig;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            var position = componentContainer.UnitView.transform.position;
            if (_playerDataModel.AllowSpawnHealth)
                _playerDataModel.AddHealth(componentContainer.UnitView.transform.position);
            componentContainer.Destroy();
            var vfx = Object.Instantiate(_healthItemConfig.HealthVFX);
            vfx.transform.position = position;
        }
    }
}