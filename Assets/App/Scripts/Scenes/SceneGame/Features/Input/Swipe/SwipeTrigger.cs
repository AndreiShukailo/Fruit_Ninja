using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    [RequireComponent(typeof(Collider2D))]
    public class SwipeTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<SwipeableComponent>(out SwipeableComponent component))
                component.OnSwipe();
        }
    }
}