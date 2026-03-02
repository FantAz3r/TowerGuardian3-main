using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AllItems<TItem, TConfig, TType> : MonoBehaviour
    where TItem : class, IItem<TType, TConfig>
    where TConfig : class
{
    public Player Player { get; private set; }
    public List<TItem> Items { get; private set; } = new();

    public event Action<TItem> Enabled, Removed;

    protected virtual void Awake()
    {
        Player = GetComponentInParent<Player>();
    }

    private void OnEnable()
    {
        Player.CardHolder.CardAdded += OnActivate;
        Player.CardHolder.CardRemoved += OnRemove;
    }

    private void OnDisable()
    {
        Player.CardHolder.CardAdded -= OnActivate;
        Player.CardHolder.CardRemoved -= OnRemove;
    }

    public void AddItem(TItem item)
    {
        Items.Add(item);
    }

    private void OnActivate(ICardConfig card)
    {
        if (card is TConfig config)
        {
            foreach (var item in Items)
            {
                if (Equals(item.Type, GetTypeFromConfig(config)))
                {
                    item.Enable();
                    Enabled?.Invoke(item);
                    Debug.Log(card.Name + " Enabled?");
                }
            }
        }
    }

    private void OnRemove(ICardConfig card)
    {
        if (card is TConfig config)
        {
            foreach (var item in Items)
            {
                if (Equals(item.Type, GetTypeFromConfig(config)))
                {
                    item.Remove();
                    Removed?.Invoke(item);
                }
            }
        }
    }

    protected abstract TType GetTypeFromConfig(TConfig config);
}
