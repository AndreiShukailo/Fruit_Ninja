using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Config;
using App.Scripts.Scenes.SceneGame.Features.Items.Samurai.Services;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Items.Samurai
{
    public class SamuraiSwipeStrategy : ISwipeStrategy
    {
        private readonly SamuraiEffectCreator _samuraiEffectCreator;
        private readonly SamuraiMod _samuraiMod;

        public SamuraiSwipeStrategy(SamuraiEffectCreator samuraiEffectCreator, SamuraiMod samuraiMod)
        {
            _samuraiEffectCreator = samuraiEffectCreator;
            _samuraiMod = samuraiMod;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            _samuraiEffectCreator.Create(cut, componentContainer);
            _samuraiMod.StartMode();
            componentContainer.Destroy();
        }
    }
}