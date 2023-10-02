using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services
{
    public class GameMath
    {
        private readonly float _gravity;

        public GameMath(GameData gameData)
        {
            _gravity = gameData.Gravity;
        }


        public Vector3 TestStartVelocity(float height, float distance, bool isLeft)
        {
            var speed = Mathf.Sqrt((distance * distance * _gravity / 8) + (height * _gravity));
            var angle = Mathf.Asin(Mathf.Sqrt(2 * _gravity * height / speed / speed)) * Mathf.Rad2Deg;
            var dir = GetDirection(angle);
            if (!isLeft)
                dir = TurnDirToRight(dir);
            return dir * speed;
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