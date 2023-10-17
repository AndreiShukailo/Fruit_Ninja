using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.SceneMenu.Feature.Buttons
{
    public class ButtonViewBase : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Subscribe(Action onClick) =>
            _button.onClick.AddListener(() => onClick?.Invoke());

        public void Unsubscribe() =>
            _button.onClick.RemoveAllListeners();
    }
}