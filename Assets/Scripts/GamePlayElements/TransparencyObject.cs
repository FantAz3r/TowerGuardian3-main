
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransparencyObject : MonoBehaviour
{
    private List<MeshRenderer> _meshRenderers = new();
    private Coroutine _disableCoroutine;
    private readonly WaitForSeconds _wait = new WaitForSeconds(0.5f);

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();

        if (_meshRenderers.Count == 0)
        {
            Debug.LogWarning("TransparencyObject требует MeshRenderer на объекте");
        }
    }

    public void MakeInvisible()
    {
        if (_disableCoroutine != null)
            StopCoroutine(_disableCoroutine);

        _disableCoroutine = StartCoroutine(DisableRoutine());
    }

    private IEnumerator DisableRoutine()
    {
        foreach (var item in _meshRenderers)
        {
            item.enabled = false;

        }

        yield return _wait;

        foreach (var item in _meshRenderers)
        {
            item.enabled = true;
        }

        _disableCoroutine = null;
    }
}
