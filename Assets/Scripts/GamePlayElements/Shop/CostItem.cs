using TMPro;
using UnityEngine;

public class CostItem : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private TMP_Text priceText; 

    public void Setup( int price)
    {
        priceText.text = price.ToString();
    }
}
