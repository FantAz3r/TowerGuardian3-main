using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityFactory : ICardFactory
{
    private Player _player;
    public CardType Type => CardType.Ability;
    public AbilityFactory(Player player) => _player = player;

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
