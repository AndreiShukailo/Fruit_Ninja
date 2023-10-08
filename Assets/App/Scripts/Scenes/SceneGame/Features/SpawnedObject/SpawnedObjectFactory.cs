using App.Scripts.Infrastructure.Configs;
using App.Scripts.Scenes.SceneGame.Features.Animation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject
{
    public class SpawnedObjectFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly CameraWrap _cameraWrap;
        private readonly GamePhysics _physics;
        private readonly ShadowSystem _shadowSystem;
        private readonly SpawnedAnimationConfig _spawnedAnimationConfig;

        public SpawnedObjectFactory(GameConfig gameConfig, CameraWrap cameraWrap, GamePhysics physics,
            ShadowSystem shadowSystem, SpawnedAnimationConfig spawnedAnimationConfig)
        {
            _gameConfig = gameConfig;
            _cameraWrap = cameraWrap;
            _physics = physics;
            _shadowSystem = shadowSystem;
            _spawnedAnimationConfig = spawnedAnimationConfig;
        }

        public Spawned Create(Vector3 position, Vector3 startVelocity)
        {
            var prefab = GetRandomPrefab();
            var spawned = Object.Instantiate(prefab);
            spawned.Construct(_physics, _cameraWrap, _shadowSystem, _spawnedAnimationConfig, position);
            spawned.transform.position = position;
            spawned.Physical.AddVelocity(startVelocity);
            return spawned;
        }

        private Spawned GetRandomPrefab() =>
            _gameConfig.ObjectsPrefab[Random.Range(0, _gameConfig.ObjectsPrefab.Count)];
    }
}