public class WeaponCard : ICard
{
    private IConfig _config;
    private float _chanseToView = 0.45f;
    public WeaponCard(IConfig config )
    {
        _config = config;
    }

    public CardType Type => CardType.WeaponSetter;

    public IConfig Config => _config;
    public float ChanseToView => _chanseToView;
}
