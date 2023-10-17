using System;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Config
{
    [Serializable]
    public class PhysicsConfig
    {
        [Range(5f, 20f)]
        public float Gravity = 7f;
    }
}