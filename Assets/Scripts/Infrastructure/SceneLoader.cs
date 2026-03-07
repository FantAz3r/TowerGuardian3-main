using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;
    private float _minLoadTime = 1;
    private float _maxLoadTime = 3;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, System.Action onLoaded)
    {
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    private IEnumerator LoadScene(string nextScene, System.Action onLoaded)
    {
        LevelID level = LevelID.LoadScene;

        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level.ToString(), LoadSceneMode.Additive);
        yield return asyncLoad;

        Scene loadingScene = SceneManager.GetSceneByName(level.ToString());
        SceneManager.SetActiveScene(loadingScene);

        asyncLoad = SceneManager.UnloadSceneAsync(currentScene.name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        yield return asyncLoad;

        TryShowInterstitialADV(nextScene);

         yield return new WaitForSecondsRealtime(Random.Range(_minLoadTime, _maxLoadTime));

        asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        yield return asyncLoad;

        currentScene = SceneManager.GetSceneByName(nextScene);

        if (currentScene.IsValid())
        {
            SceneManager.SetActiveScene(currentScene);
        }

        asyncLoad = SceneManager.UnloadSceneAsync(loadingScene.name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        yield return asyncLoad;


        onLoaded?.Invoke();
    }

    private void TryShowInterstitialADV(string nextScene)
    {
        if (nextScene != LevelID.MainMenu.ToString() &&
            nextScene != LevelID.None.ToString() &&
            nextScene != LevelID.LoadScene.ToString())
        {
            YG2.InterstitialAdvShow();
        }
    }
}
