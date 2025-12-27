using UnityEngine;
using YG;

public class OpenShopAction : MonoBehaviour, IAction
{
    private Shop _shop;
    public InteractionType GetInteractionType()  => InteractionType.OpenShop;

    private void Awake()
    {
        _shop = GetComponent<Shop>();
    }

    public void Execute()
    {
        YG2.PauseGameNoEditEventSystem(true);
        _shop.OnActivate();
    }
}