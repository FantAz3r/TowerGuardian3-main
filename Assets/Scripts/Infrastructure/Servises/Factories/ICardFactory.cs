public interface ICardFactory
{
    CardType Type { get; }
    void ActivateCard(IConfig config);

}