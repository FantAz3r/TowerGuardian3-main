using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.MainMenu.UIElements
{
    public class ContinueButton : MonoBehaviour
    {
        [SerializeField] private LevelID _levelToLoad;
        private Button _button;
        private IStateSwitchService _switchService;

        private void Awake()
        {
            _switchService = ServiceLocator.Get<IStateSwitchService>();
            _button = GetComponent<Button>();
            LoadLevel();

            if (YG2.isFirstGameSession)
                gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        public void OnClicked()
        {
            _button.interactable = false;
            _switchService.Switch(_levelToLoad);
        }

        private void LoadLevel()
        {
            if (YG2.saves.CurrentLevel == (int) LevelID.None || YG2.saves.CurrentLevel == (int) LevelID.MainMenu)
            {
                _levelToLoad = LevelID.Tower;
                return;
            }

            _levelToLoad = (LevelID) YG2.saves.CurrentLevel;
        }
    }
}
