using System;
using App.Scripts.Scenes.SceneGame.Features.Common.View.Animations;
using App.Scripts.Scenes.SceneGame.Features.GameState.View.Animations;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.View
{
    public class PausePopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;

        [SerializeField] private PausePopUpAnimation _showAnimation;
        [SerializeField] private HidePopUpAnimation _hideAnimation;

        public void Activate(Action onAnimationEnd)
        {
            _popup.SetActive(true);
            _showAnimation.StartAnimation(onAnimationEnd);
        }

        public void Deactivate() =>
            _hideAnimation.StartAnimation();
    }
}