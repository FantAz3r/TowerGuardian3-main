using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.Infrastructure
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void Register<TService>(TService service)
            where TService : IService
        {
            var type = typeof(TService);
            if (!_services.ContainsKey(type))
                _services[type] = service;
        }

        public static TService Get<TService>()
            where TService : IService
        {
            var type = typeof(TService);

            if (_services.TryGetValue(type, out IService service))
                return (TService)service;

            throw new InvalidOperationException($"No {type} service");
        }

        public static bool Remove<TService>()
            where TService : IService
        {
            var type = typeof(TService);
            return _services.Remove(type);
        }
    }
}