using TowerGuardian.Enums;
using UnityEngine;
public interface IWindowService : IService
{
    WindowBase Open(WindowType type, GameObject payload1 = null);
    WindowBase OpenPreviousWindow();
    void CreateUIRoot();
    void CreateJoystick();
}
