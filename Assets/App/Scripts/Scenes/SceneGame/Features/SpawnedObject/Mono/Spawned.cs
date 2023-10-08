using App.Scripts.Scenes.SceneGame.Features.Animation;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Mono;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Scenes.SceneGame.Features.SpawnedObject.Mono
{
    public class Spawned : MonoBehaviour
    {
        public SpawnedAnimator Animator;
        public Physical Physical;
        public DestroyWhenInvisible DestroyWhenInvisible;
        [FormerlySerializedAs("ShadowEffect")] public Shadowed Shadowed;

        public void Construct(GamePhysics gamePhysics, CameraWrap cameraWrap, ShadowSystem shadowSystem,
            SpawnedAnimationConfig animationConfig, Vector3 startPosition)
        {
            Physical.Construct(gamePhysics);
            DestroyWhenInvisible.Construct(cameraWrap);
            Shadowed.Construct(shadowSystem, startPosition);
            Animator.InitConfig(animationConfig);
            Animator.StartAnimations();
        }
    }
}