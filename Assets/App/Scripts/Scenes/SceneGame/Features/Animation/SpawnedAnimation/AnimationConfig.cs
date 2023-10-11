using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation
{
    [Serializable]
    public class AnimationConfig
    {
        [Header("IncreaseDecreaseAnimation")] [Range(1f, 5f)]
        public float IncreaseDecreaseDuration = 2f;

        [Range(1f, 2f)] public float MaxScale = 1.2f;
        [Range(0.1f, 1f)] public float MinScale = 0.9f;

        [Header("RotateAnimation")] [Range(1f, 5f)]
        public float MaxRotationSpeed = 5f;

        [Range(1f, 5f)] public float MinRotationSpeed = 2f;

        [Range(1, 3)] public int TurnCount = 2;
    }
}