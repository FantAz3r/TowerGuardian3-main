using System;
using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using TowerGuardian.Scripts.StaticData.Datas;
using TowerGuardian.Scripts.StaticData.Structs.SaveData;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    public class PlayerCardConfigContainer : MonoBehaviour
    {
        [SerializeField] private CardData _cardData;
        [SerializeField] private List<CardConfig> _startCards;
        [SerializeField] private Player _player;

        private Dictionary<CardType, ICardFactory> _factories;
        private List<ICardConfig> _selectedConfigs = new();

        public event Action<ICardConfig> CardAdded;
        public event Action<ICardConfig> CardRemoved;
        public event Action Upgraded;

        [field: SerializeField] public int MaxWeaponCards { get; private set; } = 4;
        [field: SerializeField] public int MaxAbilityCards { get; private set; } = 4;
        public IReadOnlyList<ICardConfig> SelectedCardConfigs => _selectedConfigs;

        private void Awake()
        {
            _factories = new Dictionary<CardType, ICardFactory>
            {
                {CardType.Weapon, new WeaponFactory (_player) },
                {CardType.Ability, new AbilityFactory (_player) },
            };
        }

        private void Start()
        {
            LoadPlayerCards();

            foreach (var card in _startCards)
            {
                card.SetBought(true);
            }

            _player.Attacker.LoadCurrentWeapon();
        }

        public void Add(ICardConfig config)
        {
            LoadCard(config);

            if (!_selectedConfigs.Contains(config))
            {
                AddCard(config);
            }

            if (config.IsBought)
            {
                config.Upgrade();
                Upgraded?.Invoke();
            }

            UpdateCardSave(config);
        }

        public void Remove(ICardConfig config)
        {
            config.SetBought(false);
            SaveInInventory(config);
        }

        public void SaveInInventory(ICardConfig config)
        {
            _selectedConfigs.Remove(config);
            config.SetHasPlayer(false);
            CardRemoved?.Invoke(config);
            UpdateCardSave(config);
        }

        private void Create(ICardConfig card)
        {
            if (_factories != null && _factories.TryGetValue(card.GetCardType(), out ICardFactory factory))
            {
                factory.Create(card);
            }
        }

        private void UpdateCardSave(ICardConfig card)
        {
            if (YG2.saves.AllCards == null)
                YG2.saves.AllCards = new();

            YG2.saves.AllCards.RemoveAll(savedCard => savedCard.ID == card.ID);
            YG2.saves.AllCards.Add(new CardSaveData(card.Level, card.ID, card.IsBought, card.HasPlayer));
            YG2.SaveProgress();
        }

        private void LoadPlayerCards()
        {
            if (YG2.saves.AllCards == null)
                return;

            foreach (var card in _cardData.GetConfigs())
            {
                CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
                card.InitFromData(cardData);

                if (card.HasPlayer)
                {
                    AddCard(card);
                }
            }
        }

        private void LoadCard(ICardConfig card)
        {
            if (YG2.saves.AllCards == null)
                return;

            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);

            if (!string.IsNullOrEmpty(cardData.ID))
            {
                card.InitFromData(cardData);
            }
        }

        public void AddCard(ICardConfig card)
        {
            LoadCard(card);

            int weaponCount = 0;
            int abilityCount = 0;

            foreach (var item in _selectedConfigs)
            {
                if (item is WeaponConfig && item.HasPlayer)
                    weaponCount++;

                if (item is AbilityConfig && item.HasPlayer)
                    abilityCount++;
            }

            _selectedConfigs.Add(card);

            if (card is WeaponConfig)
            {
                if (weaponCount < MaxWeaponCards)
                {
                    card.SetHasPlayer(true);
                    ActivateCard(card);
                }
                else
                {
                    card.SetHasPlayer(false);
                }
            }
            else if (card is AbilityConfig)
            {
                if (abilityCount < MaxAbilityCards)
                {
                    card.SetHasPlayer(true);
                    ActivateCard(card);
                }
                else
                {
                    card.SetHasPlayer(false);
                }
            }
            else
            {
                card.SetHasPlayer(true);
                ActivateCard(card);
            }

            UpdateCardSave(card);
        }

        private void ActivateCard(ICardConfig card)
        {
            Create(card);
            CardAdded?.Invoke(card);
        }
    }
}
