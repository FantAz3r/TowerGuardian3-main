using UnityEngine;
using YG;

public class OpenSellAction : MonoBehaviour, IAction
{
    private Sell _sell;
    public InteractionType GetInteractionType() => InteractionType.SellResources;

    private void Awake()
    {
        _sell = GetComponent<Sell>();
    }

    public void Execute()
    {
        YG2.PauseGameNoEditEventSystem(true);
        _sell.OnActivate();
    }
}
