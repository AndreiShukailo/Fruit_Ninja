using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.Installer;

namespace App.Scripts.Infrastructure.GameState
{
    public class BootstrapState : IPayLoadedState<string>
    {
        private string _currentSceneName;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStateMachine _stateMachine;
        private readonly ProjectContext _projectContext;

        public BootstrapState(GameStateMachine stateMachine, ICoroutineRunner coroutineRunner,
            ProjectContext projectContext)
        {
            _stateMachine = stateMachine;
            _coroutineRunner = coroutineRunner;
            _projectContext = projectContext;
        }

        public void Enter(string currentSceneName)
        {
            _currentSceneName = currentSceneName;
            RegisterProjectContext();
            EnterLoadLevel();
        }
        

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(_currentSceneName);

        private void RegisterProjectContext() =>
            _projectContext.Setup(_stateMachine, _coroutineRunner);
    }
}