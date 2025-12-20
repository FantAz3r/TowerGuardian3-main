using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private UIDummy _settings;
    [field: SerializeField] public SwichDamageNumbers SwichDamageNumbers { get; private set;}
    [field: SerializeField] public StartButton StartButton { get; private set;}

    private void Awake()
    {
        _settings.gameObject.SetActive(false);
    }
}
