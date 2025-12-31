using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private EnemyDetector _enemyDetector;
    private int _currentLevel = 1;
    private float _currentExp = 0f;

    private Queue<float> _expQueue = new Queue<float>();
    private bool _isUpdating = false;

    public float CurrentExp => _currentExp;
    public int CurrentLevel => _currentLevel;

    public event Action<int> OnLevelUp;
    public event Action <float, float> OnExperienceAdded; 

    public float ExpToNextLevel => _config.BaseLvlCost * Mathf.Pow(_config.LevelCostMultiplier, _currentLevel - 1);

    private void Awake()
    {
        _enemyDetector = GetComponentInChildren<EnemyDetector>();

        if (_enemyDetector != null)
            _enemyDetector.OnKilled += Add;

        LoadLevel();
    }

    private void OnDestroy()
    {
        if (_enemyDetector != null)
            _enemyDetector.OnKilled -= Add;

        SaveLevel();
    }

    public void Add(float amount)
    {
        _expQueue.Enqueue(amount);

        if (_isUpdating == false)
            StartCoroutine(ProcessExpQueue());
    }

    private IEnumerator ProcessExpQueue()
    {
        _isUpdating = true;

        while (_expQueue.Count > 0)
        {
            float amount = _expQueue.Dequeue();
            yield return StartCoroutine(AddExpCoroutine(amount));
        }

        _isUpdating = false;
    }

    private IEnumerator AddExpCoroutine(float amount)
    {
        float targetExp = _currentExp + amount;

        while (targetExp >= ExpToNextLevel)
        {
            float fillTo = ExpToNextLevel;

            yield return AnimateExperience(_currentExp, fillTo);

            targetExp -= ExpToNextLevel;
            _currentExp = 0f;
            LevelUp();

            OnExperienceAdded?.Invoke(_currentExp, ExpToNextLevel);
            yield return new WaitForSeconds(0.1f); 
        }

        yield return AnimateExperience(_currentExp, targetExp);
        _currentExp = targetExp;

        OnExperienceAdded?.Invoke(_currentExp, ExpToNextLevel);
    }

    private IEnumerator AnimateExperience(float startExp, float targetExp)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float currentExp = Mathf.Lerp(startExp, targetExp, elapsed / duration);
            float normalizedExp = Mathf.Clamp01(currentExp / ExpToNextLevel);

            OnExperienceAdded?.Invoke( currentExp, ExpToNextLevel);

            yield return null;
        }

        OnExperienceAdded?.Invoke( targetExp, ExpToNextLevel);
    }

    private void LevelUp()
    {
        int upggradePointPerLevel = 1;
        _currentLevel++;
        OnLevelUp?.Invoke(upggradePointPerLevel);
    }

    private void SaveLevel()
    {
        YG2.saves.Level = _currentLevel;
        YG2.saves.CurrentEXP = _currentExp;
        YG2.SaveProgress();
    }


    private void LoadLevel()
    {
        if (YG2.saves == null)
            return;

        _currentLevel = YG2.saves.Level;
        _currentExp = YG2.saves.CurrentEXP;
    }
}
