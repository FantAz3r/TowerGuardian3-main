using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardsButton : WindowBase
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _showButton;

    private Player _player;
    private IWindowService _windowService;

    public void Init(Player player)
    {
        _player = player;
        _windowService = ServiceLocator.Get<IWindowService>();

        _player.Experience.OnUpgradePointAdded += Open;
        _player.Experience.OnUpgradePointRemoved += Close;
        Open();
    }

    private void OnEnable()
    {
        _showButton.onClick.AddListener(OnShowButtonClicked);
    }

    private void OnDisable()
    {
        _showButton.onClick.RemoveListener(OnShowButtonClicked);
    }

    private void OnDestroy()
    {
        _player.Experience.OnUpgradePointRemoved -= Close;
        _player.Experience.OnUpgradePointAdded -= Open;
    }

    public override void Open()
    {
        if (_player.Experience.UpgradePoints > 0)
        {
            gameObject.SetActive(true);
            _text.text = _player.Experience.UpgradePoints.ToString();
        }
        else
        {
            Close();
        }
    }

    private void OnShowButtonClicked()
    {
        if (_player.Experience.UpgradePoints > 0)
        {
            _windowService.Open(WindowType.CardMenu);
        }
    }

    public override void Close()
    {
        _text.text = _player.Experience.UpgradePoints.ToString();

        if (_player.Experience.UpgradePoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
