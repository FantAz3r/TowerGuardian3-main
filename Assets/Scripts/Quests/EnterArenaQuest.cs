using System.Collections.Generic;
using UnityEngine;

public class EnterArenaQuest : Quest
{
    private ISceneContainer _container;
    private ArenaTrigger _arena;
    private List<Bridge> _bridges = new ();

    public EnterArenaQuest()
    {
        _container = ServiceLocator.Get<IGameFactory>().SceneContainer;
    }

    public override QuestType GetQuestType() => QuestType.EnterArena;

    public override Vector3 TryGetTarget()
    {
        foreach (var item in _container.QuestObjects)
        {
            if (item.TryGetComponent(out ArenaTrigger arena))
            {
                _arena = arena;
                _arena.Entered += Complete;
            }
        }

        if (_arena != null)
        {
            return _arena.transform.position;
        }

        return base.TryGetTarget();
    }

    public override void Run()
    {
        base.Run();

        foreach (var item in _container.QuestObjects)
        {
            if (item.TryGetComponent(out Bridge bridge))
            {
                _bridges.Add(bridge);
                bridge.LowerBridge();
            }

            if (item.TryGetComponent(out ArenaTrigger arena))
            {
                _arena = arena;
                _arena.Entered += Complete;
            }
        }
    }

    public override void Complete()
    {
        UpperBridges();
        _arena.Entered -= Complete;
        base.Complete();
    }

    public override void Stop()
    {
        UpperBridges();
        _arena.Entered -= Complete;
        base.Stop();
    }

    private void UpperBridges()
    {
        foreach (var bridge in _bridges)
        {
            bridge.RaiseBridge();
        }
    }
}
