using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Common.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Spot
{
    public class FruitSpotFactory
    {
        public void CreateSpot(UnitView prefab, Color color, Transform transform)
        {
            var view = Object.Instantiate(prefab);
            view.SpriteRenderer.color = color;
            var transform1 = view.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            view.Animator.StartAnimations();
        }
    }
}