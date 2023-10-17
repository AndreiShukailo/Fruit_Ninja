using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score.Factory
{
    public class AddScoreViewFactory
    {
        private readonly ScoreView _playerDataView;

        public AddScoreViewFactory(ScoreView playerDataView)
        {
            _playerDataView = playerDataView;
        }

        public void Create(AddScoreView addScoreView, Vector3 startPosition, int score)
        {
            var view = Object.Instantiate(addScoreView, _playerDataView.transform);
            view.Construct(startPosition, score);
        }
    }
}