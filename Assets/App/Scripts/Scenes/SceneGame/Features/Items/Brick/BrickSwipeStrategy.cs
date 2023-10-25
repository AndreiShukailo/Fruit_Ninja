using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Brick
{
    public class BrickSwipeStrategy : ISwipeStrategy
    {
        private readonly PlayerInput _playerInput;

        public BrickSwipeStrategy(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            _playerInput.SetBlocked();
        }
    }
}