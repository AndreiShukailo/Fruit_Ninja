using App.Scripts.Infrastructure.GameRunner;
using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.ServiceLocator;

namespace App.Scripts.Infrastructure.Installer
{
    public class ProjectContext : MonoContext
    {
        public ServiceContainer ServiceContainer { get; } = new();
        public void Setup(GameStateMachine gameStateMachine, ICoroutineRunner coroutineRunner)
        {
            BuildContainer();
            Tickables.AddRange(ServiceContainer.GetServices<ITickable>());
            ServiceContainer.SetServiceSelf(gameStateMachine);
            ServiceContainer.SetServiceSelf(coroutineRunner);
        }
        
        private void BuildContainer()
        {
            foreach (var installer in Installers) 
                installer.InstallBindings(ServiceContainer);
        }
        
        private void Awake() =>
            DontDestroyOnLoad(this);
    }
}