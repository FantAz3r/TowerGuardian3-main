using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return base.StartCoroutine(routine);
    }

    public void StopCoroutine(IEnumerator routine)
    {
        base.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
        base.StopCoroutine(routine);
    }

    public void StopAllCoroutines()
    {
        base.StopAllCoroutines();
    }
}
