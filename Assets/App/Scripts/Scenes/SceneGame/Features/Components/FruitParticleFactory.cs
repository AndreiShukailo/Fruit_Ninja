using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class FruitParticleFactory
    {
        public void Create(Spray particle, Vector2 position, Color color)
        {
            var temp = Object.Instantiate(particle);
            temp.Construct(color, position);
        }
    }
}