using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Scenes.SceneGame.Features.Player
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