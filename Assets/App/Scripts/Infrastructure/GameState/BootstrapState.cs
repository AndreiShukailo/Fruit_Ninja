using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;

namespace App.Scripts.Infrastructure.GameState
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly ProjectContext _projectContext;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ProjectContext projectContext)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _projectContext = projectContext;
        }

        public void Enter()
        {
            RegisterProjectContext();
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Game");

        private void RegisterProjectContext() =>
            _projectContext.Setup(_stateMachine);
    }
}