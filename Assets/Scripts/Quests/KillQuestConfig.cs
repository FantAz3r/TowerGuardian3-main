using UnityEngine;

public class KillQuestConfig : QuestConfig
{
    [SerializeField] private int _count;
    [SerializeField] private EntityType _entityType;

    public int Count => _count;
    public EntityType Type => _entityType;
}
