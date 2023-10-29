using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation
{
    public abstract class AnimationTweenBase : MonoBehaviour
    {
        protected Sequence _animation;

        public abstract void StartAnimation();

        public void StopAnimation() =>
            _animation.Kill();

        private void OnDestroy() =>
            StopAnimation();
    }
}