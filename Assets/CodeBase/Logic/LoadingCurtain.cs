using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
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
            _sequence = DOTween.Sequence();
            _sequence.Kill();
            Curtain.alpha = 1;
            gameObject.SetActive(true);
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