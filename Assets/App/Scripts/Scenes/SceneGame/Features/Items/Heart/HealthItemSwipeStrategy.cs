using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.View;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Heart
{
    public class HealthItemSwipeStrategy : ISwipeStrategy
    {
        private readonly PlayerDataModel _playerDataModel;
        private readonly HealthView _healthView;

        public HealthItemSwipeStrategy(PlayerDataModel playerDataModel, HealthView healthView)
        {
            _playerDataModel = playerDataModel;
            _healthView = healthView;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            if (_playerDataModel.AllowSpawnHealth)
                _playerDataModel.AddHealth();

            var physical = componentContainer.GetComponent<PhysicalComponent>();
            var swipable = componentContainer.GetComponent<SwipableComponent>();
            swipable.RemoveComponent();
            physical.DisablePhysics();
            componentContainer.UnitView.Animator.StopAnimations();
            var healthItemAnimation = componentContainer.UnitView.Animator.GetAnimation<HealthItemAnimation>();
            healthItemAnimation.StartAnimation(_healthView.GetContainerPosition(),
                () => OnAnimationEnd(componentContainer));
        }

        private void OnAnimationEnd(ComponentContainer componentContainer)
        {
            _playerDataModel.UpdateHealth();
            componentContainer.Destroy();
        }
    }
}