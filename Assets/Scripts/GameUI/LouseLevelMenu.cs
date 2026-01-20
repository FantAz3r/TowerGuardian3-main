using UnityEngine;
using UnityEngine.UI;

public class LouseLevelMenu : LevelMenu
{
    [SerializeField] private Button _resurrectionButton;

    private Health _playerHealth;
    private bool _canResurrection = true;

    public void SetPlayerHealth(Health health)
    {
        if (_playerHealth != null)
            _playerHealth.Died -= OnLouse;

        _playerHealth = health;

        if (_playerHealth != null)
            _playerHealth.Died += OnLouse;

        _resurrectionButton.onClick.AddListener(Resurrection);

        gameObject.SetActive(false);
    }

    public void OnLouse()
    {
        if(_canResurrection == false)
        {
            _resurrectionButton.onClick.RemoveListener(Resurrection);
            _resurrectionButton.gameObject.SetActive(false);
        }

        OpenMenu();
    }

    protected override void OnDestroy()
    {
        if (_playerHealth != null)
            _playerHealth.Died -= OnLouse;
        _resurrectionButton.onClick.RemoveListener(Resurrection);

        base.OnDestroy();
    }

    private void Resurrection()
    {
        Debug.Log("Resurrection");
        _canResurrection = false;
        _playerHealth.gameObject.SetActive(true);
        _playerHealth.Heal(_playerHealth.MaxHealth);
        CloseMenu();
    }
}
