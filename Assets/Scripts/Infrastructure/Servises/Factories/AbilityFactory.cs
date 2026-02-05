using UnityEngine;

public class AbilityFactory : ICardFactory
{
    private Player _player;
    public CardType Type => CardType.Ability;
    public AbilityFactory(Player player) => _player = player;

    public void Create(ICardConfig config)
    {
        if (config is AbilityConfig abilityConfig)
        {
            IAbility ability = Object.Instantiate(abilityConfig.Prefab, _player.AllAbilities.transform);
            _player.AllAbilities.AddItem(ability);
        }
    }
}
