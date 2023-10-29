using App.Scripts.Scenes.SceneGame.Features.Common.View.Animations;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private NumberAnimation _currenScore;
        [SerializeField] private NumberAnimation _bestScore;
        private PlayerDataModel _playerDataModel;

        public void Construct(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
            _playerDataModel.OnUpdateScore += OnUpdateScore;
            _playerDataModel.OnUpdateBestScore += OnUpdateBestScore;
            _playerDataModel.UpdateScore();
            _playerDataModel.UpdateBestScore();
        }

        private void OnUpdateScore(int score) =>
            _currenScore.StartAnimation(score);

        private void OnUpdateBestScore(int bestScore) =>
            _bestScore.StartAnimation(bestScore);
    }
}