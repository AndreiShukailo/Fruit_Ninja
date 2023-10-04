using App.Scripts.Infrastructure.GameState;

namespace App.Scripts.Infrastructure.Installer
{
    public class ProjectContext : MonoContext
    {
        public void Setup(GameStateMachine gameStateMachine)
        {
            var container = BuildContainer();
            Tickables.AddRange(container.GetServices<ITickable>());
            container.SetServiceSelf(gameStateMachine);
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}