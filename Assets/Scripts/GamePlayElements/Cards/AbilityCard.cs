public class AbilityCard : ICard
{
    private IConfig _config;
    private float _chanseToView = 0.25f;
    public AbilityCard(IConfig config)
    {
        _config = config;
    }

    public CardType Type => CardType.Ability;

    public IConfig Config => _config;
    public float ChanseToView => _chanseToView;
}
