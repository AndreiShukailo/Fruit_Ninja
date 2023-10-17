using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services
{
    public interface ISwipeStrategy
    {
        public void Swipe(Vector2 cut, ComponentContainer componentContainer);
    }
}