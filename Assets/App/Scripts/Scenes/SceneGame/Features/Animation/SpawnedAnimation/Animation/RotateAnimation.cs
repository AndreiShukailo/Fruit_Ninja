using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class RotateAnimation : AnimationTweenBase
    {
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            var rotationSpeed = GetRandomRotationSpeed();
            var dir = GetRandomRotationDirection();
            _animation
                .Append(transform.DORotate(new Vector3(0, 0, dir), rotationSpeed, RotateMode.FastBeyond360));
        }

        private float GetRandomRotationSpeed() =>
            Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);

        private float GetRandomRotationDirection()
        {
            float[] dirArray = new[] { 360f * _config.TurnCount, -360f * _config.TurnCount };
            return dirArray[Random.Range(0, dirArray.Length)];
        }
    }
}