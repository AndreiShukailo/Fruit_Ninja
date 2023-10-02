using CodeBase.Infrastructure;
using CodeBase.Services.Spawner.Zone;
using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Spawner
{
    public class GameSpawner : ITickable
    {
        private SpawnZone[] _zones;

        public GameSpawner(GameData data, FruitFactory fruitFactory, GameMath gameMath, CameraWrap cameraWrap)
        {
            InitZones(data, fruitFactory, gameMath, cameraWrap);
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var zone = _zones[Random.Range(0, _zones.Length)];
                zone.Spawn();
            }
        }

        private void InitZones(GameData data, FruitFactory fruitFactory, GameMath gameMath, CameraWrap cameraWrap)
        {
            _zones = new SpawnZone[data.Zones.Count];
            for (var i = 0; i < _zones.Length; i++)
            {
                var zoneData = data.Zones[i];
                var type = data.Zones[i].Type;
                _zones[i] = GetSpawnZone(type, fruitFactory, gameMath, cameraWrap, zoneData);
            }
        }

        private SpawnZone GetSpawnZone(SpawnZoneType type, FruitFactory fruitFactory, GameMath gameMath,
            CameraWrap cameraWrap, SpawnZoneData zoneData)
        {
            return type switch
            {
                SpawnZoneType.Left => new LeftSpawnZone(fruitFactory, gameMath, cameraWrap, zoneData),
                SpawnZoneType.Right => new RightSpawnZone(fruitFactory, gameMath, cameraWrap, zoneData),
                SpawnZoneType.Bottom => new BottomSpawnZone(fruitFactory, gameMath, cameraWrap, zoneData),
                _ => new LeftSpawnZone(fruitFactory, gameMath, cameraWrap, zoneData)
            };
        }
    }
}