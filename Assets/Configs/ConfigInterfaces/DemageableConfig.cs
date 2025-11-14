using UnityEngine;

public abstract class DemageableConfig : ScriptableObject, IDemageableConfig
{
    [SerializeField] private float _maxHealth = 1f;

    public float MaxHealth => _maxHealth;
}