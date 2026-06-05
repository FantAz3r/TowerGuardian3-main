using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Items;
using TowerGuardian.Scripts.StaticData;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Datas;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.BuffInfrastructure
{
    public class AllBuffs : AllItems<IBuff, BuffConfig, BuffType>
    {
        protected override void Awake()
        {
            base.Awake();
            CreateBuffs();
        }

        protected override BuffType GetTypeFromConfig(BuffConfig config)
        {
            return config.BuffType;
        }

        private void CreateBuffs()
        {
            CardData cardData = Resources.Load<CardData>(GameConstants.CardData);

            Items.Add(new Buff(Player.Health, cardData.Get(BuffType.MaxHp)));
            Items.Add(new Buff(Player.Mover, cardData.Get(BuffType.MoveSpeed)));
            Items.Add(new Buff(Player.ResourceCollector, cardData.Get(BuffType.CollectRange)));
            Items.Add(new Buff(Player.HealthRegeneration, cardData.Get(BuffType.HpRegen)));
        }
    }
}
