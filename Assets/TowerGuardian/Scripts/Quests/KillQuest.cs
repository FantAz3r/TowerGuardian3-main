using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;

namespace TowerGuardian.Scripts.Quests
{
    public class KillQuest : Quest
    {
        private EnemyDetector _enemyDetector;

        public KillQuest(EnemyDetector enemyDetector) => _enemyDetector = enemyDetector;

        public override QuestType GetQuestType() => QuestType.KillEnemy;

        public override void Run()
        {
            base.Run();
            CurrentValue--;
            _enemyDetector.OnEnemyKilled += UpdateProgress;
        }

        public override void Complete()
        {
            _enemyDetector.OnEnemyKilled -= UpdateProgress;
            base.Complete();
        }

        public override void Stop()
        {
            _enemyDetector.OnEnemyKilled -= UpdateProgress;
            base.Stop();
        }

        public override void UpdateProgress()
        {
            CurrentValue++;
            base.UpdateProgress();

            if (CurrentValue >= Config.TargetValue)
            {
                Complete();
            }
        }
    }
}