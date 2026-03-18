using System.Collections;
using System.Linq;
using UnityEngine;

public class DefendPortalQuest : Quest
{
    private PortalFrame _portalFrame;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _timeRoutine;

    public override QuestType GetQuestType() => QuestType.DefendPortal;
    public override Vector3 TryGetTarget() => _portalFrame.transform.position;

    public DefendPortalQuest()
    {
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
    }

    public override void Run()
    {
        ServiceLocator.Get<IGameFactory>().SceneContainer.QuestObjects.First().TryGetComponent(out PortalFrame portalFrame);

        if(portalFrame == null)
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

    public override void UpdateProgress(float currentHealth, float maxHealth)
    {
        base.UpdateProgress(currentHealth, maxHealth);
    }

    private IEnumerator TimeRoutine()
    {
        CurrentTime = Config.TimeLimit;

        while (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
            base.UpdateTime();
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

