using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ADVService : IADVServise
{
    private Dictionary<string, float> _lastRunTimes = new Dictionary<string, float>();
    private const float CooldownSeconds = 60f;


    public void TryShowRewardADV(string rewardID, Action callback)
    {
        float currentTime = Time.time;

        if (_lastRunTimes.TryGetValue(rewardID, out float lastRunTime))
        {
            if (currentTime - lastRunTime < CooldownSeconds)
            {
                return;
            }
        }

        _lastRunTimes[rewardID] = currentTime;

        YG2.RewardedAdvShow(rewardID, callback);
    }

    public bool CanShowRewardADV(string rewardID)
    {
        float currentTime = Time.time;

        if (_lastRunTimes.TryGetValue(rewardID, out float lastRunTime))
        {
            if (currentTime - lastRunTime < CooldownSeconds)
            {
                return false;
            }
        }

        return true;
    }
}
