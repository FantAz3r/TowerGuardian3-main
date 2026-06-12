using TMPro;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class LevelViewer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _levelText;
        [SerializeField]
        private Slider _experienceFillImage;
        [SerializeField]
        private TMP_Text _experienceText;

        private PlayerExperience _playerExperience;

        private void Awake()
        {
            _playerExperience = ServiceLocator.Get<IGameFactory>().Player.Experience;

            gameObject.SetActive(true);
            View(_playerExperience.CurrentExp, _playerExperience.ExpToNextLevel);

            _playerExperience.OnExperienceAdded += View;
            _playerExperience.OnLevelUp += OnLevelUp;
            OnLevelUp();
        }

        private void OnDestroy()
        {
            _playerExperience.OnExperienceAdded -= View;
            _playerExperience.OnLevelUp -= OnLevelUp;
        }

        private void View(float currentExp, float expForNextLevel)
        {
            _experienceText.text = $"{Mathf.Floor(currentExp)} / {Mathf.Floor(expForNextLevel)}";
            _experienceFillImage.value = Mathf.Clamp01(currentExp / expForNextLevel);
        }

        private void OnLevelUp()
        {
            _levelText.text = _playerExperience.CurrentLevel.ToString();
        }
    }
}