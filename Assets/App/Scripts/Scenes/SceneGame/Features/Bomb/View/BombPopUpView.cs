using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Bomb.View
{
    public class BombPopUpView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        [SerializeField]
        private BombPopupAnimation _animation;
        
        private readonly Vector2[,] _pivotStates = new Vector2[,] {
            {new Vector2(0,0), new Vector2(0.5f,0f), new Vector2(1,0)},
            {new Vector2(0,0.5f), new Vector2(0.5f,0.5f), new Vector2(1, 0.5f)},
            {new Vector2(0,1), new Vector2(0.5f,1f), new Vector2(1,1)}
        };
        
        private void CalculatePivot(Vector2 screenPosition)
        {
            float height = Screen.height;
            float width = Screen.width;
            var y = 0;
            if (screenPosition.x > width * 0.4f)
                y = 1;
            if (screenPosition.x > width * 0.6f)
                y = 2;
            var x = 0;
            if (screenPosition.y > height * 0.4f)
                x = 1;
            if (screenPosition.y > height * 0.6f)
                x = 2;

            _rectTransform.pivot = _pivotStates[x, y];
        }

        public void Construct(Vector3 position, CameraWrap cameraWrap)
        {
            CalculatePivot(cameraWrap.WorldPointToScreen(position));
            _rectTransform.position = position;
            _animation.StartAnimation();
        }
    }
}