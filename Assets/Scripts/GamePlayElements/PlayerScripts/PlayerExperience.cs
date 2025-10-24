using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private ScriptableObject _configObject;

    private ILevelConfig _config;
    private float _baseLvlCost;
    private float _levelCostMultiplier;
    private int _currentLevel = 1;
    private float _currentExp = 0f;
    private EnemyDetecter _enemyDetecter;

    public event Action<int> OnLevelUp; 
    public event Action<int, float, float> OnExperienceAdded;

    public float ExpToNextLevel => CalculateExpToLevel(_currentLevel);

    private void Awake()
    {
        _config = _configObject as ILevelConfig;
        _baseLvlCost = _config.BaseLvlCost;
        _levelCostMultiplier = _config.LevelCostMultiplier;

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
        return _baseLvlCost * Mathf.Pow(_levelCostMultiplier, level - 1);
    }

    public void Add(float amount) 
    {
        _currentExp += amount;

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
