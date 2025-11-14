using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductViewer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private List<CostView> _costs;

    Button _button;
    private IShopConfig _config;
    private bool _isBuyed;

    public event Action<ProductViewer, IShopConfig> BuyRequested;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Render(IShopConfig config, bool interactable = true)
    {
        _image.sprite = config.Icon;
        _config = config;
        _name.text = config.Name ?? string.Empty;
        _description.text = config.Description ?? string.Empty;

        for (int i = 0; i < _costs.Count; i++)
        {
            if(i < config.GetCosts().Count)
            {
                _costs[i].gameObject.SetActive(true);
                _costs[i].Render(config.GetCosts()[i]);
            }
            else
            {
                _costs[i].gameObject.SetActive(false);
            }
        }

        _button.interactable = interactable;

        if (_isBuyed)
        {
            _button.interactable = false;
        }
    }

    private void OnClick()
    {
        _isBuyed = true;
        if (_config == null) return;
        BuyRequested?.Invoke(this, _config);
        _button.onClick.RemoveListener(OnClick);
    }
}
