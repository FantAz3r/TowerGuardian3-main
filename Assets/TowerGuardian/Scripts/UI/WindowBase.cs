using UnityEngine;

namespace TowerGuardian.Scripts.UI
{
    public class WindowBase : MonoBehaviour, IUIWindow
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}