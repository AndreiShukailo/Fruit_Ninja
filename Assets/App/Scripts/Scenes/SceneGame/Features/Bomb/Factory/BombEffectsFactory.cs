using App.Scripts.Scenes.SceneGame.Features.Bomb.View;
using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb
{
    public class BombEffectsFactory
    {
        private readonly RectTransform _container;
        private readonly CameraWrap _cameraWrap;

        public BombEffectsFactory(RectTransform container, CameraWrap cameraWrap)
        {
            _container = container;
            _cameraWrap = cameraWrap;
        }

        public void Create(BombPopUpView view, GameObject explosionEffect, Vector3 position)
        {
            var explosion = Object.Instantiate(explosionEffect);
            explosion.transform.position = new Vector3(position.x, position.y, -1f);
            
            var popup = Object.Instantiate(view, _container);
            popup.Construct(position, _cameraWrap);
        }
    }
}