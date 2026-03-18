using UnityEngine;

public class HUD : WindowBase
{
    [field: SerializeField] public AbilityPanel AbilityPanel { get; private set; }
    [field: SerializeField] public ResourceViewer ResourceViewer { get; private set; }
    [field: SerializeField] public PlayerHealthViewer PlayerHealthViewer { get; private set; }
    [field: SerializeField] public LevelViewer PlayerLevelViewer { get; private set; }
    [field: SerializeField] public WeaponPanel WeaponPanel { get; private set; }
    [field: SerializeField] public Clock Clock { get; private set; }

    private bool IsActive = true;
    public override void Open()
    {
        base.Open();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(IsActive == false);
            }

            IsActive = IsActive == false;
        }
    }
}
