public interface ICardFactory
{
    CardType Type { get; }
    void ActivateCard(ICardConfig config);
}