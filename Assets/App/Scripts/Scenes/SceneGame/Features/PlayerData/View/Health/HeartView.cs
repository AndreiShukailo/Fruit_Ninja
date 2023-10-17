using App.Scripts.Scenes.SceneGame.Features.Animation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField] private AnimationTweenBase _increaseAnimation;
        [SerializeField] private AnimationTweenBase _destroyAnimation;

        public void Construct() =>
            _increaseAnimation.StartAnimation();

        public void Deconstruct() =>
            _destroyAnimation.StartAnimation();
    }
}