using System;

public interface IAbilityInput : IService
{
    event Action OnAbillity1Used;
    event Action OnAbillity2Used;
    event Action OnAbillity3Used;
    event Action OnAbillity4Used;
}
