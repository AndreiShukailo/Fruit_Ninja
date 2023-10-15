using App.Scripts.Scenes.SceneGame.Features.Player;

namespace App.Scripts.Scenes.SceneGame.Features
{
    public class FruitDestroyStrategy : IDestroyStrategy
    {
        private readonly PlayerDataModel _playerDataModel;

        public FruitDestroyStrategy(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
        }

        public void OnDestroy(ComponentContainer componentContainer)
        {
            _playerDataModel.RemoveHealth();
            componentContainer.Destroy();
        }
    }
}