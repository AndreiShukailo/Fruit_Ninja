using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Spray
{
    public class FruitParticleFactory
    {
        public void Create(SprayView particle, Vector2 position, Color color)
        {
            var temp = Object.Instantiate(particle);
            temp.Construct(color, position);
        }
    }
}