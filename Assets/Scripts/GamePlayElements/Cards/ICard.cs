public interface ICard 
{
    CardType Type { get; }
    IConfig Config { get; }  
    float ChanseToView { get; }
}
