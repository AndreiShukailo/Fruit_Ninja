using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.BackGround
{
    [RequireComponent(typeof(RequireComponent))]
    [ExecuteInEditMode]
    public class FlexibleBackGroundEditor : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;
        private Sprite _sprite;
        private Texture2D _texture;

        private float _prevAspect;

        private bool Init()
        {
            _camera = Camera.main;


            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _sprite = _spriteRenderer.sprite;
            if (_sprite == null) return false;
            
            _texture = _sprite.texture;

            return true;
        }

        private void Update()
        {
            if (Application.isPlaying) return;
            if (!Init()) return;
            if (_prevAspect == _camera.aspect) return;

            var scale = CalculateScale();

            var newScale = new Vector3(scale, scale, 1f);
            _spriteRenderer.transform.localScale = newScale;
            _prevAspect = _camera.aspect;
        }

        private float CalculateScale()
        {
            var ratio = _sprite.pixelsPerUnit / (_camera.pixelHeight / _camera.orthographicSize / 2);
            float scale = (float)_camera.pixelHeight / _texture.height * ratio;
            if (_camera.aspect > (float)_texture.width / _texture.height)
                scale = (float)_camera.pixelWidth / _texture.width * ratio;
            return scale;
        }
    }
}