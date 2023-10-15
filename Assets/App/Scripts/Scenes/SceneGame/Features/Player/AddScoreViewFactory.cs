using App.Scripts.Scenes.SceneGame.Features.CameraController;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class AddScoreViewFactory
    {
        private readonly CameraWrap _cameraWrap;
        private readonly PlayerDataView _playerDataView;

        public AddScoreViewFactory(CameraWrap cameraWrap, PlayerDataView playerDataView)
        {
            _cameraWrap = cameraWrap;
            _playerDataView = playerDataView;
        }

        public void Create(AddScoreView addScoreView, Vector3 startPosition, int score)
        {
            var view = Object.Instantiate(addScoreView, _playerDataView.transform);
            view.Construct(startPosition, score);
        }
    }
}