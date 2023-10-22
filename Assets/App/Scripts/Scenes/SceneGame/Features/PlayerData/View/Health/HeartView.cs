using App.Scripts.Scenes.SceneGame.Features.Animation;
using App.Scripts.Scenes.SceneGame.Features.Items.Heart.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField] private AnimationTweenBase _increaseAnimation;
        [SerializeField] private AnimationTweenBase _destroyAnimation;
        [SerializeField] private HealthItemAnimation _healthItemAnimation;

        public void Construct() =>
            _increaseAnimation.StartAnimation();

        public void Construct(Vector3 position, Vector3 endPosition, Transform parent)
        {
            transform.position = position;
            _healthItemAnimation.StartAnimation(endPosition, () => OnHealthAnimationEnd(parent));
        }

        public void Deconstruct() =>
            _destroyAnimation.StartAnimation();

        public void OnHealthAnimationEnd(Transform parent)
        {
            transform.SetParent(parent);
            transform.SetSiblingIndex(0);
        }
    }
}