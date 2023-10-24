using App.Scripts.Scenes.SceneGame.Features.Bomb.Animation;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb
{
    public class BombSwipeStrategy : ISwipeStrategy
    {
        private readonly BombConfig _bombConfig;
        private readonly CheckOnInVisible _checkOnInvisibleSystem;
        private readonly BombEffectsFactory _bombEffectsFactory;
        private readonly PlayerDataModel _playerDataModel;

        public BombSwipeStrategy(BombConfig bombConfig, CheckOnInVisible checkOnInvisibleSystem,
            BombEffectsFactory bombEffectsFactory, PlayerDataModel playerDataModel)
        {
            _bombConfig = bombConfig;
            _checkOnInvisibleSystem = checkOnInvisibleSystem;
            _bombEffectsFactory = bombEffectsFactory;
            _playerDataModel = playerDataModel;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            var bombTransform = componentContainer.UnitView.transform;
            var physical = componentContainer.GetComponent<PhysicalComponent>();
            var swipable = componentContainer.GetComponent<SwipableComponent>();
            swipable.RemoveComponent();
            physical.DisablePhysics();
            componentContainer.UnitView.Animator.StopAnimations();
            var explosionAnimation = componentContainer.UnitView.Animator.GetAnimation<ExplosionAnimation>();
            explosionAnimation.StartAnimation(bombTransform,
                () => ProcessExplosion(componentContainer, bombTransform.position));
        }

        private void ProcessExplosion(ComponentContainer componentContainer, Vector3 position)
        {
            var visibleObjects = _checkOnInvisibleSystem.GetVisibleObjects();
            foreach (var item in visibleObjects)
                AddExplosion(position, item.GetContainer().GetComponent<PhysicalComponent>());
            
            _playerDataModel.RemoveHealth();
            componentContainer.Destroy();
            _bombEffectsFactory.Create(_bombConfig.BombPopUpView, _bombConfig.Explosion, position);
        }

        private void AddExplosion(Vector3 bombPosition, PhysicalComponent item)
        {
            var distance = Vector2.Distance(bombPosition, item.Transform.position);
            if (distance <= _bombConfig.ExplosionRadius)
            {
                var heading = item.Transform.position - bombPosition;
                var dir = heading / distance;
                var newVelocity =
                    dir * Mathf.Lerp(_bombConfig.ExplosionPower, 0f, distance / _bombConfig.ExplosionRadius);
                item.SetVelocity(newVelocity);
            }
        }
    }
}