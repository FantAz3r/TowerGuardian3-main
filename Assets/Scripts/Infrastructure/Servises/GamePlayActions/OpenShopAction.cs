using UnityEngine;

public class OpenShopAction : MonoBehaviour, IAction
{
    [SerializeField] private Shop _shop;
    public InteractionType GetInteractionType()  => InteractionType.OpenShop;

    public void Execute()
    {
        _shop.Open();
    }
}