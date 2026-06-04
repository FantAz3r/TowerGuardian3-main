using TowerGuardian.Enums;

public interface IScoreService : IService
{
    void AddScore(ScoreType type, int count = 0);
    public int GetScore();
}