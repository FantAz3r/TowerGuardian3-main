using System.Collections.Generic;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.UI.Windows
{
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
}