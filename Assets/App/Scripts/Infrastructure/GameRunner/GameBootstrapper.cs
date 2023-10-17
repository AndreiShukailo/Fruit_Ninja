using App.Scripts.Infrastructure.GameState;
using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Infrastructure.GameRunner
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public ProjectContext ProjectContext;
        public LoadingCurtain CurtainPrefab;

        private void Awake()
        {
            var projectContext = Instantiate(ProjectContext);
            var curtain = Instantiate(CurtainPrefab);
            GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(this, curtain), this, projectContext);
            stateMachine.Enter<BootstrapState, string>(GetCurrentSceneName());

            DontDestroyOnLoad(this);
        }

        private string GetCurrentSceneName() =>
            SceneManager.GetActiveScene().name;
    }
}