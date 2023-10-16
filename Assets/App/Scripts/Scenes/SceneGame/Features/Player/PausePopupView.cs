using System;
using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class PausePopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;
        [SerializeField] private Button _continueButton;

        [SerializeField] private PausePopUpAnimation _showAnimation;
        [SerializeField] private HidePopUpAnimation _hideAnimation;

        public void Activate(Action onContinueButton)
        {
            _continueButton.onClick.AddListener(() => onContinueButton?.Invoke());
            _continueButton.interactable = false;
            _popup.SetActive(true);
            _showAnimation.StartAnimation(OnAnimationEnd);
        }

        public void Deactivate()
        {
            _continueButton.onClick.RemoveAllListeners();
            _hideAnimation.StartAnimation();
        }

        private void OnAnimationEnd()
        {
            _continueButton.interactable = true;
        }
    }
}