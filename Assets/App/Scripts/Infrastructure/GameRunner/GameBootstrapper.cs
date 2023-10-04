using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;
using UnityEngine;

namespace App.Scripts.Infrastructure.GameRunner
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public ProjectContext ProjectContext;
        public LoadingCurtain CurtainPrefab;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(CurtainPrefab), Instantiate(ProjectContext));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}