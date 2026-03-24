using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ScoreCounter
{
    private const string MainLeaderbord = "MainLiderboard";
    private Dictionary<LevelID, string> LevelLeaderboards = new Dictionary<LevelID, string>
    {
        {LevelID.Level1, "FirstLevelScore" },
        {LevelID.Level2, "SecondLevelScore" },
        {LevelID.Level3, "ThirdLevelScore" },
        {LevelID.Level4, "FourthLevelScore" },
        {LevelID.Level5, "FifthLevelScore" }
    };

    private float _time = 0;
    private LevelID _currentLevel;
    private Player _player;
    private IScoreService _scoreService;
    private LevelConfig _config;

    public event Action<float, float, int, int> LevelEnded;

    public ScoreCounter()
    {
        _scoreService = ServiceLocator.Get<IScoreService>();
        _player = ServiceLocator.Get<IGameFactory>().Player;
        _config = ServiceLocator.Get<IGameFactory>().LevelConfig;
        _currentLevel = _config.Level;
        _time = Time.time;
        UpdateMainLeaderbord();
    }

    public void OnEndLevel(LevelMenu sender, LevelID level = LevelID.None)
    {
        if (sender is WinLevelMenu winLevelMenu)
        {
            LevelEnded?.Invoke(_scoreService.GetScore(), Time.time - _time, CalculateStars(), (int)_scoreService.GetScore() / 20);
            CalculateReward();
            SaveScore();
        }
        else if (sender is LouseLevelMenu louseLevelMenu)
        {
            LevelEnded?.Invoke(_scoreService.GetScore(), Time.time - _time, 0, (int)_scoreService.GetScore() / 20);
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

        var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == level);

        return savedData.Score != 0 && savedData.Level != LevelID.None;
    }

    private void CalculateReward()
    {
        var costList = new List<CostInfo>()
            {
                new CostInfo(ResourceType.Coin, (int)_scoreService.GetScore() / 20)
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
        else if (scorePerSecond >= scoreForOneStar && scorePerSecond < scoreForTwoStars)
            return 1;
        else if (scorePerSecond >= scoreForTwoStars && scorePerSecond < scoreForTreeStars)
            return 2;
        else if (scorePerSecond >= scoreForTreeStars)
            return 3;
        else
            return 0;
    }

    private void SaveScore()
    {
        YG2.saves.LevelsProgress ??= new List<LevelSaveData>();

        int newScore = _scoreService.GetScore();
        int newStars = CalculateStars();
        bool levelFound = false;

        for (int i = 0; i < YG2.saves.LevelsProgress.Count; i++)
        {
            var levelSave = YG2.saves.LevelsProgress[i];

            if (_currentLevel == levelSave.Level)
            {
                levelFound = true;

                int updatedScore = (newScore > levelSave.Score) ? newScore : levelSave.Score;

                int updatedStars = Mathf.Max(newStars, levelSave.Stars);
                float updatedTime = Mathf.Min(_time, levelSave.Time);

                YG2.saves.LevelsProgress[i] = new LevelSaveData(_currentLevel, updatedScore, updatedStars, updatedTime);

                UpdateLevelLeaderboards(_currentLevel);

                break;
            }
        }

        if (levelFound == false)
        {
            YG2.saves.LevelsProgress.Add(new LevelSaveData(_currentLevel, newScore, newStars, _time));
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

        var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == level);

        if (savedData.Level == LevelID.None)
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
        foreach(var pair in LevelLeaderboards)
        {
            if(levelID == pair.Key)
            {
                var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == levelID);
                YG2.SetLeaderboard(pair.Value, savedData.Score);
            }
        }
    }

    private void UpdateMainLeaderbord()
    {
        if(YG2.saves.LevelsProgress == null) return;

        int scoreFromAllLevels = 0;

        foreach (var level in YG2.saves.LevelsProgress)
        {
            scoreFromAllLevels += level.Score;
        }

        YG2.SetLeaderboard(MainLeaderbord, scoreFromAllLevels);
    }
}
