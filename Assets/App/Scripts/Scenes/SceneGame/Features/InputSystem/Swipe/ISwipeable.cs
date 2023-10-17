using App.Scripts.Scenes.SceneGame.Features.Common;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe
{
    public interface ISwipeable : ISpriteRenderer
    {
        public void Swipe(Vector2 cut);
    }
}