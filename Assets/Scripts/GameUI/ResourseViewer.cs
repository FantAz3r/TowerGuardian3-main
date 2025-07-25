using System.Collections.Generic;
using UnityEngine;

public class ResourceViewer : MonoBehaviour
{
    [SerializeField] private List<ResourcePieceView> _resourceAmount;
    private Inventory _inventory;

    public void Init(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.ResourceChanged += OnResourceChanged;
    }

    private void OnDestroy()
    {
        _inventory.ResourceChanged -= OnResourceChanged;
    }

    private void OnResourceChanged(Dictionary<ResourceType, int> resourses)
    {
        foreach (var text in _resourceAmount)
        {
            if (resourses.ContainsKey(text.TextType))
            {
                text.SetText(resourses[text.TextType].ToString());
            }
        }
    }
}

