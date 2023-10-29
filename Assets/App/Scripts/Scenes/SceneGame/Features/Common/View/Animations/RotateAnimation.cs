using App.Scripts.Scenes.SceneGame.Features.Animation;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Scenes.SceneGame.Features.Common.View.Animations
{
    public class RotateAnimation : AnimationTweenBase
    {
        [SerializeField] private float _maxRotationSpeed = 5f;

        [SerializeField] [Range(1f, 5f)] private float _minRotationSpeed = 2f;

        [SerializeField] [Range(1, 3)] private int _turnCount = 2;

        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            var rotationSpeed = GetRandomRotationSpeed();
            var dir = GetRandomRotationDirection();
            _animation
                .Append(transform.DORotate(new Vector3(0, 0, dir), rotationSpeed, RotateMode.FastBeyond360));
        }

        private float GetRandomRotationSpeed() =>
            Random.Range(_minRotationSpeed, _maxRotationSpeed);

        private float GetRandomRotationDirection()
        {
            float[] dirArray = { 360f * _turnCount, -360f * _turnCount };
            return dirArray[Random.Range(0, dirArray.Length)];
        }
    }
}