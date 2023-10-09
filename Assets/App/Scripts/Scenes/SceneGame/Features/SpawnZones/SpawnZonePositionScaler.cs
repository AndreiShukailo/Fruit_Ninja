using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnZones
{
    public class SpawnZonePositionScaler
    {
        private readonly CameraWrap _cameraWrap;
        private readonly SpawnZoneConfig _zoneConfig;

        public SpawnZonePositionScaler(CameraWrap cameraWrap, SpawnZoneConfig zoneConfig)
        {
            _cameraWrap = cameraWrap;
            _zoneConfig = zoneConfig;
            _cameraWrap.OnAspectChanged += OnAspectChanged;
        }

        private void OnAspectChanged()
        {
            UpdateZonePosition(_cameraWrap.PrevAspect, _zoneConfig, _cameraWrap.Aspect);
        }
        
        public static void UpdateZonePosition(float prevAspect, SpawnZoneConfig zoneConfig, float newAspect)
        {
            var zoneTransform = zoneConfig.transform;
            var aspect = newAspect;
            var aspectDelta = aspect / prevAspect;

            var position = zoneTransform.position;
            position = GetScaledPosition(position, aspectDelta);
            zoneConfig.transform.position = position;

            if (zoneTransform.rotation.eulerAngles == Vector3.zero)
                zoneConfig.Lenght *= aspectDelta;
        }

        private static Vector2 GetScaledPosition(Vector3 position, float aspectDelta) =>
            new(position.x * aspectDelta, position.y);
    }
}