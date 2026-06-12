using System.Collections.Generic;
using System.Linq;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Factories
{
    public class AbilityFactory : ICardFactory
    {
        private Player _player;

        public AbilityFactory(Player player) => _player = player;

        public CardType Type => CardType.Ability;

        public void Create(ICardConfig config)
        {
            List<Ability> abilities = _player.AllAbilities.transform.GetComponentsInChildren<Ability>().ToList();

            foreach (var ability in abilities)
            {
                if (config.ID == ability.Config.ID)
                {
                    return;
                }
            }

            if (config is AbilityConfig abilityConfig)
            {
                IAbility ability = Object.Instantiate(abilityConfig.Prefab, _player.AllAbilities.transform);
                _player.AllAbilities.AddItem(ability);
            }
        }
    }
}