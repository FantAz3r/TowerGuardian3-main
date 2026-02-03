using UnityEngine;

public class HUD : WindowBase
{
    [field: SerializeField] public AbilityPanel AbilityPanel { get; private set; }
    [field: SerializeField] public ResourceViewer ResourceViewer { get; private set; }
    [field: SerializeField] public PlayerHealthViewer PlayerHealthViewer { get; private set; }
    [field: SerializeField] public LevelViewer PlayerLevelViewer { get; private set; }
    [field: SerializeField] public WeaponPanel WeaponPanel { get; private set; }
    [field: SerializeField] public Clock Clock { get; private set; }

    public void Init(
         Player player,
         DayCycle cycle)
    {
        ResourceViewer.Init(player.Inventory);
        PlayerHealthViewer.Init(player.Health);
        PlayerLevelViewer.Init(player.Experience);
        AbilityPanel.Init(player);
        WeaponPanel.Init(player);
        Clock.Init(cycle);
    }

    public override void Open()
    {
        base.Open();
        ServiceLocator.Get<ITimeService>().Resume();
    }
}
