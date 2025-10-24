using UnityEngine;
public class OpenShopAction : MonoBehaviour, IAction
{
    public InteractionType GetInteractionType()  => InteractionType.OpenShop;

    public void Execute()
    {
        gameObject.SetActive(true);
    }
}