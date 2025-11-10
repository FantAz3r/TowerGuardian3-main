using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private ScriptableObject _configObject;

    private ILevelConfig _config;
    private int _currentLevel = 1;
    private float _currentExp = 0f;

    public event Action<int> OnLevelUp;
    public event Action<int, float, float> OnExperienceAdded;
    public float ExpToNextLevel => _config.BaseLvlCost * Mathf.Pow(_config.LevelCostMultiplier, _currentLevel - 1);

    private void Awake()
    {
        _config = _configObject as ILevelConfig;

        var enemyDetecter = GetComponentInChildren<EnemyDetecter>();

        if (enemyDetecter != null)
        {
            enemyDetecter.OnKilled += Add;
        }
    }

    private void OnDestroy()
    {
        var enemyDetecter = GetComponentInChildren<EnemyDetecter>();
        if (enemyDetecter != null)
        {
            enemyDetecter.OnKilled -= Add;
        }
    }

    public void Add(float amount)
    {
        _currentExp += amount;

        while (_currentExp >= ExpToNextLevel)
        {
            _currentExp -= ExpToNextLevel;
            LevelUp();
            OnExperienceAdded?.Invoke(_currentLevel, _currentExp, ExpToNextLevel);
        }
    }

    private void LevelUp()
    {
        _currentLevel++;
        OnLevelUp?.Invoke(_currentLevel);
    }
}
