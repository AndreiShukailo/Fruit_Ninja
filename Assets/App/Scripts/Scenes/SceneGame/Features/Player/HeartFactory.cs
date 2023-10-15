using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class HeartFactory
    {
        public HeartView Create(HeartView unitView, Transform parent)
        {
            var view = Object.Instantiate(unitView, parent);
            view.Construct();
            return view;
        }
    }
}