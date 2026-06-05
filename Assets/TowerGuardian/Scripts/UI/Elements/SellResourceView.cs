using System;
using System.Collections.Generic;
using TMPro;
using TowerGuardian.Scripts.GamePlayElements.Shop;
using TowerGuardian.Scripts.StaticData.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class SellResourceView : MonoBehaviour
    {
        [SerializeField] private Image _sellImage;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private List<CostView> _costs;
        [SerializeField] private Button _button;

        private CounterSlider _slider;
        private PieceConfig _config;
        private int _maxValue;

        public event Action<SellResourceView, PieceConfig, int> SellRequested;

        private void Awake()
        {
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

            _sellImage.sprite = _config.Icon;
            _name.text = _config.Name ?? string.Empty;
            _description.text = _config.Description ?? string.Empty;

            for (int i = 0; i < _costs.Count; i++)
            {
                if (i < _config.GetSellCosts().Count)
                {
                    _costs[i].gameObject.SetActive(true);
                    _costs[i].Render(_config.GetCosts()[i]);
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
            _slider.Confirmed += OnByeReqest;
        }

        public void OnByeReqest(int count)
        {
            SellRequested?.Invoke(this, _config, count);
            _slider.Close();
            _slider.Confirmed -= OnByeReqest;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}