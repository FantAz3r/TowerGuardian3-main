using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyBuildingsQuest : Quest
{
    private ICoroutineRunner _coroutineRunner;
    private List<EnemyBuilding> _buildings = new ();
    private Coroutine _timeRoutine;
    public override QuestType GetQuestType() => QuestType.DestroyBuildings;

    public override void Run()
    {
        base.Run();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();

        foreach (var item in ServiceLocator.Get<IGameFactory>().SceneContainer.QuestObjects)
        {
            if (item.TryGetComponent(out EnemyBuilding building))
            {
                _buildings.Add(building);
            }
        }

        if (_buildings.Count == 0)
        {
            Debug.LogError("Постройки не найдены на сцене!");
            Complete();
            return;
        }

        foreach (var building in _buildings)
        {
            building.Destroyed += OnBuildingDestroyed;
            building.Health.enabled = true;
        }

        _timeRoutine = _coroutineRunner.StartCoroutine(TimeRoutine());
    }

    private void OnBuildingDestroyed()
    {
        CurrentValue++;
        UpdateProgress(CurrentValue, Config.TargetValue);

        if (CurrentValue >= Config.TargetValue)
        {
            Complete();
            UnsubscribeAll();
        }
    }

    private void UnsubscribeAll()
    {
        foreach (var building in _buildings)
        {
            building.Destroyed += OnBuildingDestroyed;
        }
    }

    public override void Fail()
    {
        base.Fail();
        _coroutineRunner.StopCoroutine(_timeRoutine);
        UnsubscribeAll();
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
        base.Stop();
        _coroutineRunner.StopCoroutine(_timeRoutine);
        UnsubscribeAll();
        QuestViewer.Highlighter.DeactivateWarning();
    }

    public override void Complete()
    {
        _coroutineRunner.StopCoroutine(_timeRoutine);
        UnsubscribeAll();
        QuestViewer.Highlighter.DeactivateWarning();
        base.Complete();
    }
}
