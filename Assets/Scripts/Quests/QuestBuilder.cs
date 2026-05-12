using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuilder
{
    private List<IQuest> _quests = new();
    private Player _player;
    private List<Portal> _portals;
    private TowerDoor _door;
    private StairsTrigger _stairsTrigger;
    private QuestData _questData;

    public QuestBuilder(Player player,
        List<Portal> portals = null,
        TowerDoor door = null,
        StairsTrigger stairsTrigger = null)
    {
        _player = player;
        _stairsTrigger = stairsTrigger;
        _portals = portals;
        _door = door;
        _questData = Resources.Load<QuestData>(GameConstants.QuestData);

        CreateQuests();
    }

    private void CreateQuests()
    {
        _quests.Add(new MoveQuest());
        _quests.Add(new AttackQuest(_player.Attacker));
        _quests.Add(new CollectWoodQuest(_player.Inventory));
        _quests.Add(new CollectStonesQuest(_player.Inventory));
        _quests.Add(new UpgradeQuest(_player));
        _quests.Add(new KillQuest(_player.Detector));
        _quests.Add(new ExitLevelQuest(_portals));
        _quests.Add(new EnterTowerQuest(_door));
        _quests.Add(new UpstairsQuest(_stairsTrigger));
        _quests.Add(new EnterFirstLevelQuest(GetPortalByLevel(LevelID.Level1)));
        _quests.Add(new EnterSecondLevelQuest(GetPortalByLevel(LevelID.Level2)));
        _quests.Add(new EnterThirdLevelQuest(GetPortalByLevel(LevelID.Level3)));
        _quests.Add(new EnterFourthLevelQuest(GetPortalByLevel(LevelID.Level4)));
        _quests.Add(new EnterFinalLevelQuest(GetPortalByLevel(LevelID.Level5)));
        _quests.Add(new SwapWeaponQuest());
        _quests.Add(new DefendPortalQuest());
        _quests.Add(new SelectWeaponCardQuest());
        _quests.Add(new OutpostCaptureQuest());
        _quests.Add(new DestroyEnemyBuildingsQuest());
        _quests.Add(new StayAliveQuest());
        _quests.Add(new EnterArenaQuest());
        _quests.Add(new KillBossQuest());
        _quests.Add(new GameCompleteQuest());
    }

    private Portal GetPortalByLevel(LevelID level)
    {
        foreach (var portal in _portals)
        {
            if (portal.NextLevel == level)
            {
                return portal;
            }
        }

        return null;
    }

    public IQuest GetQuest(QuestType type)
    {
        foreach (IQuest quest in _quests)
        {
            if (quest.GetQuestType() == type)
            {
                foreach(var questinfo in _questData.QuestInfos)
                {
                    if(questinfo.Type == type)
                    {
                        quest.SetConfig(questinfo.Config);
                        return quest;
                    }    
                }
            }
        }

        throw new ArgumentNullException();
    }
}
