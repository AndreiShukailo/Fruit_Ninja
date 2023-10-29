using App.Scripts.Scenes.SceneGame.Features.Common.View;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.View.Score;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Config
{
    public class FruitConfig : MonoBehaviour
    {
        public UnitView LeftPiece;
        public UnitView RightPiece;
        public Color UnitColor;
        public int AddScore;
        public AddScoreView AddScoreView;
    }
}