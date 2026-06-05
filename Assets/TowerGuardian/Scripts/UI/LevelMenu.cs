using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI
{
    public abstract class LevelMenu : PauseWindow
    {
        public const int EnumGameLevelOffset = 3;

        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        public IADVServise ADVServise { get; private set; }
        public IGameConditionService ConditionService { get; private set; }
        public LevelID CurrentLevel { get; private set; }
        public ScoreCounter ScoreCounter { get; private set; }
        public IStateSwitchService StateSwitchService { get; private set; }
        public IGameFactory GameFactory { get; private set; }
        public IWindowService WindowService { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ConditionService = ServiceLocator.Get<IGameConditionService>();
            WindowService = ServiceLocator.Get<IWindowService>();
            StateSwitchService = ServiceLocator.Get<IStateSwitchService>();
            GameFactory = ServiceLocator.Get<IGameFactory>();
            ADVServise = ServiceLocator.Get<IADVServise>();

            ScoreCounter = GameFactory.ScoreCounter;
            CurrentLevel = GameFactory.LevelConfig.Level;
        }

        protected virtual void OnEnable()
        {
            if (_restartButton != null)
            {
                _restartButton.onClick.AddListener(OnRestartClicked);
            }

            if (_homeButton != null)
            {
                _homeButton.onClick.AddListener(OnHomeClicked);
            }
        }

        protected virtual void OnDisable()
        {
            if (_restartButton != null)
            {
                _restartButton.onClick.RemoveListener(OnRestartClicked);
            }

            if (_homeButton != null)
            {
                _homeButton.onClick.RemoveListener(OnHomeClicked);
            }
        }

        private void OnRestartClicked()
        {
            StateSwitchService.Switch(CurrentLevel);
            ADVServise.TryShowInterstitialADV(CurrentLevel.ToString());
            ConditionService.SetLevelEnded();
            Close();
        }

        protected virtual void OnHomeClicked()
        {
            StateSwitchService.Switch(LevelID.Tower);
            ADVServise.TryShowInterstitialADV(LevelID.Tower.ToString());
            ConditionService.SetLevelEnded();
            Close();
        }
    }
}