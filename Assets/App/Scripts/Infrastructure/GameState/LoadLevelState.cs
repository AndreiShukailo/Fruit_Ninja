using App.Scripts.Infrastructure.Installer;
using App.Scripts.Infrastructure.SceneManagement;
using App.Scripts.Infrastructure.ServiceLocator;
using UnityEngine;

namespace App.Scripts.Infrastructure.GameState
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _projectContainer;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceContainer projectContainer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _projectContainer = projectContainer;
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
            sceneContext.Setup(_projectContainer);
        }
    }
}