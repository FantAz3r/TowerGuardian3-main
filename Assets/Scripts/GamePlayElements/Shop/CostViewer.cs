using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostViewer : MonoBehaviour
{
    [SerializeField] private List<CostItem> _costs = new List<CostItem>();
    private Platform _platform;

    private void Awake()
    {
        _platform = GetComponent<Platform>();
    }

    private void View()
    {
       
    }
}
