using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Ice
{
    public class IceSwipeStrategy : ISwipeStrategy
    {
        private readonly IceConfig _iceConfig;
        private readonly RetardSystem _retardSystem;

        public IceSwipeStrategy(IceConfig iceConfig, RetardSystem retardSystem)
        {
            _iceConfig = iceConfig;
            _retardSystem = retardSystem;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            _retardSystem.ApplyRetard(_iceConfig.ReatardTime);
            componentContainer.Destroy();
        }
    }
}