using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones
{
    public class SpawnZone
    {
        private const float TopOffset = 0.8f;
        public readonly float Priority;
        private readonly PhysicalStartVelocityCalculator _physicalStartVelocityCalculator;
        private readonly CameraWrap _cameraWrap;
        private readonly SpawnZoneConfig _spawnZoneConfig;

        public SpawnZone(PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap,
            SpawnZoneConfig spawnZoneConfig)
        {
            _cameraWrap = cameraWrap;
            _spawnZoneConfig = spawnZoneConfig;
            var scaler = new SpawnZonePositionScaler(_cameraWrap, _spawnZoneConfig);
            _physicalStartVelocityCalculator = physicalStartVelocityCalculator;
            Priority = _spawnZoneConfig.Priority;
        }

        public async void Spawn(SpawnedObjectContainer spawnedObjectContainer, float swipeAfter)
        {
            var startPoint = GetRandomPosition(_spawnZoneConfig.GetStartPosition(),
                _spawnZoneConfig.GetEndPosition());
            var maxHeight = GetMaxHeight(startPoint);
            var randomAngel = GetRandomAngel();
            var maxLenght = GetMaxLenght(startPoint, randomAngel);
            var startVelocity = _physicalStartVelocityCalculator.GetStartVelocity(maxHeight, maxLenght, randomAngel,
                spawnedObjectContainer.Mass);
            var spawnedPrefab = GetRandomPrefab(spawnedObjectContainer);
            await spawnedObjectContainer.SpawnedObjectFactoryBase.Create(spawnedPrefab, spawnedObjectContainer.Mass,
                startPoint, startVelocity, swipeAfter);
        }

        private float GetRandomAngel() =>
            Random.Range(_spawnZoneConfig.MinAngle, _spawnZoneConfig.MaxAngle);

        private float GetMaxLenght(Vector2 startPosition, float angle)
        {
            if (angle > 90f)
                return _cameraWrap.HalfWidth + startPosition.x;
            return _cameraWrap.HalfWidth - startPosition.x;
        }

        private float GetMaxHeight(Vector2 startPosition) =>
            _cameraWrap.Position.y + _cameraWrap.OrthographicSize * TopOffset - startPosition.y;

        private Vector2 GetRandomPosition(Vector2 start, Vector2 end)
        {
            var value = Random.value;
            return Vector2.Lerp(start, end, value);
        }

        private UnitView GetRandomPrefab(SpawnedObjectContainer spawnedObjectContainer) =>
            spawnedObjectContainer.Units[Random.Range(0, spawnedObjectContainer.Units.Count)];
    }
}