using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class SwipeableComponent : MonoBehaviour
    {
        public abstract void OnSwipe();
    }
}