using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlexibleBackGround : MonoBehaviour
    {
        private CameraWrap _cameraWrap;
        private Texture2D _texture;
        private Sprite _sprite;

        [Inject]
        private void Construct(CameraWrap cameraWrap)
        {
            _cameraWrap = cameraWrap;
            _cameraWrap.OnAspectChanged += SetBackGround;
            
            _sprite = GetComponent<SpriteRenderer>().sprite;
            _texture = _sprite.texture;
            
            SetBackGround();
        }

        private void OnDestroy() =>
            _cameraWrap.OnAspectChanged -= SetBackGround;

        private void SetBackGround()
        {
            if(_texture == null)
                return;
            
            var ratio = _sprite.pixelsPerUnit / _cameraWrap.PixelsPerUnit;
            var scaleY = _cameraWrap.PixelsWidth / _texture.width * ratio;
            var scaleX = _cameraWrap.PixelsHeight / _texture.height * ratio;
            var newScale = new Vector3(scaleY, scaleX, 1f);
            transform.localScale = newScale;
        }
    }
}