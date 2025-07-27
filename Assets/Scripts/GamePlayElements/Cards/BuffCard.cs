public class BuffCard : ICard
{
    private IConfig _config;
    private float _chanseToView = 0.30f;
    public BuffCard(IConfig config)
    {
        _config = config;
    }

    public CardType Type => CardType.Buff;

    public IConfig Config => _config;

    public float ChanseToView => _chanseToView;
}
