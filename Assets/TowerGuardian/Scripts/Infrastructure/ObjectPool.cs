using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerGuardian.Scripts.Infrastructure
{
    public class ObjectPool<T>
        where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly bool _autoExpand = true;
        private List<T> _objects;

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
            T createdObject = Object.Instantiate(_prefab, _container);
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

                if (!item.gameObject.activeInHierarchy)
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

        public int GetActiveObjectsCount()
        {
            int count = 0;

            foreach (var obj in _objects)
            {
                if (obj && obj.gameObject.activeInHierarchy)
                {
                    count++;
                }
            }

            return count;
        }

        public void Clear()
        {
            foreach (var mono in _objects)
            {
                if (mono != null)
                {
                    if (mono.gameObject.activeInHierarchy)
                    {
                        mono.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void DestroyPool()
        {
            _objects.Clear();
        }
    }
}