using CodeBase.Infrastructure;
using CodeBase.StaticData;

namespace CodeBase.Services.Spawner.Zone
{
    public class LeftSpawnZone : SpawnZone
    {
        public LeftSpawnZone(FruitFactory fruitFactory, GameMath gameMath, CameraWrap cameraWrap, SpawnZoneData spawnZoneData) :
            base(fruitFactory, gameMath, cameraWrap, spawnZoneData)
        {
        }

        public override void Spawn() =>
            SpawnOnTheSide(true);
    }
}