using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Common.Buttons
{
    [Serializable]
    public class ButtonsContainerView
    {
        [SerializeField] private List<ButtonViewBase> _buttons;

        public TButtonView Get<TButtonView>() where TButtonView : ButtonViewBase
        {
            var findComponent = _buttons.Find(p => p is TButtonView);
            return (TButtonView)findComponent;
        }
    }
}