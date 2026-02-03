using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private List<AbilityViewer> _viewers;

    private Dictionary<AbilityKeyCode, bool> _keyCodes = new()
    {
        { AbilityKeyCode.First, false },
        { AbilityKeyCode.Second, false },
        { AbilityKeyCode.Third, false },
        { AbilityKeyCode.Fourth, false}
    };

    private Player _player;
    private IInputService _inputService;
    private int _count = 0;
    private void OnAbility1Used() => ActivateAbilityByKey(AbilityKeyCode.First);
    private void OnAbility2Used() => ActivateAbilityByKey(AbilityKeyCode.Second);
    private void OnAbility3Used() => ActivateAbilityByKey(AbilityKeyCode.Third);
    private void OnAbility4Used() => ActivateAbilityByKey(AbilityKeyCode.Fourth);

    private void Awake()
    {
        _inputService = ServiceLocator.Get<IInputService>();
        _inputService.OnAbillity1Used += OnAbility1Used;
        _inputService.OnAbillity2Used += OnAbility2Used;
        _inputService.OnAbillity3Used += OnAbility3Used;
        _inputService.OnAbillity4Used += OnAbility4Used;
    }

    public void Init(Player player)
    {
        _player = player;
        gameObject.SetActive(true);

        _player.AllAbilities.Enabled += View;
        _player.AllAbilities.Removed += RemoveView;

        TryHidePanel();
    }

    private void OnDestroy()
    {
        _player.AllAbilities.Enabled -= RemoveView;
        _player.AllAbilities.Removed -= View;
        _inputService.OnAbillity1Used -= OnAbility1Used;
        _inputService.OnAbillity2Used -= OnAbility2Used;
        _inputService.OnAbillity3Used -= OnAbility3Used;
        _inputService.OnAbillity4Used -= OnAbility4Used;
    }

    private void ActivateAbilityByKey(AbilityKeyCode key)
    {
        foreach (var viewer in _viewers)
        {
            if (viewer.HasAbility && viewer.AbilityKey == key)
            {
                viewer.ActivateAbility();
                break;
            }
        }
    }

    private void View(IAbility ability)
    {
        gameObject.SetActive(true);
        _viewers[_count].ActivateViewer(ability, GetFreeKey(ability), _player.Attacker);
        _count++;
    }

    private void RemoveView(IAbility ability)
    {
        foreach (var viewer in _viewers)
        {
            if (viewer.Ability == ability)
            {
                viewer.DeactivateViewer();
            }
        }

        _count--;
        TryHidePanel();
    }

    private AbilityKeyCode GetFreeKey(IAbility ability)
    {
        if (ability is UsebleAbility == false)
            return AbilityKeyCode.None;

        foreach(var keyCode in _keyCodes)
        {
            if(keyCode.Value == false)
            {
                _keyCodes[keyCode.Key] = true;
                return keyCode.Key;
            }
        }

        return AbilityKeyCode.None;
    }

    private void TryHidePanel()
    {
        foreach (var viewer in _viewers)
        {
            if (viewer.HasAbility == false)
            {
                continue;
            }
            else
                return;
        }

        gameObject.SetActive(false);
    }
}
