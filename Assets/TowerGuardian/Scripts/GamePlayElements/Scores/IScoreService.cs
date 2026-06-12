using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.GamePlayElements.Scores
{
    public interface IScoreService : IService
    {
        void AddScore(ScoreType type, int count = 0);

        int GetScore();
    }
}