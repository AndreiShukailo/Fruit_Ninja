using App.Scripts.Scenes.SceneGame.Features.Player;
using App.Scripts.Scenes.SceneGame.Features.View;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.FruitMono
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