using CodeBase.Infrastructure;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.Spawner.Zone
{
    public abstract class SpawnZone : ISpawnZone
    {
        public float Priority { get; }

        private readonly FruitFactory _fruitFactory;
        private readonly GameMath _gameMath;
        private readonly CameraWrap _cameraWrap;

        protected SpawnZone(FruitFactory fruitFactory, GameMath gameMath, CameraWrap cameraWrap, SpawnZoneData spawnZoneData)
        {
            _cameraWrap = cameraWrap;
            _fruitFactory = fruitFactory;
            _gameMath = gameMath;
            Priority = spawnZoneData.Priority;
        }

        public virtual void Spawn()
        {
        }

        protected void SpawnOnTheSide(bool leftSide)
        {
            var spawnPoint = GetSpawnPointSide(leftSide, out var pointY);

            var distance = GetDistance();
            var height = GetHeight(pointY);

            var startVelocity = _gameMath.GetStartVelocity(height, distance, leftSide);
            _fruitFactory.Create(spawnPoint, startVelocity);
        }

        protected void SpawnOnTheBottom()
        {
            var spawnPoint = GetSpawnPointBottom(out var pointX, out var pointY);

            var height = GetHeight(pointY);
            var endPointX = GetPointXBottom();
            var distance = Mathf.Abs(endPointX - pointX);

            var startVelocity = _gameMath.GetStartVelocity(height, distance, !(endPointX < pointX));
            _fruitFactory.Create(spawnPoint, startVelocity);
        }

        private Vector3 GetSpawnPointBottom(out float pointX, out float pointY)
        {
            pointX = GetPointXBottom();
            pointY = GetPointYBottom();
            
            return GetSpawnPoint(pointX, pointY);
        }

        private float GetPointXBottom() =>
            Random.Range(_cameraWrap.LeftBoarder, _cameraWrap.RightBoarder);

        private float GetPointYBottom() =>
            _cameraWrap.OrthographicSize + 1;

        private Vector3 GetSpawnPointSide(bool leftSide, out float pointY)
        {
            var pointX = GetPointXSide(leftSide);
            pointY = GetPointYSide();
            
            return GetSpawnPoint(pointX, pointY);
        }

        private float GetHeight(float pointY)
        {
            var height = Random.Range(_cameraWrap.OrthographicSize, _cameraWrap.Height);
            height -= _cameraWrap.OrthographicSize - pointY;
            
            return height;
        }


        private float GetDistance() =>
            Random.Range(_cameraWrap.HalfWidth, _cameraWrap.Width);

        private Vector3 GetSpawnPoint(float pointX, float pointY) =>
            new(pointX, _cameraWrap.Position.y -  pointY, 0);

        private float GetPointYSide()
        {
            var minPointY = 0.2f * _cameraWrap.OrthographicSize;
            var maxPointY = _cameraWrap.OrthographicSize;
            
            return Random.Range(minPointY, maxPointY);
        }

        private float GetPointXSide(bool leftSide) =>
            leftSide ? _cameraWrap.LeftBoarder - 1 : _cameraWrap.RightBoarder + 1;
    }
}