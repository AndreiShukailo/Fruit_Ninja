using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;
using UnityEngine;

namespace App.Scripts.Infrastructure.GameState
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ProjectContext _projectContext;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ProjectContext projectContext)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _projectContext = projectContext;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _stateMachine.Enter<GameLoopState>();
            RegisteredSceneContext();
        }

        private void RegisteredSceneContext()
        {
            var sceneContext = Object.FindObjectOfType<SceneContext>();
            if (sceneContext == null) return;
            sceneContext.Setup(_projectContext);
        }
    }
}