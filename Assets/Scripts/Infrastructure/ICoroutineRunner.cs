using System.Collections;
using UnityEngine;

public interface ICoroutineRunner : IService
{
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
    void StopAllCoroutines();
}