using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Extentions
{
    public static class SpriteRendererExtensions
    {
        public static Rect GetRect(this SpriteRenderer spriteRenderer)
        {
            var bounds = spriteRenderer.bounds;
            return new Rect(bounds.min.x, bounds.min.y,
                bounds.size.x, bounds.size.y);
        }
    }
}