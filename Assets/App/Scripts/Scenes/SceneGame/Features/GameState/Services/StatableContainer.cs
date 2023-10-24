using System.Collections.Generic;
using App.Scripts.Scenes.SceneGame.Features.SpawnerSystem;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.Services
{
    public class StatableContainer
    {
        private List<IStatable> _statables = new();

        public void Add<TService>(TService service) where TService : IStatable =>
            _statables.Add(service);

        public void SetState(GameState gameState)
        {
            foreach (var statable in _statables)
                statable.SetGameState(gameState);
        }
    }
}