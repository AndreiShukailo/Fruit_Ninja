using App.Scripts.Scenes.SceneGame.Features.CameraController;
using App.Scripts.Scenes.SceneGame.Features.Physics;
using App.Scripts.Scenes.SceneGame.Features.Physics.Mono;
using App.Scripts.Scenes.SceneGame.Features.Shadow;
using App.Scripts.Scenes.SceneGame.Features.Shadow.Mono;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruit.Pieces
{
    public class Piece : MonoBehaviour
    {
        public Physical Physical;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private DestroyWhenInvisible _destroyWhenInvisible;
        [SerializeField] private Shadowed _shadowed;

        public void Construct(GamePhysics physics, CameraWrap cameraWrap,
            ShadowSystem shadowSystem, Vector3 startPosition)
        {
            Physical.Construct(physics);
            _destroyWhenInvisible.Construct(cameraWrap, _spriteRenderer);
            _shadowed.Construct(shadowSystem, _spriteRenderer, startPosition);
        }
    }
}