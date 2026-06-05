using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.CardsInfrastructure;
using TowerGuardian.Scripts.GamePlayElements.MainMenu;
using TowerGuardian.Scripts.GamePlayElements.Shop;
using TowerGuardian.Scripts.GamePlayElements.Sounds;
using TowerGuardian.Scripts.UI.Elements;
using TowerGuardian.Scripts.UI.Windows;
using UnityEngine;

namespace TowerGuardian.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [field: SerializeField] public Shop Shop { get; private set; }
        [field: SerializeField] public SaleWindow Sell { get; private set; }
        [field: SerializeField] public LouseLevelMenu LouseLevelMenu { get; private set; }
        [field: SerializeField] public ScoreViewer LouseScoreViewer { get; private set; }
        [field: SerializeField] public StartLevelMenu StartLevelMenu { get; private set; }
        [field: SerializeField] public ScoreViewer StartScoreViewer { get; private set; }
        [field: SerializeField] public WinLevelMenu WinLevelMenu { get; private set; }
        [field: SerializeField] public ScoreViewer WinScoreViewer { get; private set; }
        [field: SerializeField] public HUD HUD { get; private set; }
        [field: SerializeField] public QuestViewer QuestViewer { get; private set; }
        [field: SerializeField] public PauseUI PauseUI { get; private set; }
        [field: SerializeField] public CardSelectionMenu CardSelectionMenu { get; private set; }
        [field: SerializeField] public ShowCardsButton ShowCardsButton { get; private set; }
        [field: SerializeField] public List<CardButton> CardButtons { get; private set; }
        [field: SerializeField] public Settings Settings { get; private set; }
        [field: SerializeField] public SwichDamageNumbers SwichDamageNumbers { get; private set; }
        [field: SerializeField] public Mute Mute { get; private set; }
    }
}