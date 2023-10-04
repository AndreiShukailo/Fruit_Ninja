using App.Scripts.Scenes.SceneGame.Features.Camera;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.BackGround
{
    public class FlexibleBackGround 
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly CameraWrap _cameraWrap;
        private readonly Texture2D _texture;
        private readonly Sprite _sprite;
        
        public FlexibleBackGround(CameraWrap cameraWrap, SpriteRenderer spriteRenderer)
        {
            _cameraWrap = cameraWrap;
            _spriteRenderer = spriteRenderer;
            _cameraWrap.OnAspectChanged += SetBackGround;

            _sprite = spriteRenderer.sprite;
            _texture = _sprite.texture;
            
            SetBackGround();
        }

        private void SetBackGround()
        {
            if(_texture == null)
                return;
            
            var ratio = _sprite.pixelsPerUnit / _cameraWrap.PixelsPerUnit;
            var scaleY = _cameraWrap.PixelsWidth / _texture.width * ratio;
            var scaleX = _cameraWrap.PixelsHeight / _texture.height * ratio;
            var newScale = new Vector3(scaleY, scaleX, 1f);
            _spriteRenderer.transform.localScale = newScale;
        }
    }
}