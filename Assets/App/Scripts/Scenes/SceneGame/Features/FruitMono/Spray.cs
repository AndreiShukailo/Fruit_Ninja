using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class Spray : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Construct(Color color, Vector3 position)
        {
            transform.position = position;
            var main = _particleSystem.main;
            main.startColor = color;
            _particleSystem.Play();
        }
    }
}