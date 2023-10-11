using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono.Pieces
{
    public class Piece : MonoBehaviour, IPhysical, ICheckVisible
    {
        public bool IsVisible { get; set; }
        public Transform Transform => transform;
        public Vector3 Velocity { get; set; }
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ObjectdAnimator _animator;
        private GamePhysics _physics;
        private CheckOnInVisible _checkOnInVisible;
        private ShadowSystem _shadowSystem;


        public void Construct(GamePhysics physics, CheckOnInVisible checkOnInVisible,
            ShadowSystem shadowSystem, AnimationConfig animationConfig)
        {
            _physics = physics;
            _checkOnInVisible = checkOnInVisible;
            _shadowSystem = shadowSystem;
            _physics.AddObject(this);
            _checkOnInVisible.AddObject(this);
            _shadowSystem.Add(this, transform.position);
            _animator.StartAnimations(animationConfig);
        }

        public void OnInvisible()
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            _physics.RemoveObject(this);
            _checkOnInVisible.RemoveObject(this);
            _shadowSystem.Remove(this);
        }
    }
}