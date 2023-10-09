using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation
{
    [Serializable]
    public class SpawnedAnimationConfig
    {
        [Header("IncreaseDecreaseAnimation")] [Range(1f, 5f)]
        public float IncreaseDecreaseDuration = 2f;
        [Range(1f, 2f)]
        public float MaxScale = 1.2f;
        [Range(0.1f, 1f)]
        public float MinScale = 0.9f;
        
        public float MaxRotationSpeed = 1f;

        public float MinRotationSpeed = 0.1f;
    }
}