using App.Scripts.Scenes.SceneGame.Features.FruitMono;
using App.Scripts.Scenes.SceneGame.Features.Player;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class FruitSwipeStrategy : ISwipeStrategy
    {
        private readonly FruitSwipeEffectsCreator _swipeEffectsCreator;
        private readonly IDestroyStrategy _destroyStrategy;
        private readonly PlayerDataModel _playerDataModel;

        public FruitSwipeStrategy(FruitSwipeEffectsCreator swipeEffectsCreator, IDestroyStrategy destroyStrategy,
            PlayerDataModel playerDataModel)
        {
            _swipeEffectsCreator = swipeEffectsCreator;
            _destroyStrategy = destroyStrategy;
            _playerDataModel = playerDataModel;
        }

        public void Swipe(Vector2 cut, ComponentContainer componentContainer)
        {
            var fruitConfig = componentContainer.UnitView.GetComponentInChildren<FruitConfig>();
            _swipeEffectsCreator.CreateEffect(cut, componentContainer, fruitConfig);
            _playerDataModel.AddScore(fruitConfig.AddScore);
            _destroyStrategy.OnDestroy(componentContainer);
        }
    }
}