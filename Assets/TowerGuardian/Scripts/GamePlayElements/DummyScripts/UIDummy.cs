using TowerGuardian.Scripts.UI;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.DummyScripts
{
    public class UIDummy : MonoBehaviour, IUIWindow
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
