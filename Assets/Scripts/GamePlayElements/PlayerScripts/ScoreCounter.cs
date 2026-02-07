using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ScoreCounter
{
    private float _time = 0;
    private LevelID _currentLevel;

    private IScoreService _scoreService;
    private LevelConfig _config;

    public event Action<float, float, int> LevelEnded;

    public ScoreCounter(Player player, LevelConfig config)
    {
        _scoreService = ServiceLocator.Get<IScoreService>();
        _config = config;
        _currentLevel = config.Level;
        _time = Time.time;
    }

    public void OnEndLevel(LevelID level = LevelID.None)
    {
        if (level == LevelID.None)
        {
            LevelEnded?.Invoke(_scoreService.GetScore(), Time.time - _time, CalculateStars());
            SaveScore();
        }
        else
        {
            LoadBestScore(level);
        }
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

        bool levelFound = false;

        for (int i = 0; i < YG2.saves.LevelsProgress.Count; i++)
        {
            var levelSave = YG2.saves.LevelsProgress[i];

            if (_currentLevel == levelSave.Level)
            {
                levelFound = true;

                if (_scoreService.GetScore() > levelSave.Score)
                {
                    YG2.saves.LevelsProgress[i] = new LevelSaveData(_currentLevel, _scoreService.GetScore(), CalculateStars(), _time);
                }

                break;
            }
        }

        if (levelFound == false)
        {
            YG2.saves.LevelsProgress.Add(new LevelSaveData(_currentLevel, _scoreService.GetScore(), CalculateStars(), _time));
        }

        YG2.SaveProgress();
    }

    private void LoadBestScore(LevelID level)
    {
        if (YG2.saves.LevelsProgress == null)
        {
            LevelEnded?.Invoke(0, 0, 0);
            return;
        }

        var savedData = YG2.saves.LevelsProgress.Find(levelSave => levelSave.Level == level);
        
        if(savedData.Level == LevelID.None)
        {
            LevelEnded?.Invoke(0, 0, 0);
        }
        else
        {
            LevelEnded?.Invoke(savedData.Score, savedData.Time, savedData.Stars);
        }
    }
}
