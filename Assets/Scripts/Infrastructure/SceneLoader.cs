using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

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

        yield return new WaitForSecondsRealtime(5);



        asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        yield return asyncLoad;

        currentScene = SceneManager.GetSceneByName(nextScene);

        if (currentScene.IsValid())
        {
            Debug.Log(currentScene.IsValid());
            SceneManager.SetActiveScene(currentScene);
        }

        asyncLoad = SceneManager.UnloadSceneAsync(loadingScene.name, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        yield return asyncLoad;


        onLoaded?.Invoke();
    }
}
