using System.Collections.Generic;
using System;

public class AllServices
{
    private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    public void Register<TService>(TService service) where TService : IService
    {
        var type = typeof(TService);
        if (_services.ContainsKey(type) == false)
            _services[type] = service;
    }

    public TService GetService<TService>() where TService : IService
    {
        var type = typeof(TService);

        if (_services.TryGetValue(type, out IService service))
            return (TService)service;

        throw new InvalidOperationException($"Сервис {type} не зарегистрирован");
    }
}
