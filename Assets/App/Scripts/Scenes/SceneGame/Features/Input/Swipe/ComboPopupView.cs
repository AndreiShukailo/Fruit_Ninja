using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Input.Swipe
{
    public class ComboPopupView : MonoBehaviour
    {
        [SerializeField] private GameObject _compoPopup;
        [SerializeField] private TMP_Text _compoCount;
        [SerializeField] private TMP_Text _fruitCount;
        [SerializeField] private AnimationTweenBase _animation;
        
        public void Show(int comboCount)
        {
            _compoPopup.gameObject.SetActive(true);
            _compoCount.text = comboCount.ToString();
            _fruitCount.text = comboCount.ToString();
            _animation.StartAnimation();
        }
    }
}