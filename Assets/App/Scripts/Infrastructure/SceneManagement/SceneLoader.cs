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
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name != "Initial")
            {
                _loadingCurtain.Show();
                yield return new WaitForSeconds(_loadingCurtain.Duration);
            }

            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            _loadingCurtain.Hide();
            onLoaded?.Invoke();
        }
    }
}