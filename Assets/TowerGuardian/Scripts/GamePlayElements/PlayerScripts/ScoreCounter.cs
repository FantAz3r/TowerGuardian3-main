using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Scores;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Structs;
using TowerGuardian.Scripts.StaticData.Structs.SaveData;
using TowerGuardian.Scripts.UI;
using TowerGuardian.Scripts.UI.Windows;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    public class ScoreCounter
    {
        private const string MainLeaderbord = "MainLiderboard";

        private Dictionary<LevelID, string> LevelLeaderboards = new Dictionary<LevelID, string>
        {
            { LevelID.Level1, "FirstLevelScore" },
            { LevelID.Level2, "SecondLevelScore" },
            { LevelID.Level3, "ThirdLevelScore" },
            { LevelID.Level4, "FourthLevelScore" },
            { LevelID.Level5, "FifthLevelScore" },
        };

        private float _time;
        private LevelID _currentLevel;
        private Player _player;
        private IScoreService _scoreService;
        private LevelConfig _config;

        public ScoreCounter()
        {
            _scoreService = ServiceLocator.Get<IScoreService>();
            _player = ServiceLocator.Get<IGameFactory>().Player;
            _config = ServiceLocator.Get<IGameFactory>().LevelConfig;
            _currentLevel = _config.Level;
            _time = Time.time;
            UpdateMainLeaderbord();
        }

        public event Action<float, float, int, int> LevelEnded;

        public void OnEndLevel(LevelMenu sender, LevelID level = LevelID.None)
        {
            if (sender is WinLevelMenu winLevelMenu)
            {
                LevelEnded?.Invoke(_scoreService.GetScore(), Time.time - _time, CalculateStars(), _scoreService.GetScore() / 20);
                CalculateReward();
                SaveScore();
            }
            else if (sender is LouseLevelMenu louseLevelMenu)
            {
                LevelEnded?.Invoke(_scoreService.GetScore(), Time.time - _time, 0, _scoreService.GetScore() / 20);
            }
            else if (sender is StartLevelMenu startLevelMenu)
            {
                LoadBestScore(level);
            }
        }

        public bool HasScoreInfo(LevelID level)
        {
            if (YG2.saves.LevelsProgress == null)
                return false;

            var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == (int) level);

            return savedData.Score != 0 && savedData.Level != (int) LevelID.None;
        }

        private void CalculateReward()
        {
            var costList = new List<CostInfo>
            {
                new CostInfo(ResourceType.Coin, _scoreService.GetScore() / 20),
            };

            _player.Inventory.AddResousres(costList);
        }

        private int CalculateStars()
        {
            float scoreForOneStar = _config.OneStarScore;
            float scoreForTwoStars = _config.TwoStarScore;
            float scoreForTreeStars = _config.ThreeStarScore;
            float scorePerSecond = _scoreService.GetScore() / _time;

            if (scorePerSecond < scoreForOneStar)
                return 0;
            if (scorePerSecond >= scoreForOneStar && scorePerSecond < scoreForTwoStars)
                return 1;
            if (scorePerSecond >= scoreForTwoStars && scorePerSecond < scoreForTreeStars)
                return 2;
            if (scorePerSecond >= scoreForTreeStars)
                return 3;
            return 0;
        }

        private void SaveScore()
        {
            YG2.saves.LevelsProgress ??= new List<LevelSaveData>();

            float newTime = Time.time - _time;
            int newScore = _scoreService.GetScore();
            int newStars = CalculateStars();
            bool levelFound = false;

            for (int i = 0; i < YG2.saves.LevelsProgress.Count; i++)
            {
                var levelSave = YG2.saves.LevelsProgress[i];

                if ((int) _currentLevel == levelSave.Level)
                {
                    levelFound = true;

                    int updatedScore = (newScore > levelSave.Score) ? newScore : levelSave.Score;

                    int updatedStars = Mathf.Max(newStars, levelSave.Stars);
                    float updatedTime = Mathf.Min(newTime, levelSave.Time);

                    YG2.saves.LevelsProgress[i] = new LevelSaveData((int) _currentLevel, updatedScore, updatedStars, updatedTime);

                    UpdateLevelLeaderboards(_currentLevel);

                    break;
                }
            }

            if (!levelFound)
            {
                YG2.saves.LevelsProgress.Add(new LevelSaveData((int) _currentLevel, newScore, newStars, newTime));
                UpdateLevelLeaderboards(_currentLevel);
            }

            YG2.SaveProgress();
        }

        private void LoadBestScore(LevelID level)
        {
            if (YG2.saves.LevelsProgress == null)
            {
                LevelEnded?.Invoke(0, 0, 0, 0);
                return;
            }

            var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == (int) level);

            if (savedData.Level == (int) LevelID.None)
            {
                LevelEnded?.Invoke(0, 0, 0, 0);
            }
            else
            {
                LevelEnded?.Invoke(savedData.Score, savedData.Time, savedData.Stars, 0);
            }
        }

        private void UpdateLevelLeaderboards(LevelID levelID)
        {
            foreach (var pair in LevelLeaderboards)
            {
                if (levelID == pair.Key)
                {
                    var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == (int) levelID);
                    YG2.SetLeaderboard(pair.Value, savedData.Score);
                }
            }
        }

        private void UpdateMainLeaderbord()
        {
            if (YG2.saves.LevelsProgress == null) return;

            int scoreFromAllLevels = 0;

            foreach (var level in YG2.saves.LevelsProgress)
            {
                scoreFromAllLevels += level.Score;
            }

            YG2.SetLeaderboard(MainLeaderbord, scoreFromAllLevels);
        }
    }
}
