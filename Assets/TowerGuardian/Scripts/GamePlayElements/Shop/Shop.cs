using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using TowerGuardian.Scripts.StaticData.Structs.SaveData;
using TowerGuardian.Scripts.UI.Elements;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Shop
{
    public class Shop : BaseShop
    {
        public override void Open()
        {
            base.Open();
            LoadContent();
            RenderAll();
            WeaponContentParent.gameObject.SetActive(true);
        }

        protected override void OnTradeRequested(ProductViewer button, ICardConfig config)
        {
            if (config.Level >= config.MaxCardLevel)
            {
                return;
            }

            if (!CanAfford(config))
            {
                return;
            }

            Player.Inventory?.SpendResource(config.GetCosts());

            UpdateCardSave(config);
            Player.CardHolder.Add(config);

            ClearOldButtons();
            LoadContent();
            RenderAll();
        }

        protected override CardSaveData CreateSaveData(ICardConfig card)
        {
            return card.CreateSaveData(true);
        }

        protected override void OnNoSaveData()
        {
            foreach (var card in CardData.GetConfigs())
            {
                CardSaveData cardData = new CardSaveData(0, card.ID);
                card.InitFromData(cardData);
            }
        }

        protected override void OnParentFounded(RectTransform parent, ICardConfig config)
        {
            CreateButton(parent);
        }

        private void LoadContent()
        {
            foreach (var config in CardData.GetConfigs())
            {
                Configs.Add(config);
            }

            LoadCards();
            SetParents();
        }

        private void RenderAll()
        {
            for (int i = 0; i < Configs.Count; i++)
            {
                bool canBuy = CanAfford(Configs[i]);
                ProductButtons[i].gameObject.SetActive(true);
                ProductButtons[i].Render(Configs[i], true, canBuy);
            }
        }

        private bool CanAfford(IShopConfig config)
        {
            if (Player.Inventory == null)
            {
                return true;
            }

            return Player.Inventory.IsEnoughResource(config.GetCosts());
        }
    }
}