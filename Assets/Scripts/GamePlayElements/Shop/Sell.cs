using System.Collections.Generic;
using UnityEngine;
using YG;

public class Sell : BaseShop
{
    [SerializeField] private SellResources _resourcesPanel;

    public override void Init(Player player)
    {
        base.Init(player);
        _resourcesPanel.Init(player.Inventory);
    }

    public override void Open()
    {
        base.Open();

        LoadContent();
        _resourcesPanel.gameObject.SetActive(true);
        _resourcesPanel.RenderSellItems();
    }

    private void LoadContent()
    {
        ClearOldButtons();
        LoadCards();
        SetParents();
    }

    protected override void OnTradeRequested(ProductViewer button, ICardConfig config)
    {
        List<CostInfo> sellPrice = config.GetSellCosts();
        Player.Inventory.AddResousres(sellPrice);

        if (config is ICardConfig card)
        {
            Configs.Remove(card);
            Player.CardHolder.Remove(card);

            UpdateCardSave(card);
            Player.Experience.AddUpgradePoints(card.Level);
        }

        ClearOldButtons();
        LoadContent();
    }

    protected override void LoadCards()
    {
        if (YG2.saves.AllCards == null)
            return;

        foreach (var card in CardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
            card.InitFromData(cardData);

            if (card.HasPlayer)
            {
                Configs.Add(card);
            }
        }
    }

    protected override void OnParentFounded(RectTransform parent, ICardConfig config)
    {
        var button = CreateButton(parent);
        button.Render(config, true);
    }
}

