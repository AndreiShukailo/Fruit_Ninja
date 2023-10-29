using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.Services
{
    public class PhysicalStartVelocityCalculator
    {
        private readonly PhysicsConfig _config;

        public PhysicalStartVelocityCalculator(PhysicsConfig config)
        {
            _config = config;
        }

        public Vector3 GetStartVelocity(float height, float lenght, float angle, float mass)
        {
            var dirAngle = angle;
            if (angle > 90f)
                angle = 180f - angle;
            var startSpeed = 0f;
            if (dirAngle < 180f && dirAngle != 0f && height > 0f)
            {
                startSpeed = GetLenght(height, angle) > lenght
                    ? GetSpeedByLenght(angle, lenght, mass)
                    : GetSpeedByHeight(angle, height, mass);
            }

            var dir = dirAngle.GetVectorRotation();
            return dir * startSpeed;
        }

        private float GetSpeedByHeight(float angle, float height, float mass) =>
            Mathf.Sqrt(2 * Mathf.Abs(height) * _config.Gravity * mass / Mathf.Sin(angle * Mathf.PI / 180) /
                       Mathf.Sin(angle * Mathf.PI / 180));

        private float GetLenght(float height, float angle) =>
            4 * height / Mathf.Tan(angle * Mathf.Deg2Rad);

        private float GetSpeedByLenght(float angle, float lenght, float mass) =>
            Mathf.Sqrt(Mathf.Abs(lenght) * _config.Gravity * mass / Mathf.Sin(2 * angle * Mathf.PI / 180));
    }
}