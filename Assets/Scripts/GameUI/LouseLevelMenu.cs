using UnityEngine;
using UnityEngine.UI;

public class LouseLevelMenu : LevelMenu
{
    [SerializeField] private Button _resurrectionButton;
    private Player _player;
    private int _resurrectionCount = 1;

    public void Init(ScoreCounter scorecounter, LevelConfig levelConfig, Player player)
    {
        base.Init(scorecounter, levelConfig);
        _player = player;
    }

    public void SetResurrection()
    {
        if (_resurrectionCount > 0)
        {
            _resurrectionCount--;
            _resurrectionButton.gameObject.SetActive(true);
        }
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

    public override void Open()
    {
        base.Open();
        ScoreCounter.OnEndLevel();
    }
}
