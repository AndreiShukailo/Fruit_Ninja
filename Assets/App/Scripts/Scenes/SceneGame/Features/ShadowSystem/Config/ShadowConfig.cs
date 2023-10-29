using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.ShadowSystem.Config
{
    [Serializable]
    public class ShadowConfig
    {
        [Range(0.1f, 1f)] public float ShadowOffset = 0.2f;
        [Range(0.1f, 1f)] public float Transparent = 0.3f;
        [Range(1f, 3f)] public float ScaleFactor = 2f;
        public Transform ShadowContainer;
        public SpriteRenderer ShadowPrefab;
    }
}