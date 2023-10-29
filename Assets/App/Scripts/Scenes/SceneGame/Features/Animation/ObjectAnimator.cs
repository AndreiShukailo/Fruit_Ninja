using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation
{
    [Serializable]
    public class ObjectAnimator
    {
        [SerializeField] private List<AnimationTweenBase> _animations;
        [SerializeField] private List<AnimationTweenBase> _additionalAnimations;

        public void StartAnimations()
        {
            foreach (var animation in _animations)
                animation.StartAnimation();
        }

        public void StopAnimations()
        {
            foreach (var animation in _animations)
                animation.StopAnimation();
        }

        public TAnimation GetAnimation<TAnimation>() where TAnimation : AnimationTweenBase
        {
            var findAnimation = _additionalAnimations.Find(p => p is TAnimation);
            return (TAnimation)findAnimation;
        }
    }
}