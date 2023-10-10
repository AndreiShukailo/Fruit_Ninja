using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruit
{
    public class FruitComponent : SwipeableComponent
    {
        public override void OnSwipe()
        {
            Debug.Log("Swipe");            
        }
    }
}