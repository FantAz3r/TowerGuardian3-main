using System.Collections.Generic;
using UnityEngine;
using YG;

public class LeaderBoard : PauseWindow
{
    [SerializeField] private List<LeaderboardYG> _leaderboards;

    private void OnEnable()
    {
        foreach (var leaderboard in _leaderboards)
        {
            leaderboard.gameObject.SetActive(false);
        }
    }
}
