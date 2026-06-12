using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.Quests
{
    public class EnterFirstLevelQuest : Quest
    {
        private Portal _portalLevel1;

        public EnterFirstLevelQuest(Portal portal)
        {
            _portalLevel1 = portal;
        }

        public override QuestType GetQuestType() => QuestType.EnterLevel1;

        public override Vector3 TryGetTarget()
        {
            return _portalLevel1.transform.position;
        }

        public override void Run()
        {
            base.Run();

            if (YG2.saves.LevelsProgress == null)
            {
                return;
            }

            foreach (var levelData in YG2.saves.LevelsProgress)
            {
                if (levelData.Level == (int)LevelID.Level1 && levelData.IsComplite)
                {
                    Complete();
                    break;
                }
            }
        }
    }
}