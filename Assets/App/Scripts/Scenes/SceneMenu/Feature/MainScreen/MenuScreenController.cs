using App.Scripts.Infrastructure.GameState;
using App.Scripts.Scenes.Common;
using App.Scripts.Scenes.Common.Buttons;
using App.Scripts.Scenes.Common.Services.SaveLoad;
using App.Scripts.Scenes.SceneMenu.Feature.BestScore;
using App.Scripts.Scenes.SceneMenu.Feature.Buttons;
using UnityEngine;

namespace App.Scripts.Scenes.SceneMenu.Feature.MainScreen
{
    public class MenuScreenController
    {
        private const string GameSceneName = "Game";
        private readonly ButtonsContainerView _buttonsContainerView;
        private readonly BestScoreView _bestScoreView;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public MenuScreenController(ButtonsContainerView buttonsContainerView, BestScoreView bestScoreView,
            GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _buttonsContainerView = buttonsContainerView;
            _bestScoreView = bestScoreView;
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void IniView()
        {
            SubscribeOnButtons();
            UpdateBestScoreView();
        }

        private void SubscribeOnButtons()
        {
            var startGameButton = _buttonsContainerView.Get<StartGameButtonView>();
            startGameButton.Subscribe(OnStartGameButtonClick);
            var exitGameButtonView = _buttonsContainerView.Get<ExitGameButtonView>();
            exitGameButtonView.Subscribe(OnExitGameButtonClick);
        }

        private void UnsubscribeButtons()
        {
            var startGameButton = _buttonsContainerView.Get<StartGameButtonView>();
            startGameButton.Unsubscribe();
            var exitGameButtonView = _buttonsContainerView.Get<ExitGameButtonView>();
            exitGameButtonView.Unsubscribe();
        }

        private void UpdateBestScoreView()
        {
            var playerProgress = _saveLoadService.Load() ?? new PlayerProgress();
            var bestScore = playerProgress.BestScore;
            _bestScoreView.Construct(bestScore);
        }

        private void OnStartGameButtonClick()
        {
            UnsubscribeButtons();
            LoadGameScene();
        }


        private void LoadGameScene() =>
            _gameStateMachine.Enter<LoadLevelState, string>(GameSceneName);

        private void OnExitGameButtonClick()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying == true)
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}