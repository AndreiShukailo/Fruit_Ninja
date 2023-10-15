using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation
{
    [Serializable]
    public class ObjectAnimator
    {
        [SerializeField] private List<AnimationTweenBase> _animations;

        public void StartAnimations()
        {
            foreach (var animation in _animations)
                animation.StartAnimation();
        }
    }
}