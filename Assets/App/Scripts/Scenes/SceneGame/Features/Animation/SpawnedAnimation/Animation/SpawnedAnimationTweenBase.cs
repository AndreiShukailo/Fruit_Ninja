using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public abstract class SpawnedAnimationTweenBase : MonoBehaviour
    {
        protected SpawnedAnimationConfig _config;
        protected Sequence _animation;

        public void InitConfig(SpawnedAnimationConfig config) =>
            _config = config;

        public abstract void StartAnimation();

        private void StopAnimation() =>
            _animation.Kill();

        private void OnDestroy() =>
            StopAnimation();
    }
}