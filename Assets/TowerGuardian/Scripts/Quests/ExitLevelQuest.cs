using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using UnityEngine;

namespace TowerGuardian.Scripts.Quests
{
    public class ExitLevelQuest : Quest
    {
        private Portal _portal;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _timeRoutine;

        public ExitLevelQuest(List<Portal> portals)
        {
            _portal = portals.First();
            _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        }

        public override QuestType GetQuestType() => QuestType.GetOut;

        public override Vector3 TryGetTarget() => _portal.transform.position;

        public override void Run()
        {
            base.Run();
            _portal.gameObject.SetActive(true);
            _portal.CanExit(true);
            _portal.Entered += Complete;
            _timeRoutine = _coroutineRunner.StartCoroutine(TimeRoutine());
        }

        private IEnumerator TimeRoutine()
        {
            CurrentTime = Config.TimeLimit;
            QuestViewer.Highlighter.ActivateWarning();

            while (CurrentTime >= 0)
            {
                CurrentTime -= Time.deltaTime;
                UpdateTime();
                yield return null;
            }

            QuestViewer.Highlighter.DeactivateWarning();
            Fail();
        }

        public override void Stop()
        {
            _portal.Entered -= Complete;
            _coroutineRunner.StopCoroutine(_timeRoutine);
            QuestViewer.Highlighter.DeactivateWarning();
            base.Stop();
        }

        public override void Complete()
        {
            _coroutineRunner.StopCoroutine(_timeRoutine);
            QuestViewer.Highlighter.DeactivateWarning();
            _portal.Entered -= Complete;
            base.Complete();
        }
    }
}