using System;
using UnityEngine;
using YG;

public class PlayerSpawnPointSeter
{
    private PlayerSpawnPoints _spawnPointsData;
    private Player _player;

    public PlayerSpawnPointSeter(PlayerSpawnPoints spawnPointsData)
    {
        _spawnPointsData = spawnPointsData;
    }

    public Vector3 GetSpawnPoint(LevelConfig config, LevelID previousLevel)
    {
        if (previousLevel == LevelID.None || previousLevel == LevelID.MainMenu)
        {
            if (YG2.saves.PlayerPosition == Vector3.zero)
            {
                return config.PlayerSpawnPoint;
            }

            return YG2.saves.PlayerPosition;
        }

        if (config.Level == LevelID.Tower)
        {
            foreach (var point in _spawnPointsData.SpawnPoints)
            {
                if (point.PreviousLevel == previousLevel)
                {
                    return point.SpawnPoint.position;
                }
            }

            throw new ArgumentNullException("нет соответствующей точки спавна");
        }
        else
        {
            return config.PlayerSpawnPoint;
        }
    }
}
