using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
{
    public class Fruit : MonoBehaviour, IPhysical, ICheckVisible, ISwipeable
    {
        public bool IsVisible { get; set; }
        public Transform Transform => transform;
        public Vector3 Velocity { get; set; }
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private FruitPiecesCreator _fruitPiecesCreator;
        [SerializeField] private ObjectdAnimator _animator;
        private GamePhysics _physics;
        private CheckOnInVisible _checkOnInVisible;
        private SwipeTracker _swipeTracker;
        private ShadowSystem _shadowSystem;


        public void Construct(GamePhysics physics, CheckOnInVisible checkOnInVisible, SwipeTracker swipeTracker,
            ShadowSystem shadowSystem, AnimationConfig animationConfig, PieceFactory pieceFactory,
            SpotFactory spotFactory)
        {
            _physics = physics;
            _checkOnInVisible = checkOnInVisible;
            _swipeTracker = swipeTracker;
            _shadowSystem = shadowSystem;
            _physics.AddObject(this);
            _checkOnInVisible.AddObject(this);
            _swipeTracker.Add(this);
            _shadowSystem.Add(this, transform.position);
            _animator.StartAnimations(animationConfig);
            _fruitPiecesCreator.Construct(pieceFactory, spotFactory);
        }

        public void Swipe(Vector2 cut, Transform transform)
        {
            _fruitPiecesCreator.CreatePieces(cut, transform);
            Destroy(this.gameObject);
        }

        public void OnInvisible()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _physics.RemoveObject(this);
            _checkOnInVisible.RemoveObject(this);
            _swipeTracker.Remove(this);
            _shadowSystem.Remove(this);
        }
    }
}