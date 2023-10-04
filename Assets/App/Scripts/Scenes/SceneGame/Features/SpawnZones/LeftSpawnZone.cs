using App.Scripts.Infrastructure;
using App.Scripts.Scenes.SceneGame.Features.Camera;
using App.Scripts.Scenes.SceneGame.Features.Physics;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones
{
    public class LeftSpawnZone : SpawnZone
    {
        public LeftSpawnZone(FruitFactory fruitFactory, PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap, SpawnZoneConfig.SpawnZoneConfig spawnZoneConfig) :
            base(fruitFactory, physicalStartVelocityCalculator, cameraWrap, spawnZoneConfig)
        {
        }

        public override void Spawn() =>
            SpawnOnTheSide(true);
    }
}