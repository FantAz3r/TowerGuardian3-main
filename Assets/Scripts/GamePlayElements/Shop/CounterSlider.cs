using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class CounterSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;      
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private TMP_Text _resourceAdded;
    [SerializeField] private Image _resourceSprite;
    [SerializeField] private Button _confirmButton;

    private PieceConfig _config;
    private int _count;

    public event Action<int> Ñonfirmed;

    public void Init(int maxValue, PieceConfig config)
    {
        _config = config;
        _slider.maxValue = maxValue;

        _slider.minValue = 1;
        _slider.wholeNumbers = true;
        _slider.value = 1;

        gameObject.SetActive(true);
        _slider.onValueChanged.AddListener(OnSliderChanged);
        _confirmButton.onClick.AddListener(OnClick);
        UpdateQuantityText();
        gameObject.SetActive(false);

    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnSliderChanged(float value)
    {
        _count = (int)value;
        UpdateQuantityText();
    }

    private void UpdateQuantityText()
    {
        _resourceSprite.sprite = _config.Costs.First().Image;
        _resourceAdded.text = (_count * _config.Costs.First().Value).ToString(); 
        _quantityText.text = _count.ToString();
    }

    private void OnClick()
    {
        Ñonfirmed?.Invoke(_count);
        Close();
    }
}

