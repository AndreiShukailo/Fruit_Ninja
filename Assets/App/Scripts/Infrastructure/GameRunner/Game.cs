using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;

namespace App.Scripts.Infrastructure.GameRunner
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, ProjectContext projectContext)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner, loadingCurtain), projectContext);
        }
    }
}