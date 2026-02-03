using UnityEngine;
using UnityEngine.UI;

public class LouseLevelMenu : LevelMenu
{
    [SerializeField] private Button _resurrectionButton;
    private Player _player;

    public void Init(ScoreCounter scorecounter, LevelID currentLevel, Player player)
    {
        base.Init(scorecounter, currentLevel);
        _player = player;
    }
    protected override void OnEnable()
    {
        _resurrectionButton.onClick.AddListener(Resurrection);
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _resurrectionButton.onClick.RemoveListener(Resurrection);
    }

    private void Resurrection()
    {
        _player.Health.Heal(_player.Health.MaxHealth);
        base.Close();
    }
}
