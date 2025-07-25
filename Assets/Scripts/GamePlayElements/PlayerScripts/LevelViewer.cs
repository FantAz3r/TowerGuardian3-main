using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText; 
    [SerializeField] private Image experienceFillImage;

    private PlayerExperience _playerExperience;
    private Coroutine _fillCoroutine;

    private void Awake()
    {
        _playerExperience = GetComponentInChildren<PlayerExperience>();
    }

    private void OnEnable()
    {
        _playerExperience.OnExperienceAdded += View;
    }

    private void OnDisable()
    {
        _playerExperience.OnExperienceAdded -= View;
    }

    public void View(int currentLevel, float currentExp, float expForNextLevel)
    {
        levelText.text = $"LVL {currentLevel}";

        float targetFill = Mathf.Clamp01(currentExp / expForNextLevel);

        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(AnimateFillAmount(targetFill));
    }

    private IEnumerator AnimateFillAmount(float targetFill)
    {
        float startFill = experienceFillImage.fillAmount;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            experienceFillImage.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / duration);
            yield return null;
        }

        experienceFillImage.fillAmount = targetFill;
        _fillCoroutine = null;
    }
}
