public class UpgradeQuest : Quest
{
    private Player _player;

    public UpgradeQuest(Player player) => _player = player;

    public override QuestType GetQuestType() => QuestType.Upgrade;

    public override void Run()
    {
        base.Run();
        _player.CardHolder.CardAdded += Complete;
        _player.CardHolder.Upgraded += Complete;
    }

    public override void Complete()
    {
        base.Complete();
        _player.CardHolder.CardAdded -= Complete;
        _player.CardHolder.Upgraded -= Complete;
    }

    public override void Stop()
    {
        base.Stop();
        _player.CardHolder.CardAdded -= Complete;
        _player.CardHolder.Upgraded -= Complete;
    }

    private void Complete(ICardConfig useles)
    {
        Complete();
    }
}
