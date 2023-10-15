using App.Scripts.Scenes.SceneGame.Features.Physics;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    public interface ISwipeable : ISpriteRenderer
    {
        public void Swipe(Vector2 cut);
    }
}