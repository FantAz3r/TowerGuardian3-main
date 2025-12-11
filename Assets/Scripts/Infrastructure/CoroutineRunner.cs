using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    public new Coroutine StartCoroutine(IEnumerator routine)
    {
        return base.StartCoroutine(routine);
    }

    public new void StopCoroutine(IEnumerator routine)
    {
        base.StopCoroutine(routine);
    }

    public new void StopCoroutine(Coroutine routine)
    {
        base.StopCoroutine(routine);
    }

    public new void StopAllCoroutines()
    {
        base.StopAllCoroutines();
    }
}
