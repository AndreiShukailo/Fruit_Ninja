using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation
{
    public class PieceAnimation : AnimationTweenBase
    {
        [SerializeField] private float _duration = 2f;
        [SerializeField] private float _targetAngle = 90;
        public override void StartAnimation()
        {
            _animation = DOTween.Sequence();
            _animation.Append(transform.DORotate(new Vector3(0, 0, _targetAngle), _duration));
        }
    }
}