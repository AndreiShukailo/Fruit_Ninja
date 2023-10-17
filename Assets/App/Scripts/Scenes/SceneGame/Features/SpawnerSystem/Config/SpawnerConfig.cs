using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.Config
{
    [Serializable]
    public class SpawnerConfig
    {
        public List<UnitView> SpawnedObjects;

        [Header("ObjectDelay")] [Range(0.1f, 2f)]
        public float MaxObjectDelay = 1f;

        [Range(0.1f, 1f)] public float ObjectComplicationFactor = 0.1f;

        [Range(0.1f, 1f)] public float MinObjectDelay = 1f;

        [Header("PackCount")] [Range(1, 6)] public int MaxPackCount = 5;
        [Range(1, 6)] public int MinPackCount = 2;

        [Header("PackDelay")] [Range(0.1f, 3f)]
        public float MaxPackDelay = 3f;

        [Range(0.1f, 3f)] public float MinPackDelay = 0.1f;
        [Range(0.1f, 2f)] public float PackComplicationFactor = 0.1f;
    }
}