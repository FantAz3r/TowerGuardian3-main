using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private float _baseExpToLevelUp = 50f; 
    [SerializeField] private float _expMultiplier = 1.2f;

    private int _currentLevel = 1;
    private float _currentExp = 0f;
    private EnemyDetecter _enemyDetecter;

    public event Action<int> OnLevelUp; 
    public event Action<int, float, float> OnExperienceAdded;

    public float ExpToNextLevel => CalculateExpToLevel(_currentLevel);

    private void Awake()
    {
        _enemyDetecter = GetComponentInChildren<EnemyDetecter>();
    }

    private void OnEnable()
    {
        _enemyDetecter.OnKilled += Add;
    }

    private void OnDisable()
    {
        _enemyDetecter.OnKilled -= Add;
    }

    private float CalculateExpToLevel(int level)
    {
        return _baseExpToLevelUp * Mathf.Pow(_expMultiplier, level - 1);
    }

    public void Add(IDemageable enemy)
    {
        _currentExp += enemy.MaxHealth;

        if (_currentExp >= ExpToNextLevel)
        {
            _currentExp -= ExpToNextLevel; 
            LevelUp();
            CalculateExpToLevel(_currentLevel);
        }

        OnExperienceAdded?.Invoke(_currentLevel, _currentExp, ExpToNextLevel);
    }

    private void LevelUp()
    {
        _currentLevel++;
        OnLevelUp?.Invoke(_currentLevel);
    }
}
