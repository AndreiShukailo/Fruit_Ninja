using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Physics.Mono
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DestroyWhenInvisible : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Texture2D _texture;
        private Sprite _sprite;
        private CameraWrap _camera;
        
        private float _offsetX;
        private float _offsetY;

        private bool _visible;
        
        public void Construct(CameraWrap cameraWrap)
        {
            _camera = cameraWrap;
            _sprite = _spriteRenderer.sprite;
            _texture = _sprite.texture;
            InitOffsets();
        }

        private void FixedUpdate()
        {
            UpdateBorders(out var left,out var right,out var up);
            if (_visible == false)
                CheckOnVisible(left, right, up);
            else
                CheckOnInvisible(left, right, up);
        }

        private void InitOffsets()
        {
            if (_texture == null)
                return;

            var localScale = transform.localScale;
            _offsetX = _texture.width / _sprite.pixelsPerUnit / 2f * localScale.x;
            _offsetY = _texture.height / _sprite.pixelsPerUnit / 2f * localScale.y;
        }

        private void UpdateBorders(out float left, out float right, out float up)
        {
            var position = transform.position;
            left = position.x - _offsetX;
            right = position.x + _offsetX;
            up = position.y + _offsetY;
        }

        private void CheckOnVisible(float left, float right, float up)
        {
            if (InCameraVisible(left, right, up))
                _visible = true;
        }

        private void CheckOnInvisible(float left, float right, float up)
        {
            if (OutOfCamera(left, right, up))
                Destroy(gameObject);
        }

        private bool OutOfCamera(float left, float right, float up)
        {
            return left > _camera.RightBoarder || right < _camera.LeftBoarder ||
                   up < _camera.BottomBoarder;
        }

        private bool InCameraVisible(float left, float right, float up)
        {
            return right > _camera.LeftBoarder && left < _camera.RightBoarder &&
                   up > _camera.BottomBoarder;
        }
    }
}