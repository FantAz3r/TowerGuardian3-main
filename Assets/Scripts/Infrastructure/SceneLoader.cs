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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);

        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        onLoaded?.Invoke();
    }
}
