using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.GameState
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Main");

        private void RegisterServices()
        {
        }
    }
}