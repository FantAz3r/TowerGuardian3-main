using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;


public class LevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Slider _experienceFillImage;
    [SerializeField] private TMP_Text _experienceText;

    private PlayerExperience _playerExperience;

    public void Init(PlayerExperience playerExperience)
    {
        _playerExperience = playerExperience;
        _playerExperience.OnExperienceAdded += View;
        _playerExperience.OnLevelUp += OnLevelUp;


        View(_playerExperience.CurrentExp, _playerExperience.ExpToNextLevel);
        OnLevelUp(_playerExperience.CurrentLevel);
    }

    private void OnDestroy()
    {
        if (_playerExperience != null)
        {
            _playerExperience.OnExperienceAdded -= View;
            _playerExperience.OnLevelUp -= OnLevelUp;
        }
    }

    private void View(float currentExp, float expForNextLevel)
    {
        _experienceText.text = $"{Mathf.Floor(currentExp)} / {Mathf.Floor(expForNextLevel)}";
        _experienceFillImage.value = Mathf.Clamp01(currentExp / expForNextLevel);
    }

    private void OnLevelUp(int newLevel)
    {
        _levelText.text = newLevel.ToString();
    }
}
