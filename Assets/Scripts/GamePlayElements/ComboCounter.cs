using System.Collections;
using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private float comboResetTime = 3f;

    private int _comboCounter = 0;
    private Coroutine _comboCoroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new WaitForSeconds(comboResetTime);
    }

    private void OnEnable()
    {
        _enemyDetector.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _enemyDetector.OnEnemyKilled -= OnEnemyKilled;

        if (_comboCoroutine != null)
        {
            StopCoroutine(_comboCoroutine);
            _comboCoroutine = null;
        }
    }

    private void OnEnemyKilled()
    {
        _comboCounter++;

        if (_comboCoroutine != null)
        {
            StopCoroutine(_comboCoroutine);
        }

        _comboCoroutine = StartCoroutine(ResetComboAfterDelay());

    }

    private IEnumerator ResetComboAfterDelay()
    {
        yield return _wait;

        _comboCounter = 0;
        _comboCoroutine = null;
    }
}
