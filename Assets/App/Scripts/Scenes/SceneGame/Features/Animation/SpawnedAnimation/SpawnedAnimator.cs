using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation
{
    [Serializable]
    public class SpawnedAnimator
    {
        [SerializeField] private List<SpawnedAnimationTweenBase> _animations;

        public void StartAnimations(SpawnedAnimationConfig animationConfig)
        {
            foreach (var animation in _animations)
            {
                animation.Construct(animationConfig);
                animation.StartAnimation();
            }
        }
    }
}