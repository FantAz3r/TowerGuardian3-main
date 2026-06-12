using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;

namespace TowerGuardian.Scripts.Quests
{
    public class EnterTowerQuest : Quest
    {
        private TowerDoor _door;

        public EnterTowerQuest(TowerDoor door)
        {
            _door = door;
        }

        public override QuestType GetQuestType() => QuestType.EnterTower;

        public override Vector3 TryGetTarget() => _door.transform.position;

        public override void Run()
        {
            _door.Opened += Complete;
            base.Run();
        }

        public override void Complete()
        {
            base.Complete();
            _door.Opened -= Complete;
        }
    }
}