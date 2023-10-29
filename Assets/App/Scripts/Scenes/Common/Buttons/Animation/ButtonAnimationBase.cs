using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Scenes.Common.Buttons.Animation
{
    public abstract class ButtonAnimationBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        protected abstract void OnButtonDown();
        protected abstract void OnButtonUp();

        public void OnPointerDown(PointerEventData eventData) =>
            OnButtonDown();

        public void OnPointerUp(PointerEventData eventData) =>
            OnButtonUp();
    }
}