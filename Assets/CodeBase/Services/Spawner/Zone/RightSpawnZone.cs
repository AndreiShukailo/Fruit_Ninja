using CodeBase.Infrastructure;
using CodeBase.StaticData;

namespace CodeBase.Services.Spawner.Zone
{
    public class RightSpawnZone : SpawnZone
    {
        public RightSpawnZone(FruitFactory fruitFactory, GameMath gameMath, CameraWrap cameraWrap, SpawnZoneData spawnZoneData) :
            base(fruitFactory, gameMath, cameraWrap, spawnZoneData)
        {
        }

        public override void Spawn() =>
            SpawnOnTheSide(false);
    }
}