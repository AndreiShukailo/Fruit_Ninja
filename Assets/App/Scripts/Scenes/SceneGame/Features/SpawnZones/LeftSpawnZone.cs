using App.Scripts.Infrastructure;
using App.Scripts.Scenes.SceneGame.Features.Physics;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones
{
    public class LeftSpawnZone : SpawnZone
    {
        public LeftSpawnZone(PhysicalFactory physicalFactory, PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap.CameraWrap cameraWrap, SpawnZoneConfig.SpawnZoneConfig spawnZoneConfig) :
            base(physicalFactory, physicalStartVelocityCalculator, cameraWrap, spawnZoneConfig)
        {
        }

        public override void Spawn() =>
            SpawnOnTheSide(true);
    }
}