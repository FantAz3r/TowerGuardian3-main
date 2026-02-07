using System.Collections.Generic;

public class ScoreService : IScoreService
{
    private Dictionary<ScoreType, int> _scoreBySource = new Dictionary<ScoreType, int>();

    public void AddScore(ScoreType type, int count = 0)
    {

        if (_scoreBySource.ContainsKey(type) == false)
        {
            _scoreBySource[type] = 0;
        }

        _scoreBySource[type] += count;
    }

    public int GetScore()
    {
        int scores = 0;

        foreach (var pair in _scoreBySource)
        {
            scores += pair.Value;
        }

        return scores;
    }
}
