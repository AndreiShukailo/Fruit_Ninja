using App.Scripts.Infrastructure.Configs;
using App.Scripts.Scenes.SceneGame.Features.Camera;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using UnityEngine;

namespace App.Scripts.Infrastructure
{
    public class FruitFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly CameraWrap _cameraWrap;
        private readonly GamePhysics _physics;

        public FruitFactory(GameConfig gameConfig, CameraWrap cameraWrap, GamePhysics physics)
        {
            _gameConfig = gameConfig;
            _cameraWrap = cameraWrap;
            _physics = physics;
        }

        public GameObject Create(Vector3 position, Vector3 startVelocity)
        {
            var prefab = GetRandomPrefab();
            var temp = Object.Instantiate(prefab);
            temp.transform.position = position;
            var physical = temp.GetComponent<Physical>();
            physical.Construct(_physics);
            physical.AddVelocity(startVelocity);
            temp.GetComponent<DestroyWhenInvisible>().Construct(_cameraWrap);
            return temp;
        }

        private GameObject GetRandomPrefab() =>
            _gameConfig.FruitPrefabs[Random.Range(0, _gameConfig.FruitPrefabs.Count)];
    }
}