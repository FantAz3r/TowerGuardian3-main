using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LouseLevelMenu : LevelMenu
{
    private float ImmunityTime = 5f;

    [SerializeField] private Button _resurrectionButton;
    [SerializeField] private AudioClip _louseSound;

    private string _rewardId = "HealPlayer";
    private Player _player;
    private IADVServise _advService;
    private IInputService _inputService;
    private bool _canResurrection = true;

    protected override void Awake()
    {
        _inputService = ServiceLocator.Get<IInputService>();
        _advService = ServiceLocator.Get<IADVServise>();
        base.Awake();
        _player = GameFactory.Player;
        _resurrectionButton.gameObject.SetActive(false);
    }

    public void SetResurrection()
    {
        bool show = _canResurrection && _advService.CanShowRewardADV(_rewardId);
        _canResurrection = show == false;
        SetResurrectionButtonActive(show);
    }

    private void SetResurrectionButtonActive(bool active)
    {
        _resurrectionButton.gameObject.SetActive(active);

        if (active)
        {
            _resurrectionButton.transform.localScale = Vector3.one;
            _resurrectionButton.transform.DOScale(1.1f, 0.6f).
                SetLoops(-1, LoopType.Yoyo).
                SetEase(Ease.InOutSine).
                SetUpdate(true);
        }
        else
        {
            _resurrectionButton.transform.localScale = Vector3.one;
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
        _resurrectionButton.transform.DOKill();
    }

    private void Resurrection()
    {
        Close();
        WindowService.Open(WindowType.HUD);

        _advService.TryShowRewardADV(_rewardId, () =>
        {
            _player.Health.Resurect();
            _player.PlayerAnimator.OnRevive();
            _inputService.EnableInput();
            _player.Health.ImmunityPerTime(ImmunityTime);
        });
    }

    public override void Open()
    {
        base.Open();
        ScoreCounter.OnEndLevel(this);
        ServiceLocator.Get<ISpawnerService>().SendSoundReqest(_louseSound);
    }
}
