using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using TowerGuardian.Scripts.StaticData.Structs.SaveData;
using TowerGuardian.Scripts.UI.Elements;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.Shop
{
    public class SaleWindow : BaseShop
    {
        [SerializeField]
        private SellResources _resourcesPanel;

        public override void Open()
        {
            base.Open();

            LoadContent();
            _resourcesPanel.gameObject.SetActive(true);
            _resourcesPanel.RenderSellItems();
        }

        protected override void OnTradeRequested(ProductViewer button, ICardConfig config)
        {
            Player.Inventory.AddResousres(config.GetSellCosts());

            if (config is ICardConfig card)
            {
                if (card.Level > 1)
                {
                    card.Regrade();
                }
                else
                {
                    card.Regrade();
                    card.SetBought(false);
                    card.SetHasPlayer(false);

                    Player.CardHolder.Remove(card);
                    Configs.Remove(card);
                }

                UpdateCardSave(card);
            }

            ClearOldButtons();
            LoadContent();
        }

        protected override void LoadCards()
        {
            if (YG2.saves.AllCards == null)
            {
                return;
            }

            foreach (var card in CardData.GetConfigs())
            {
                CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
                card.InitFromData(cardData);

                if (card.IsBought)
                {
                    Configs.Add(card);
                }
            }
        }

        protected override void OnParentFounded(RectTransform parent, ICardConfig config)
        {
            var button = CreateButton(parent);
            button.Render(config, false);
        }

        private void LoadContent()
        {
            ClearOldButtons();
            LoadCards();
            SetParents();
        }
    }
}