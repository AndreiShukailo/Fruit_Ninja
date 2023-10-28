using App.Scripts.Scenes.SceneGame.Features.Common.View.Animations;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.PhysicsSystem.View
{
    public class FrostEffectView : MonoBehaviour
    {
        [SerializeField] private GameObject _popup;

        [SerializeField] private ShowPopUpAnimation _showAnimation;
        [SerializeField] private HidePopUpAnimation _hideAnimation;

        public void Activate()
        {
            _hideAnimation.StopAnimation();
            _popup.SetActive(true);
            _showAnimation.StartAnimation();
        }

        public void Deactivate() =>
            _hideAnimation.StartAnimation();
    }
}