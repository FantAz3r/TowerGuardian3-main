using System.Collections.Generic;
using UnityEngine;

public class ResourceViewer : MonoBehaviour
{
    [SerializeField] private List<ResourcePieceView> _resourceAmount;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = ServiceLocator.Get<IGameFactory>().Player.Inventory;
        OnResourceChanged(_inventory.Resources);

        _inventory.ResourceChanged -= OnResourceChanged;
        _inventory.ResourceChanged += OnResourceChanged;
    }


    private void OnDestroy()
    {
        _inventory.ResourceChanged -= OnResourceChanged;
    }

    private void OnEnable()
    {
        OnResourceChanged(_inventory.Resources);
    }

    private void OnResourceChanged(Dictionary<ResourceType, int> resourses)
    {
        foreach (var text in _resourceAmount)
        {
            if (resourses.ContainsKey(text.TextType))
            {
                if (resourses[text.TextType] <= 0)
                {
                    text.SetText(0.ToString());
                }
                else 
                {
                    text.SetText(resourses[text.TextType].ToString());
                }
            }
        }
    }
}

