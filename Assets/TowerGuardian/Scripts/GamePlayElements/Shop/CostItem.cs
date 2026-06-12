using TMPro;
using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Shop
{
    public class CostItem : MonoBehaviour
    {
        [SerializeField]
        private ResourceType _resourceType;
        [SerializeField]
        private TMP_Text _priceText;

        public void Setup(int price)
        {
            _priceText.text = price.ToString();
        }
    }
}
