using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner.Config
{
    [Serializable]
    public class SpawnerConfig
    {
        public List<Spawned> SpawnedObjects;
        [Range(0.1f, 1f)]
        public float ObjectDelay = 0.5f;
        [Range(1, 6)]
        public int MaxPackCount = 5;
        [Range(1, 6)]
        public int MinPackCount = 5;
        [Range(1f, 5f)]
        public float PackDelay = 3f;
    }
}