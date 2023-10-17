using App.Scripts.Scenes.Common.Buttons;
using App.Scripts.Scenes.SceneGame.Features.GameState.View;

namespace App.Scripts.Scenes.SceneGame.Features.GameState.Services
{
    public class GameStateManager
    {
        private readonly GameState _gameState;
        private readonly RestartPopupView _restartPopupView;
        private readonly PauseButtonView _pauseButtonView;
        private readonly PausePopupView _pausePopupView;
        private readonly ButtonsContainerView _buttonsContainerView;

        public GameStateManager(GameState gameState, RestartPopupView restartPopupView,
            PausePopupView pausePopupView, ButtonsContainerView buttonsContainerView)
        {
            _gameState = gameState;
            _restartPopupView = restartPopupView;
            _pausePopupView = pausePopupView;
            _buttonsContainerView = buttonsContainerView;
        }

        public void InitView()
        {
            SubscribePauseButton();
            _gameState.OnLoose += OnLoose;
        }

        private void OnLoose(int current, int best)
        {
            _restartPopupView.Activate(current, best);
            SubscribeRestartButton();
        }

        private void SubscribePauseButton()
        {
            var pauseButton = GetPauseButton();
            pauseButton.Subscribe(OnPauseButtonClick);
        }

        private void SubscribeContinueButton()
        {
            var continueButton = GetContinueButton();
            continueButton.Subscribe(OnContinueButtonClick);
        }
        
        private void SubscribeRestartButton()
        {
            var restartButton = GetRestartButton();
            restartButton.Subscribe(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            GetRestartButton().Unsubscribe();
            _restartPopupView.Deactivate();
            _gameState.StartPlay();
        }

        private void OnPauseButtonClick()
        {   
            GetPauseButton().Unsubscribe();
            _gameState.SetPause();
            ShowPausePopup();
        }

        private void OnContinueButtonClick()
        {
            GetContinueButton().Unsubscribe();
            SubscribePauseButton();
            HidePausePopup();
            _gameState.Continue();
        }

        private ContinueButtonView GetContinueButton() =>
            _buttonsContainerView.Get<ContinueButtonView>();

        private PauseButtonView GetPauseButton() =>
            _buttonsContainerView.Get<PauseButtonView>();
        
        private RestartButtonView GetRestartButton() =>
            _buttonsContainerView.Get<RestartButtonView>();

        private void ShowPausePopup() =>
            _pausePopupView.Activate(SubscribeContinueButton);

        private void HidePausePopup() =>
            _pausePopupView.Deactivate();
    }
}