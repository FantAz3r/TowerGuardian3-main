using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider experienceFillImage;
    [SerializeField] private TMP_Text experienceText;

    private PlayerExperience _playerExperience;
    private Coroutine _fillCoroutine;

    public void Init(PlayerExperience playerExperience)
    {
        _playerExperience = playerExperience;
        _playerExperience.OnExperienceAdded += View;
    }

    private void OnDestroy()
    {
        if (_playerExperience != null)
            _playerExperience.OnExperienceAdded -= View;
    }

    public void View(int currentLevel, float currentExp, float expForNextLevel)
    {
        levelText.text = currentLevel.ToString();
        experienceText.text = $"{Mathf.Floor(currentExp)} / {Mathf.Floor(expForNextLevel)}";

        float normalizedExp = Mathf.Clamp01(currentExp / expForNextLevel);

        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(AnimateFill(normalizedExp));
    }

    private IEnumerator AnimateFill(float targetFill)
    {
        float duration = 0.5f;
        float startFill = experienceFillImage.value;
        float elapsed = 0f;

        if (targetFill < startFill)
        {
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                experienceFillImage.value = Mathf.Lerp(startFill, 1f, elapsed / duration);
                yield return null;
            }

            experienceFillImage.value = 1f;

            yield return new WaitForSeconds(0.1f);

            experienceFillImage.value = 0f;
            experienceText.text = $"0 / {_playerExperience.ExpToNextLevel}";

            elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                experienceFillImage.value = Mathf.Lerp(0f, targetFill, elapsed / duration);
                yield return null;
            }
        }
        else
        {
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                experienceFillImage.value = Mathf.Lerp(startFill, targetFill, elapsed / duration);
                yield return null;
            }
        }

        experienceFillImage.value = targetFill;
        _fillCoroutine = null;
    }
}