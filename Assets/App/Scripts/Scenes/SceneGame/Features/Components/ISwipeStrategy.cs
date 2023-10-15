using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public interface ISwipeStrategy
    {
        public void Swipe(Vector2 cut, ComponentContainer componentContainer);
    }
}