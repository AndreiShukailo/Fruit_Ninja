using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject;
using App.Scripts.Scenes.SceneGame.Features.Spawner.Config;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones
{
    public class ZoneInitializer
    {
        private readonly SpawnZone[] _zones;

        public ZoneInitializer(SpawnZoneConfigs config, FruitFactory fruitFactory,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap,
            SpawnerConfig spawnerConfig)
        {
            _zones = new SpawnZone[config.Zones.Count];
            for (var i = 0; i < _zones.Length; i++)
            {
                _zones[i] = new SpawnZone(fruitFactory, physicalStartVelocityCalculator, cameraWrap,
                    config.Zones[i], spawnerConfig);
            }
        }

        public SpawnZone GetRandomZone()
        {
            if (_zones.Length == 0) return null;
            var probs = new float[_zones.Length];
            for (int i = 0; i < _zones.Length; i++)
                probs[i] = _zones[i].Priority / _zones.Length;

            return _zones[GetRandomZoneNumber(probs)];
        }

        private int GetRandomZoneNumber(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                    return i;

                randomPoint -= probs[i];
            }

            return probs.Length - 1;
        }
    }
}