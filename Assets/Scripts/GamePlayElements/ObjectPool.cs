using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;
    private Transform _container;
    private bool _autoExpand = true;

    public ObjectPool(T prefab, int count, bool autoExpand)
    {
        _prefab = prefab;
        _container = null;
        _autoExpand = autoExpand;

        CreatePool(count);
    }

    public ObjectPool(T prefab, int count, bool autoExpand, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _autoExpand = autoExpand;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _objects = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        T createdObject = UnityEngine.Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _objects.Add(createdObject);

        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in _objects)
        {
            if (item == null)
                continue;

            if (item.gameObject.activeInHierarchy == false)
            {
                element = item;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T Get()
    {
        if (HasFreeElement(out T element))
        {
            element.gameObject.SetActive(true);
            return element;
        }

        if (_autoExpand)
            return CreateObject(true);

        throw new Exception($"No free elenent of type {typeof(T)}");
    }

    public void Clear()
    {
        foreach (var mono in _objects)
        {
            if (mono.gameObject.activeInHierarchy)
            {
                mono.gameObject.SetActive(false);
            }
        }
    }

    public void DestroyPool()
    {
        _objects.Clear();

    }
}