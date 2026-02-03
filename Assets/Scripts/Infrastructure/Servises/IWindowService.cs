using UnityEngine;

public interface IWindowService : IService
{
    WindowBase Open(WindowType type, GameObject payload1 = null);
    void CreateUIRoot();
}
