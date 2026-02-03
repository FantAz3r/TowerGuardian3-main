using UnityEngine;

public class UIDummy : MonoBehaviour, IUIWindow
{
    public bool IsActive => gameObject.activeSelf;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
