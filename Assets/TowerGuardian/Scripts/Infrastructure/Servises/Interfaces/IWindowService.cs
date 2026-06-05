using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.UI;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface IWindowService : IService
    {
        WindowBase Open(WindowType type, GameObject payload1 = null);
        WindowBase OpenPreviousWindow();
        void CreateUIRoot();
        void CreateJoystick();
    }
}
