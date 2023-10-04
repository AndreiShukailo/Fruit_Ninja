using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Infrastructure.SceneManagement
{
    public class LoadingCurtain : MonoBehaviour
    {
        public float Duration = 0.5f;
        public CanvasGroup Curtain;
        private Sequence _sequence;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        public void Show()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(Curtain.DOFade(1f, Duration))
                .OnComplete(() => gameObject.SetActive(true));
        }


        public void Hide()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(Curtain.DOFade(0f, Duration))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}