using App.Scripts.Scenes.SceneGame.Features.CheckOnInvizibleSystem;
using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Magnet.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Magnet
{
    public class MagnetSwipeStrategy : ISwipeStrategy
    {
        private readonly MagnetSystem _magnetSystem;

        public MagnetSwipeStrategy(MagnetSystem magnetSystem)
        {
            _magnetSystem = magnetSystem;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            if(!componentContainer.GetComponent<CheckOnInvisibleComponent>().IsVisible) return;
            _magnetSystem.ApplyMagnet(componentContainer.UnitView.transform.position);
            componentContainer.Destroy();
        }
    }
}