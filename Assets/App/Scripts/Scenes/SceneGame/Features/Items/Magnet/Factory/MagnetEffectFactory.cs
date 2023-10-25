using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Factory
{
    public class MagnetEffectFactory
    {
        public void Create(MagneticEffectView particle, Vector3 position, float duration, float radius)
        {
            var temp = Object.Instantiate(particle);
            temp.Construct(position, duration, radius);
        }
    }
}