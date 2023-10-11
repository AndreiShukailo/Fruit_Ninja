using App.Scripts.Infrastructure.ServiceLocator;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Fruit;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Mono;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.Shadow.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono
{
    public class Spawned : MonoBehaviour
    {
        public Physical Physical;
        public SpawnedAnimator _animator;
        [SerializeField] private FruitPiecesCreator _fruitPiecesCreator;
        [SerializeField] private FruitSwipeComponent _fruitSwipeComponent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private DestroyWhenInvisible _destroyWhenInvisible;
        [SerializeField] private Shadowed _shadowed;

        public void Construct(GamePhysics gamePhysics, CameraWrap cameraWrap, ShadowSystem shadowSystem,
            SpawnedAnimationConfig animationConfig, SwipeTracker swipeTracker, Vector3 startPosition,
            ServiceContainer serviceContainer)
        {
            var pieceFactory = new PieceFactory(serviceContainer);
            Physical.Construct(gamePhysics);
            _fruitPiecesCreator.Construct(pieceFactory);
            _fruitSwipeComponent.Construct(swipeTracker, _spriteRenderer, _fruitPiecesCreator);
            _destroyWhenInvisible.Construct(cameraWrap, _spriteRenderer);
            _shadowed.Construct(shadowSystem, _spriteRenderer, startPosition);
            _animator.StartAnimations(animationConfig);
        }
    }
}