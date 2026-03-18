using System.Collections;
using UnityEngine;

public class OutpostCaptureQuest : Quest
{
    private AvanpostContainer _avanpostContainer;
    private ICoroutineRunner _coroutineRunner;
    private Coroutine _timeRoutine;
    
    public override QuestType GetQuestType() => QuestType.OutpostCapture;

    public override void Run()
    {
        base.Run();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();

        foreach (var item in ServiceLocator.Get<IGameFactory>().SceneContainer.QuestObjects)
        {
            if (item.TryGetComponent(out AvanpostContainer outpost))
            {
                _avanpostContainer = outpost;
            }
        }

        if (_avanpostContainer == null)
        {
            Debug.LogError("AvanpostContainer не найден на сцене!");
            Complete();
            return;
        }

        foreach (Outpost outpost in _avanpostContainer.Outposts)
        {
            outpost.Complited += OnOutpostCaptured;
            outpost.Enable();
        }

        _timeRoutine = _coroutineRunner.StartCoroutine(TimeRoutine());
    }

    private void OnOutpostCaptured()
    {
        CurrentValue++;
        UpdateProgress();

        if (CurrentValue >= Config.TargetValue)
        {
            Complete();
            UnsubscribeAll();
        }
    }

    public override void UpdateProgress()
    {
        base.UpdateProgress(CurrentValue, Config.TargetValue);
    }

    public override void Stop()
    {
        _coroutineRunner.StopCoroutine(_timeRoutine);
        UnsubscribeAll();
        base.Stop();
    }

    public override void Fail()
    {
        base.Fail();
        _coroutineRunner.StopCoroutine(_timeRoutine);
        UnsubscribeAll();
    }

    private void UnsubscribeAll()
    {
        foreach (var outpost in _avanpostContainer.Outposts)
        {
            outpost.Complited -= OnOutpostCaptured;
        }
    }

    private IEnumerator TimeRoutine()
    {
        CurrentTime = Config.TimeLimit;
        QuestViewer.Highlighter.ActivateWarning();

        while (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
            base.UpdateTime();
            yield return null;
        }

        QuestViewer.Highlighter.DeactivateWarning();
        Fail();
    }

    public override void Complete()
    {
        _coroutineRunner.StopCoroutine(_timeRoutine);
        QuestViewer.Highlighter.DeactivateWarning();
        base.Complete();
    }
}
