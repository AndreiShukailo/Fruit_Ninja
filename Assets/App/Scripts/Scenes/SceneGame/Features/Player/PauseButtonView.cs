using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class PauseButtonView : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;

        public void Construct(GameStateManager gameStateManager) =>
            _pauseButton.onClick.AddListener(gameStateManager.OnPause);
    }
}