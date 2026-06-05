using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;

namespace TowerGuardian.Scripts.Quests
{
    public class GameCompleteQuest : Quest
    {
        public override QuestType GetQuestType() => QuestType.GameComplete;
    }
}