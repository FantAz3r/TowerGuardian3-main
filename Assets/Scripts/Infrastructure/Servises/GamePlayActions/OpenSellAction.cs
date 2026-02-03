using UnityEngine;

public class OpenSellAction : MonoBehaviour, IAction
{
    [SerializeField] private Sell _sell;
    public InteractionType GetInteractionType() => InteractionType.SellResources;

    public void Execute()
    {
        _sell.Open();
    }
}
