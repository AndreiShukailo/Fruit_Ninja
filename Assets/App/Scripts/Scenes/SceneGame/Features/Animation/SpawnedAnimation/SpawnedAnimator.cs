using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation
{
    [Serializable]
    public class SpawnedAnimator
    {
        [SerializeField] private List<SpawnedAnimationTweenBase> _animations;

        public void InitConfig(SpawnedAnimationConfig animationConfig)
        {
            foreach (var animation in _animations)
                animation.InitConfig(animationConfig);
        }

        public void StartAnimations()
        {
            foreach (var animation in _animations)
                animation.StartAnimation();
        }
    }
}