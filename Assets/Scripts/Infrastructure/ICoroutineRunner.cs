using UnityEngine;
using System.Collections;

public interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
    void StopAllCoroutines();
}