using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ScoreCounter
{
    private float _currentScore;
    private int _time = 0;
    private LevelID _currentLevel;

    private LevelConfig _config;
    private PlayerAttacker _attacker;
    private Player _player;
    private AllAbilities _allAbilities;
    private DayCycle _dayCycle;

    public event Action<float, int, int> LevelEnded;

    public ScoreCounter(Player player, LevelConfig config, DayCycle dayCycle)
    {
        _player = player;
        _config = config;
        _dayCycle = dayCycle;

        _currentLevel = _config.Level;
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
        _allAbilities = _player.GetComponentInChildren<AllAbilities>();

        _attacker.DialedDamage += Add;
        _allAbilities.DialedDamage += Add;
        _dayCycle.TimePassedFromStart += AddTime;
    }

    public void OnEndLevel(LevelID level = LevelID.None)
    {

        if(level == LevelID.None)
        {
            Debug.Log(level + "Save");
            _attacker.DialedDamage -= Add;
            _allAbilities.DialedDamage -= Add;
            LevelEnded?.Invoke(_currentScore, _time, CalculateStars());
            SaveScore();
        }
        else
        {
            ShowBestScore(level);
        }
    }

    private void Add(float score)
    {
        _currentScore += score;
    }

    private void AddTime(float value)
    {
        _time += (int)value;
    }

    private int CalculateStars()
    {
        float scoreForOneStar = _config.OneStarScore;
        float scoreForTwoStars = _config.TwoStarScore;
        float scoreForTreeStars = _config.ThreeStarScore;
        float scorePerSecond = _currentScore / _time;

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

                if (_currentScore > levelSave.Score)
                {
                    YG2.saves.LevelsProgress[i] = new LevelSaveData(_currentLevel, (int)_currentScore, CalculateStars(), _time);
                }

                break;
            }
        }

        if (levelFound == false)
        {
            YG2.saves.LevelsProgress.Add(new LevelSaveData(_currentLevel, (int)_currentScore, CalculateStars(), _time));
        }
    }

    private void ShowBestScore(LevelID level)
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
