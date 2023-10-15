using UnityEngine;

namespace App.Scripts.Scenes.Common
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public int BestScore;
        public int StartScore = 0;
        public int StartHealth = 3;
    }
}