using App.Scripts.Scenes.SceneGame.Features.Common.View.Animations;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.View
{
    public class RestartPopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;
        [SerializeField] private NumberAnimation _currentScore;
        [SerializeField] private NumberAnimation _bestScore;

        [SerializeField] private ShowPopUpAnimation _showAnimarion;
        [SerializeField] private HidePopUpAnimation _hideAnimarion;

        public void Activate(int currentScore, int bestScore)
        {
            _popup.SetActive(true);
            _showAnimarion.StartAnimation();
            _currentScore.StartAnimation(currentScore);
            _bestScore.StartAnimation(bestScore);
        }

        public void Deactivate()
        {
            _hideAnimarion.StartAnimation();
        }
    }
}