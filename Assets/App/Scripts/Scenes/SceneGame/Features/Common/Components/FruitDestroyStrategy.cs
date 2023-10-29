using App.Scripts.Scenes.SceneGame.Features.PlayerData.Model;

namespace App.Scripts.Scenes.SceneGame.Features.Common.Components
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