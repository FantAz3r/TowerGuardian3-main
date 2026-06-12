using System;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface IADVServise : IService
    {
        void TryShowRewardADV(string rewardID, Action callback);

        bool CanShowRewardADV(string rewardID);

        void TryShowInterstitialADV(string nextLevel);
    }
}
