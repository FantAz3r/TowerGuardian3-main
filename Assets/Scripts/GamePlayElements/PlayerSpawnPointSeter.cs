using System;
using System.Linq;
using UnityEngine;
using YG;

public class PlayerSpawnPointSeter
{
    private ISceneContainer _sceneContainer;

    public PlayerSpawnPointSeter(ISceneContainer sceneContainer)
    {
        _sceneContainer = sceneContainer;
    }

    public Vector3 GetSpawnPoint(LevelID currentLevel, LevelID previousLevel)
    {
        if (previousLevel == LevelID.None || previousLevel == LevelID.MainMenu)
        {
            if (YG2.saves.PlayerPosition == Vector3.zero)
            {
                return _sceneContainer.PlayerSpawnPoints.First().transform.position;
            }

            return YG2.saves.PlayerPosition;
        }

        if (currentLevel == LevelID.Tower)
        {
            foreach (var point in _sceneContainer.PlayerSpawnPoints)
            {
                if (point.PreviousLevel == previousLevel)
                {
                    return point.transform.position;
                }
            }

            throw new ArgumentNullException("нет соответствующей точки спавна");
        }
        else
        {
            return _sceneContainer.PlayerSpawnPoints.First().transform.position;
        }
    }
}
