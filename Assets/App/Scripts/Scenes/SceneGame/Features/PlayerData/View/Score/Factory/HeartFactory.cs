using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Health;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory
{
    public class HeartFactory
    {
        public HeartView Create(HeartView unitView, Transform parent)
        {
            var view = Object.Instantiate(unitView, parent);
            view.transform.SetSiblingIndex(0);
            view.Construct();
            return view;
        }
    }
}