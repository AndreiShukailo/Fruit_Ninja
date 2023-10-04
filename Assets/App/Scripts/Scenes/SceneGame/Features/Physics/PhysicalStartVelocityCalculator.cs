using App.Scripts.Infrastructure.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class PhysicalStartVelocityCalculator
    {
        private readonly float _gravity;

        public PhysicalStartVelocityCalculator(GameConfig gameConfig)
        {
            _gravity = gameConfig.Gravity;
        }

        public Vector3 GetStartVelocity(float height, float distance, bool isLeft)
        {
            var angle = GetAngle(height, distance);
            var startSpeed= GetSpeed(angle, height);
            var dir = GetDirection(angle);
            if (!isLeft)
                dir = TurnDirToRight(dir);
            
            return dir * startSpeed;
        }

        private Vector3 TurnDirToRight(Vector3 dir) =>
            new(dir.x * -1f, dir.y, 0);

        private Vector3 GetDirection(float angle) =>
            new(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f), 0);

        private float GetAngle(float height, float distance) =>
            Mathf.Atan( 4* height /  distance) * Mathf.Rad2Deg;

        private float GetSpeed(float angle, float height) =>
            Mathf.Sqrt((2* height * _gravity) / Mathf.Sin(angle * Mathf.PI / 180) / Mathf.Sin(angle * Mathf.PI / 180));
    }
}