using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransparencyObject : MonoBehaviour
{
    [SerializeField] private Material _transparentMaterial;

    private List<MeshRenderer> _meshRenderers = new();
    private Coroutine _changeMaterialCoroutine;
    private readonly WaitForSeconds _wait = new WaitForSeconds(0.5f);
    private List<Material[]> _originalMaterials;
    private ICoroutineRunner _coroutineRunner;

    private void Awake()
    {
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>().ToList();

        if (_meshRenderers.Count == 0)
        {
            Debug.LogWarning("TransparencyObject требует MeshRenderer на объекте");
        }

        _originalMaterials = new List<Material[]>();
        
        foreach (var mr in _meshRenderers)
        {
            _originalMaterials.Add(mr.materials);
        }
    }

    public void MakeInvisible()
    {
        if (_changeMaterialCoroutine != null)
            _coroutineRunner.StopCoroutine(_changeMaterialCoroutine);

        _changeMaterialCoroutine = _coroutineRunner.StartCoroutine(ChangeMaterialRoutine());
    }

    private IEnumerator ChangeMaterialRoutine()
    {
        for (int i = 0; i < _meshRenderers.Count; i++)
        {
            var mr = _meshRenderers[i];

            var newMaterials = new Material[mr.materials.Length];

            for (int j = 0; j < newMaterials.Length; j++)
                newMaterials[j] = _transparentMaterial;

            mr.materials = newMaterials;
        }

        yield return _wait;

        for (int i = 0; i < _meshRenderers.Count; i++)
        {
            if (_meshRenderers[i] != null)
            {
                _meshRenderers[i].materials = _originalMaterials[i];
            }
        }

        _changeMaterialCoroutine = null;
    }
}
