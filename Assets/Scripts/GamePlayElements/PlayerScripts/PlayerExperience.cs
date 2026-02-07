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
    private int _upgradePoints = 50;
    private float _currentExp = 0f;
    private ISpawnerService _spawnerService;
    private IScoreService _scoreService;
    private Queue<float> _expQueue = new Queue<float>();
    private bool _isUpdating = false;

    public float CurrentExp => _currentExp;
    public int CurrentLevel => _currentLevel;
    public int UpgradePoints => _upgradePoints;

    public event Action OnLevelUp;
    public event Action OnUpgradePointAdded;
    public event Action OnUpgradePointRemoved;
    public event Action<float, float> OnExperienceAdded;

    public float ExpToNextLevel => _config.BaseLvlCost * Mathf.Pow(_config.LevelCostMultiplier, _currentLevel - 1);

    private void Awake()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _scoreService = ServiceLocator.Get<IScoreService>();
        _enemyDetector = GetComponentInChildren<EnemyDetector>();

        if (_enemyDetector != null)
            _enemyDetector.OnGetExperience += AddEXP;

        LoadLevel();
    }

    private void OnDestroy()
    {
        if (_enemyDetector != null)
            _enemyDetector.OnGetExperience -= AddEXP;

        SaveLevel();
    }

    public void AddEXP(float amount)
    {
        _expQueue.Enqueue(amount);

        if (_isUpdating == false)
            StartCoroutine(ProcessExpQueue());
    }

    public void AddUpgradePoints(int count)
    {
        _upgradePoints += count;
        OnUpgradePointAdded?.Invoke();
    }

    public void RemoveUpgradePoint(int count)
    {
        _upgradePoints -= count;
        OnUpgradePointRemoved?.Invoke();
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

            OnExperienceAdded?.Invoke(currentExp, ExpToNextLevel);

            yield return null;
        }

        OnExperienceAdded?.Invoke(targetExp, ExpToNextLevel);
    }

    private void LevelUp()
    {
        _scoreService.AddScore(ScoreType.Levelup, _config.ScorePerLevel);
        _spawnerService.SendEffectReqest(_config.LevelUpEffect, transform.position, transform);
        AddUpgradePoints(1);
        _currentLevel++;
        OnLevelUp?.Invoke();
    }

    private void SaveLevel()
    {
        YG2.saves.Level = _currentLevel;
        YG2.saves.CurrentEXP = _currentExp;
        YG2.saves.UpgradePoints = _upgradePoints;
        YG2.SaveProgress();
    }

    private void LoadLevel()
    {
        if (YG2.saves == null)
            return;

        _upgradePoints = YG2.saves.UpgradePoints;
        _currentLevel = YG2.saves.Level;
        _currentExp = YG2.saves.CurrentEXP;
    }
}
