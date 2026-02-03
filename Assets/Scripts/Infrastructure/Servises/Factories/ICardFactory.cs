public interface ICardFactory
{
    CardType Type { get; }
    void Create(ICardConfig config);
}