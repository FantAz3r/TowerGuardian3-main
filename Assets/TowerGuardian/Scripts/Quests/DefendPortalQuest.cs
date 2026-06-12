using System.Collections;
using System.Linq;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;

namespace TowerGuardian.Scripts.Quests
{
    public class DefendPortalQuest : Quest
    {
        private PortalFrame _portalFrame;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _timeRoutine;

        public DefendPortalQuest()
        {
            _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        }

        public override QuestType GetQuestType() => QuestType.DefendPortal;

        public override Vector3 TryGetTarget() => _portalFrame.transform.position;

        public override void Run()
        {
            ServiceLocator.Get<IGameFactory>().SceneContainer.QuestObjects.First().TryGetComponent(out PortalFrame portalFrame);

            if (portalFrame == null)
            {
                Complete();
                return;
            }

            _portalFrame = portalFrame;
            _portalFrame.Activate();

            base.Run();
            CanStop = false;

            _portalFrame.Health.IsValueChange += UpdateProgress;
            _portalFrame.Health.Died += Fail;

            UpdateProgress(_portalFrame.Health.CurrentHealth, _portalFrame.Health.MaxHealth);

            _timeRoutine = _coroutineRunner.StartCoroutine(TimeRoutine());
        }

        private IEnumerator TimeRoutine()
        {
            CurrentTime = Config.TimeLimit;

            while (CurrentTime >= 0)
            {
                CurrentTime -= Time.deltaTime;
                UpdateTime();
                yield return null;
            }

            Complete();
        }

        public override void Fail()
        {
            EndQuest();
            base.Fail();
        }

        public override void Stop()
        {
            base.Stop();
            EndQuest();
        }

        public override void Complete()
        {
            EndQuest();
            base.Complete();
        }

        private void EndQuest()
        {
            _coroutineRunner.StopCoroutine(_timeRoutine);

            if (_portalFrame != null)
            {
                _portalFrame.Health.IsValueChange -= UpdateProgress;
                _portalFrame.Health.Died -= Fail;
                _portalFrame.Deactivate();
            }

            CanStop = true;
        }
    }
}