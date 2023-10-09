using UnityEngine;

namespace App.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public string StartScene = "Game";
    }
}