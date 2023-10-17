using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Common.Buttons
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