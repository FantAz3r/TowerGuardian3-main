using TMPro;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Windows
{
    public class WinLevelMenu : LevelMenu
    {
        [SerializeField]
        private Button _nextLevelButton;
        [SerializeField]
        private AudioClip _winSound;
        [SerializeField]
        private TMP_Text _levelNumberText;

        protected override void OnEnable()
        {
            base.OnEnable();
            _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }

        protected override void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
            base.OnDisable();
        }

        private void OnNextLevelClicked()
        {
            StateSwitchService.Switch(LevelID.Tower);
            Close();
        }

        public override void Open()
        {
            base.Open();
            _levelNumberText.text = ((int) CurrentLevel - EnumGameLevelOffset).ToString();
            ScoreCounter.OnEndLevel(this);
            ServiceLocator.Get<ISpawnerService>().SendSoundReqest(_winSound);
        }
    }
}