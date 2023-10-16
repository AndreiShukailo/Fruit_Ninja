namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class GameStateManager
    {
        private readonly GameState _gameState;
        private readonly RestartPopupView _restartPopupView;
        private readonly PauseButtonView _pauseButtonView;
        private readonly PausePopupView _pausePopupView;

        public GameStateManager(GameState gameState, RestartPopupView restartPopupView, PauseButtonView pauseButtonView,
            PausePopupView pausePopupView)
        {
            _gameState = gameState;
            _restartPopupView = restartPopupView;
            _pauseButtonView = pauseButtonView;
            _pausePopupView = pausePopupView;
        }

        public void OnPause()
        {
            _gameState.SetPause();
            ShowPausePopup();
        }

        private void ShowPausePopup() =>
            _pausePopupView.Activate(OnContinue);

        private void HidePausePopup() =>
            _pausePopupView.Deactivate();

        private void OnContinue()
        {
            _gameState.Continue();
            HidePausePopup();
        }
    }
}