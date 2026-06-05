using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.Quests
{
    public class EnterSecondLevelQuest : Quest
    {
        private Portal _portalLevel2;

        public EnterSecondLevelQuest(Portal portal)
        {
            _portalLevel2 = portal;
        }
        public override QuestType GetQuestType() => QuestType.EnterLevel2;

        public override Vector3 TryGetTarget()
        {
            return _portalLevel2.transform.position;
        }

        public override void Run()
        {
            base.Run();

            if (YG2.saves.LevelsProgress == null) return;

            foreach (var levelData in YG2.saves.LevelsProgress)
            {
                if (levelData.Level == LevelID.Level2 && levelData.IsComplite)
                {
                    Complete();
                    break;
                }
            }
        }
    }
}