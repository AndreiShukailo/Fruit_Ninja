using System;
using App.Scripts.Scenes.SceneGame.Features.Fruits;

namespace App.Scripts.Scenes.SceneGame.Features.Items.FruitBox
{
    [Serializable]
    public class FruitBoxConfig
    {
        public int FruitCount = 3;
        public FruitsContainer FruitsContainer;
        public float ImmortalityDuration = 0.5f;
    }
}