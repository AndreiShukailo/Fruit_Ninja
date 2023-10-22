using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Heart
{
    public class HealthItemSwipeStrategy : ISwipeStrategy
    {
        private readonly PlayerDataModel _playerDataModel;

        public HealthItemSwipeStrategy(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            if (_playerDataModel.AllowSpawnHealth)
                _playerDataModel.AddHealth(componentContainer.UnitView.transform.position);
            componentContainer.Destroy();
        }
    }
}