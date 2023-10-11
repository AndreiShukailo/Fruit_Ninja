using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    public interface ISwipeable
    {
        public Rect Rect { get; }
        public void Swipe(Vector2 cut, Vector2 position);
    }
}