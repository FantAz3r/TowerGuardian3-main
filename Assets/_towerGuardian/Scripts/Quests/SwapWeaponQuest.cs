using TowerGuardian.Enums;
using TowerGuardian.Factories;
using TowerGuardian.Infrastructure;

public class SwapWeaponQuest : Quest
{
    private WeaponPanel _panel;
    public override QuestType GetQuestType() => QuestType.SwapWeapon;

    public override void Run()
    {
        _panel = ServiceLocator.Get<IUIFactory>().HUD.WeaponPanel;
        base.Run();
        _panel.WeaponSwaped += Complete;
        _panel.Highlighter.ActivateWarning();
    }

    public override void Complete()
    {
        _panel.WeaponSwaped -= Complete;
        _panel.Highlighter.DeactivateWarning();
        base.Complete();
    }

    public override void Stop()
    {
        base.Stop();
        _panel.WeaponSwaped -= Complete;
        _panel.Highlighter.DeactivateWarning();
    }
}
