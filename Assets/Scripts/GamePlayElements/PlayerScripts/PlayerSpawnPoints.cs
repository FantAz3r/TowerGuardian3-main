using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpawnPoints", menuName = "Configs/SpawnPoints")]
public class PlayerSpawnPoints : ScriptableObject
{
    public List<PointInfo> SpawnPoints = new ();

}
