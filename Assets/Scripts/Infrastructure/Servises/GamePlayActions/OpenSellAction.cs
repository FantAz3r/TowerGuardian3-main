using UnityEngine;

public class OpenSellAction : MonoBehaviour, IAction
{
    private Sell _sell;
    private ITimeService _timeService;
    public InteractionType GetInteractionType() => InteractionType.SellResources;

    private void Awake()
    {
        _sell = GetComponent<Sell>();
        _timeService = ServicesLocator.GetService<ITimeService>();
    }

    public void Execute()
    {
        _timeService.PauseGame();
        _sell.OnActivate();
    }
}
