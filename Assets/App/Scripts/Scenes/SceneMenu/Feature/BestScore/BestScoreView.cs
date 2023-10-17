using App.Scripts.Scenes.SceneGame.Features.Player;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneMenu.Feature.BestScore
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bestScoreCount;

        public void Construct(int bestScore) =>
            _bestScoreCount.text = bestScore.ToString();
    }
}