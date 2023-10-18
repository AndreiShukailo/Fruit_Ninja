using System;
using System.Collections;
using App.Scripts.Infrastructure.GameRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Infrastructure.SceneManagement
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _loadingCurtain;

        public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            _coroutineRunner = coroutineRunner;
            _loadingCurtain = loadingCurtain;
        }

        public void Load(string name, Action onLoaded = null) =>
            StartLoadScene(name, onLoaded);

        private void StartLoadScene(string nextScene, Action onLoaded = null)
        {
            _loadingCurtain.Show(() => _coroutineRunner.StartCoroutine(LoadScene(nextScene, onLoaded)));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
            
            Time.timeScale = 1f;
            _loadingCurtain.Hide();
            onLoaded?.Invoke();
        }
    }
}