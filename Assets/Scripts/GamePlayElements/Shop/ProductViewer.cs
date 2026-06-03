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
    [SerializeField] private TMP_Text _level;
    [SerializeField] private List<CostView> _costs;
    [SerializeField] private Button _button;

    private ICardConfig _config;

    public event Action<ProductViewer, ICardConfig> BuyRequested;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void Render(ICardConfig config, bool isBuy, bool interactable = true)
    {
        _config = config;
        _image.sprite = config.Icon;
        _name.text = config.Name ?? string.Empty;
        _description.text = config.Description ?? string.Empty;


        if (_config is CardConfig card && _config.Level < _config.MaxCardLevel)
        {
            _level.text = $"{UIText.LVL} {card.Level.ToString()}";
        }
        else
        {
            _level.text = $"{UIText.MaxLevel}";
        }

        for (int i = 0; i < _costs.Count; i++)
        {
            if (isBuy)
            {
                if (_config.Level < _config.MaxCardLevel)
                {
                    if (i < _config.GetCosts().Count)
                    {
                        _costs[i].gameObject.SetActive(true);
                        _costs[i].Render(_config.GetCosts()[i]);
                    }
                    else
                    {
                        _costs[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    _costs[i].gameObject.SetActive(false);
                }
            }
            else
            {
                if (i < _config.GetSellCosts().Count)
                {
                    _costs[i].gameObject.SetActive(true);
                    _costs[i].Render(_config.GetSellCosts()[i]);
                }
                else
                {
                    _costs[i].gameObject.SetActive(false);
                }
            }
        }

        _button.interactable = interactable;
    }

    private void OnClick()
    {
        if (_config == null) return;

        BuyRequested?.Invoke(this, _config);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}