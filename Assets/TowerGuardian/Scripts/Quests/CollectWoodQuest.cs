using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;

namespace TowerGuardian.Scripts.Quests
{
    public class CollectWoodQuest : Quest
    {
        private readonly Inventory _inventory;

        public CollectWoodQuest(Inventory inventory)
        {
            _inventory = inventory;
        }

        public override QuestType GetQuestType() => QuestType.Collect;

        public override void Run()
        {
            base.Run();
            _inventory.WoodCollected += UpdateProgress;
            CurrentValue--;
        }

        public override void Stop()
        {
            base.Stop();
            _inventory.WoodCollected -= UpdateProgress;
        }

        public override void Complete()
        {
            base.Complete();
            _inventory.WoodCollected -= UpdateProgress;
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