using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Input.Swipe;
using App.Scripts.Scenes.SceneGame.Features.Player;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class FruitSwipeStrategy : ISwipeStrategy
    {
        private readonly FruitSwipeEffectsCreator _swipeEffectsCreator;
        private readonly IDestroyStrategy _destroyStrategy;
        private readonly PlayerDataModel _playerDataModel;
        private readonly ComboSystem _comboSystem;

        public FruitSwipeStrategy(FruitSwipeEffectsCreator swipeEffectsCreator, IDestroyStrategy destroyStrategy,
            PlayerDataModel playerDataModel, ComboSystem comboSystem)
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