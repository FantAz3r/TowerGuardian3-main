using UnityEngine;

public class OpenShopAction : MonoBehaviour, IAction
{
    private Shop _shop;
    private ITimeService _timeService;
    public InteractionType GetInteractionType()  => InteractionType.OpenShop;

    private void Awake()
    {
        _shop = GetComponent<Shop>();
        _timeService = ServicesLocator.GetService<ITimeService>();
    }

    public void Execute()
    {
        _timeService.PauseGame();
        _shop.OnActivate();
    }
}