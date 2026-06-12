using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Items
{
    public abstract class AllItems<TItem, TConfig, TType> : MonoBehaviour
        where TItem : class, IItem<TType, TConfig>
        where TConfig : class
    {
        public event Action<TItem> Enabled;

        public event Action<TItem> Removed;

        public Player Player { get; private set; }

        public List<TItem> Items { get; private set; } = new ();

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

        protected abstract TType GetTypeFromConfig(TConfig config);

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
                        item.Disable();
                        Removed?.Invoke(item);
                    }
                }
            }
        }
    }
}
