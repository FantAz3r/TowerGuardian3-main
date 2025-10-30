using System;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _configs;

    private List<IQuest> _quests = new List<IQuest>();
    private int _currentQuestIndex = -1;
    private bool _isTutoeialComplite = false;


    private void Awake()
    {
        foreach (var config in _configs)
        {
            if (config is IQuest)
            {
                _quests.Add(config as IQuest);
            }
        }
    }

    private void RunTutorial()
    {
        _quests[_currentQuestIndex].Run();
    }
}
    