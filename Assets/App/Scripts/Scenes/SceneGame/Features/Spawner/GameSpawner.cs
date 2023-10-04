using App.Scripts.Infrastructure;
using App.Scripts.Infrastructure.Configs;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Scenes.SceneGame.Features.Camera;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.SpawnZoneConfig;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Spawner
{
    public class GameSpawner : ITickable
    {
        private SpawnZone[] _zones;

        public GameSpawner(GameConfig config, FruitFactory fruitFactory, PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap)
        {
            InitZones(config, fruitFactory, physicalStartVelocityCalculator, cameraWrap);
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var zone = _zones[Random.Range(0, _zones.Length)];
                zone.Spawn();
            }
        }

        private void InitZones(GameConfig config, FruitFactory fruitFactory, PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap)
        {
            _zones = new SpawnZone[config.Zones.Count];
            for (var i = 0; i < _zones.Length; i++)
            {
                var zoneData = config.Zones[i];
                var type = config.Zones[i].Type;
                _zones[i] = GetSpawnZone(type, fruitFactory, physicalStartVelocityCalculator, cameraWrap, zoneData);
            }
        }

        private SpawnZone GetSpawnZone(SpawnZoneType type, FruitFactory fruitFactory, PhysicalStartVelocityCalculator physicalStartVelocityCalculator,
            CameraWrap cameraWrap, SpawnZoneConfig zoneConfig)
        {
            return type switch
            {
                SpawnZoneType.Left => new LeftSpawnZone(fruitFactory, physicalStartVelocityCalculator, cameraWrap, zoneConfig),
                SpawnZoneType.Right => new RightSpawnZone(fruitFactory, physicalStartVelocityCalculator, cameraWrap, zoneConfig),
                SpawnZoneType.Bottom => new BottomSpawnZone(fruitFactory, physicalStartVelocityCalculator, cameraWrap, zoneConfig),
                _ => new LeftSpawnZone(fruitFactory, physicalStartVelocityCalculator, cameraWrap, zoneConfig)
            };
        }
    }
}