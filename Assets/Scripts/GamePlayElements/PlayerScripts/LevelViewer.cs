using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText; 
    [SerializeField] private Image experienceFillImage;

    private float _wait = 0.1f;
    private WaitForSeconds _delay;
    private PlayerExperience _playerExperience;
    private Coroutine _fillCoroutine;

    private void Awake()
    {
        _delay = new WaitForSeconds(_wait);
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

        float normalizedExp = Mathf.Clamp01(currentExp / expForNextLevel);

        if (_fillCoroutine != null)
            StopCoroutine(_fillCoroutine);

        _fillCoroutine = StartCoroutine(AnimateFillWithOverflow(normalizedExp));
    }

    private IEnumerator AnimateFillWithOverflow(float targetFill)
    {
        float duration = 0.5f;

        IEnumerator AnimateFillSegment(float from, float to)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                experienceFillImage.fillAmount = Mathf.Lerp(from, to, elapsed / duration);
                yield return null;
            }
            experienceFillImage.fillAmount = to;
        }

        float startFill = experienceFillImage.fillAmount;

        if (targetFill < startFill)
        {
            yield return AnimateFillSegment(startFill, 1f);
            yield return _delay;
            experienceFillImage.fillAmount = 0f;
            yield return AnimateFillSegment(0f, targetFill);
        }
        else
        {
            yield return AnimateFillSegment(startFill, targetFill);
        }

        _fillCoroutine = null;
    }

}
