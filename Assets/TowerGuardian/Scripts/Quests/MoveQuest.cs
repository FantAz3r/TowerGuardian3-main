using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;

namespace TowerGuardian.Scripts.Quests
{
    public class MoveQuest : Quest
    {
        private PlayerMover _mover;

        public override QuestType GetQuestType() => QuestType.Move;

        public override void Run()
        {
            _mover = ServiceLocator.Get<IGameFactory>().Player.PlayerMover;
            base.Run();
            _mover.Moved += Complete;
        }

        public override void Stop()
        {
            base.Stop();
            _mover.Moved -= Complete;
        }

        public override void Complete()
        {
            _mover.Moved -= Complete;
            base.Complete();
        }
    }
}