using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class GameConditionService : IGameConditionService
    {
        private IWindowService _windowService;

        public GameConditionService(IWindowService windowService) => _windowService = windowService;

        public bool IsLevelEnded { get; private set; }
        public bool IsEndLevelWindowOpen { get; private set; }

        public void OnLouse(GameObject louseReason = null)
        {
            IsEndLevelWindowOpen = true;
            _windowService.Open(WindowType.LouseLevelMenu, louseReason);
        }

        public void OnStart(Portal portal)
        {
            IsEndLevelWindowOpen = true;
            _windowService.Open(WindowType.StartLevelMenu, portal.gameObject);
        }

        public void OnWin()
        {
            IsEndLevelWindowOpen = true;
            _windowService.Open(WindowType.WinLevelMenu);
            SetLevelEnded();
        }

        public void SetLevelEnded(bool isLevelEnded = true)
        {
            IsLevelEnded = isLevelEnded;
        }

        public void SetEndLevelWindowOpen(bool isEndLevelWindowOpen)
        {
            IsEndLevelWindowOpen = isEndLevelWindowOpen;
        }
    }
}