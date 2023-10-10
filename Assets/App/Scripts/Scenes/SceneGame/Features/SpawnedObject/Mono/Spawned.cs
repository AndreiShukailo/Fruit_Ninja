using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Mono;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.Shadow.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono
{
    public class Spawned : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public SpawnedAnimator Animator;
        public Physical Physical;
        public DestroyWhenInvisible DestroyWhenInvisible;
        public Shadowed Shadowed;

        public void Construct(GamePhysics gamePhysics, CameraWrap cameraWrap, ShadowSystem shadowSystem,
            SpawnedAnimationConfig animationConfig, Vector3 startPosition)
        {
            Physical.Construct(gamePhysics);
            DestroyWhenInvisible.Construct(cameraWrap, _spriteRenderer);
            Shadowed.Construct(shadowSystem, _spriteRenderer, startPosition);
            Animator.StartAnimations(animationConfig);
            
        }
    }
}