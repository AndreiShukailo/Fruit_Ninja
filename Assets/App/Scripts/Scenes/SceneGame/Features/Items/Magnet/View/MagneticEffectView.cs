using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet.View
{
    public class MagneticEffectView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Construct(Vector3 position, float duration, float radius)
        {
            transform.position = new Vector3(position.x, position.y, -1f);
            var mainModule = _particleSystem.main;
            mainModule.duration = duration;
            mainModule.startSize = radius * 2;
            _particleSystem.Play();
        }
    }
}