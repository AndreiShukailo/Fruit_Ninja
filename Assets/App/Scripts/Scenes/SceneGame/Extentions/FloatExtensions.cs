using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Extentions
{
    public static class FloatExtensions
    {
        public static Vector2 GetVectorRotation(this float angle) =>
            new Vector3(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));
    }
}