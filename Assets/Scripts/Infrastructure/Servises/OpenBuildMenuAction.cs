using UnityEngine;
internal class OpenBuildMenuAction : MonoBehaviour, IAction
{
    private InteractionType _interactionType = InteractionType.OpenBuildMenu;

    public InteractionType GetInteractionType() => _interactionType;

    public void Execute()
    {
        gameObject.SetActive(true);
        Debug.Log("Открываем меню строительства");
    }
}