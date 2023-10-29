using UnityEngine;

namespace App.Scripts.Scenes.Common.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgressKey";

        public PlayerProgress Load() =>
            JsonUtility.FromJson<PlayerProgress>(PlayerPrefs.GetString(PlayerProgressKey));

        public void Save(PlayerProgress playerProgress) =>
            PlayerPrefs.SetString(PlayerProgressKey, JsonUtility.ToJson(playerProgress));
    }
}