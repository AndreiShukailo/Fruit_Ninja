using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation
{
    [Serializable]
    public class ObjectdAnimator
    {
        [SerializeField] private List<AnimationTweenBase> _animations;

        public void StartAnimations(AnimationConfig animationConfig)
        {
            foreach (var animation in _animations)
            {
                animation.Construct(animationConfig);
                animation.StartAnimation();
            }
        }
    }
}