using UnityEditor;
using UnityEngine;

namespace App.Scripts.Scenes.Editor
{
    public class Tools
    {
        [MenuItem("FruitNinja/ClearPlayerProgress")]
        public static void ClearPlayerProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}