public interface IItem<T, TConfig> where TConfig : class
{
    TConfig Config { get;}
    T Type { get; }

    void Enable();
    void Remove();
}

