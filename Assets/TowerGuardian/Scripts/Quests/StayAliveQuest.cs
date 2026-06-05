using System.Linq;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Effects;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using TowerGuardian.Scripts.Quests.QuestInfrastructure;
using TowerGuardian.Scripts.StaticData;
using TowerGuardian.Scripts.StaticData.Datas;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.Quests
{
    public class StayAliveQuest : Quest
    {
        private Effect _effect;
        private EffectData _effectData;
        private Portal _portal;
        private ISceneContainer _sceneContainer;

        public StayAliveQuest()
        {
            _effectData = Resources.Load<EffectData>(GameConstants.EffectData);
        }

        public override QuestType GetQuestType() => QuestType.StayAlive;

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
}