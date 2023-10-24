using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem.SpawnZones.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.FruitBox
{
    public class FruitBoxSwipeStrategy : ISwipeStrategy
    {
        private readonly FruitBoxConfig _fruitBoxConfig;
        private readonly PhysicalStartVelocityCalculator _physicalStartVelocityCalculator;
        private readonly CameraWrap _cameraWrap;

        public FruitBoxSwipeStrategy(FruitBoxConfig fruitBoxConfig,
            PhysicalStartVelocityCalculator physicalStartVelocityCalculator, CameraWrap cameraWrap)
        {
            _fruitBoxConfig = fruitBoxConfig;
            _physicalStartVelocityCalculator = physicalStartVelocityCalculator;
            _cameraWrap = cameraWrap;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            if (!componentContainer.GetComponent<CheckOnInvisibleComponent>().IsVisible) return;
            var spawnZone = CreateSpawnZone(componentContainer);

            for (int i = 0; i < _fruitBoxConfig.FruitCount; i++)
                spawnZone.Spawn(_fruitBoxConfig.FruitsContainer, _fruitBoxConfig.ImmortalityDuration);

            componentContainer.Destroy();
        }

        private SpawnZone CreateSpawnZone(ComponentContainer componentContainer)
        {
            var spawnZoneConfig = componentContainer.UnitView.GetComponent<SpawnZoneConfig>();
            return new SpawnZone(_physicalStartVelocityCalculator, _cameraWrap, spawnZoneConfig);
        }
    }
}