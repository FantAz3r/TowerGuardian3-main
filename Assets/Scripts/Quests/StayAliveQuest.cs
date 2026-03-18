using System.Linq;
using UnityEngine;

public class StayAliveQuest : Quest
{
    public override QuestType GetQuestType() => QuestType.StayAlive;

    private Effect _effect;
    private EffectData _effectData;
    private Portal _portal;
    private ISceneContainer _sceneContainer;

    public StayAliveQuest()
    {
       
        _effectData = Resources.Load<EffectData>(GameConstants.EffectData);
    }

    public override void Run()
    {
        _sceneContainer = ServiceLocator.Get<IGameFactory>().SceneContainer;
        base.Run();
        _portal = _sceneContainer.Portals.First();
        EffectInfo info = _effectData.GetEffectInfo(EffectType.PortalChrge);
        _effect = Object.Instantiate(info.Prefab, _portal.transform.position + info.Offset, Quaternion.identity);
    }

    public override void Stop()
    {
        _effect.Destroy();
        base.Stop();
    }
}
