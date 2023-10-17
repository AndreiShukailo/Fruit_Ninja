using App.Scripts.Scenes.SceneGame.Features.Common.Components;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Config;
using App.Scripts.Scenes.SceneGame.Features.Fruits.Services;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe;
using App.Scripts.Scenes.SceneGame.Features.InputSystem.Swipe.Services;
using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Fruits.Strategies
{
    public class FruitSwipeStrategy : ISwipeStrategy
    {
        private readonly FruitSwipeEffectsCreator _swipeEffectsCreator;
        private readonly IDestroyStrategy _destroyStrategy;
        private readonly PlayerDataModel _playerDataModel;
        private readonly ComboSystem.Services.ComboSystem _comboSystem;

        public FruitSwipeStrategy(FruitSwipeEffectsCreator swipeEffectsCreator, IDestroyStrategy destroyStrategy,
            PlayerDataModel playerDataModel, ComboSystem.Services.ComboSystem comboSystem)
        {
            _swipeEffectsCreator = swipeEffectsCreator;
            _destroyStrategy = destroyStrategy;
            _playerDataModel = playerDataModel;
            _comboSystem = comboSystem;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            var fruitConfig = componentContainer.UnitView.GetComponentInChildren<FruitConfig>();
            _comboSystem.GetCombo();
            fruitConfig.AddScore *= _comboSystem.ComboCount;
            _swipeEffectsCreator.CreateEffect(cut, componentContainer, fruitConfig);
            _playerDataModel.AddScore(fruitConfig.AddScore * _comboSystem.ComboCount);
            _destroyStrategy.OnDestroy(componentContainer);
        }
    }
}