using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class StatsButton : MonoBehaviour
    {
        private UIItem _item;
        private InventoryStats _stats;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _item = GetComponent<UIItem>();
        }

        public void Init(InventoryStats stats)
        {
            _stats = stats;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void OnClick()
        {
            _stats.View(_item.SlotConfig);
        }
    }
}