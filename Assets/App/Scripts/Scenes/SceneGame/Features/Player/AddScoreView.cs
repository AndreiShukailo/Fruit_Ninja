using App.Scripts.Scenes.SceneGame.Features.Animation.SpawnedAnimation.Animation;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.SceneGame.Features.Player
{
    public class AddScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreCount;
        [SerializeField] private AnimationTweenBase _addScoreAnimation;
        
        public void Construct(Vector3 startPosition, int score)
        {
            transform.position = startPosition;
            _addScoreAnimation.StartAnimation();
            _scoreCount.text = score.ToString();
        }
    }
}