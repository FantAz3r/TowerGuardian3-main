using System;
using System.Linq;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
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
                if (currentLevel == LevelID.Tower && YG2.saves.PreviousLevel != LevelID.None)
                {
                    return GetPortalPoint(YG2.saves.PreviousLevel);
                }

                return _sceneContainer.PlayerSpawnPoints.First().transform.position;
            }

            if (currentLevel == LevelID.Tower)
            {
                return GetPortalPoint(previousLevel);

                throw new ArgumentNullException("no spawn point");
            }

            return _sceneContainer.PlayerSpawnPoints.First().transform.position;
        }

        private Vector3 GetPortalPoint(LevelID previousLevel)
        {
            foreach (var point in _sceneContainer.PlayerSpawnPoints)
            {
                if (point.PreviousLevel == previousLevel)
                {
                    return point.transform.position;
                }
            }

            return Vector3.zero;
        }
    }
}