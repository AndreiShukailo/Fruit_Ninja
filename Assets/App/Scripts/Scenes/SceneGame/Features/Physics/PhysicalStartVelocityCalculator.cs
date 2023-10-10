using App.Scripts.Scenes.SceneGame.Extentions;
using App.Scripts.Scenes.SceneGame.Features.Physics.Config;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics
{
    public class PhysicalStartVelocityCalculator
    {
        private readonly PhysicsConfig _config;

        public PhysicalStartVelocityCalculator(PhysicsConfig config)
        {
            _config = config;
        }

        public Vector3 GetStartVelocity(float height, float lenght, float angle)
        {
            var dirAngle = angle;
            if (angle > 90f)
                angle = 180f - angle;
            var startSpeed = 0f;
            if (dirAngle < 180f && dirAngle != 0f && height > 0f)
            {
                startSpeed = GetLenght(height, angle) > lenght
                    ? GetSpeedByLenght(angle, lenght)
                    : GetSpeedByHeight(angle, height);
            }

            var dir = dirAngle.GetVectorRotation();
            return dir * startSpeed;
        }

        private float GetSpeedByHeight(float angle, float height) =>
            Mathf.Sqrt(2 * Mathf.Abs(height) * _config.Gravity / Mathf.Sin(angle * Mathf.PI / 180) /
                       Mathf.Sin(angle * Mathf.PI / 180));

        private float GetLenght(float height, float angle) =>
            4 * height / Mathf.Tan(angle * Mathf.Deg2Rad);

        private float GetSpeedByLenght(float angle, float lenght) =>
            Mathf.Sqrt(Mathf.Abs(lenght) * _config.Gravity / Mathf.Sin(2 * angle * Mathf.PI / 180));
    }
}