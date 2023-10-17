using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation
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