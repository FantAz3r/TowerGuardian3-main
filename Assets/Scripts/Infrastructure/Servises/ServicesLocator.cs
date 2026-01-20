using System.Collections.Generic;
using System;

public static class ServicesLocator
{
    private static Dictionary<System.Type, IService> _services = new Dictionary<System.Type, IService>();

    public static void Register<TService>(TService service) where TService : IService
    {
        var type = typeof(TService);
        if (_services.ContainsKey(type) == false)
            _services[type] = service;
    }

    public static TService GetService<TService>() where TService : IService
    {
        var type = typeof(TService);

        if (_services.TryGetValue(type, out IService service))
            return (TService)service;

        throw new InvalidOperationException($"Сервис {type} не зарегистрирован");
    }
}
