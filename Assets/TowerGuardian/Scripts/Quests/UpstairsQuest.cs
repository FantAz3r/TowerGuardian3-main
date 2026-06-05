using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;

namespace TowerGuardian.Scripts.Quests
{
    public class UpstairsQuest : Quest
    {
        private StairsTrigger _collider;

        public UpstairsQuest(StairsTrigger collider)
        {
            _collider = collider;
        }

        public override QuestType GetQuestType() => QuestType.UpStairs;

        public override Vector3 TryGetTarget() => _collider.Center;

        public override void Run()
        {
            base.Run();
            _collider.Entered += Complete;
        }

        public override void Complete()
        {
            _collider.Entered -= Complete;
            base.Complete();
        }
    }
}