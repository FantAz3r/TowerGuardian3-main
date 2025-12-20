using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class SellResourceView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private List<CostView> _costs;

    private CounterSlider _slider;
    private Button _button;
    private PieceConfig _config;
    private int _maxValue;

    public event Action<SellResourceView, PieceConfig, int> SellRequested;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Init(CounterSlider slider)
    {
        _slider = slider;
    }

    public void Render(PieceConfig config, bool interactable, int maxValue)
    {
        _config = config;
        _maxValue = maxValue;
        _count.text = maxValue.ToString();
        _image.sprite = _config.Icon;
        _name.text = _config.Name ?? string.Empty;
        _description.text = _config.Description ?? string.Empty;

        for (int i = 0; i < _costs.Count; i++)
        {
            if (i < _config.GetSellCost().Count)
            {
                _costs[i].gameObject.SetActive(true);
                _costs[i].Render(_config.GetSellCost()[i]);
            }
            else
            {
                _costs[i].gameObject.SetActive(false);
            }
        }

        _button.interactable = interactable;
    }

    private void OnClick()
    {
        if (_config == null)
            return;

        _slider.Init(_maxValue, _config);
        _slider.Open();
        _slider.Ñonfirmed += OnByeReqest;
    }

    public void OnByeReqest(int count)
    {
        SellRequested?.Invoke(this, _config, count);
        _slider.Close();
        _slider.Ñonfirmed -= OnByeReqest;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
