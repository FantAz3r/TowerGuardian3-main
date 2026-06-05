using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    [CreateAssetMenu(fileName = "PlayerSpawnPoints", menuName = "Configs/SpawnPoints")]
    public class PlayerSpawnPoints : ScriptableObject
    {
        public List<PointInfo> SpawnPoints = new();
    }
}
