using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Brick.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Brick
{
    public class BrickSwipeStrategy : ISwipeStrategy
    {
        private readonly PlayerInput _playerInput;
        private readonly BrickConfig _brickConfig;

        public BrickSwipeStrategy(PlayerInput playerInput, BrickConfig brickConfig)
        {
            _playerInput = playerInput;
            _brickConfig = brickConfig;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            _playerInput.SetBlocked();
            var brickEffect = Object.Instantiate(_brickConfig.BrickEffect);
            var brickPosition = componentContainer.UnitView.transform.position;
            brickEffect.transform.position = brickPosition;
        }
    }
}