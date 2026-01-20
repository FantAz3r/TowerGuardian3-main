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

    private AllAbilities _container;
    private PlayerAttacker _playerAttacker;
    private IInputService _inputService;
    private int _count = 0;

    public void Init(AllAbilities container, PlayerAttacker playerAttacker)
    {
        _container = container;
        _playerAttacker = playerAttacker;
        _inputService = ServicesLocator.GetService<IInputService>();
        _container.AbilityActivated += View;
        _container.AbilityRemoved += RemoveView;

        SubscribeInput();
        TryHidePanel();
    }

    private void OnDestroy()
    {
        _inputService.OnAbillity1Used -= () => ActivateAbilityByKey(AbilityKeyCode.First);
        _inputService.OnAbillity2Used -= () => ActivateAbilityByKey(AbilityKeyCode.Second);
        _inputService.OnAbillity3Used -= () => ActivateAbilityByKey(AbilityKeyCode.Third);
        _inputService.OnAbillity4Used -= () => ActivateAbilityByKey(AbilityKeyCode.Fourth);
        _container.AbilityActivated -= View;
    }

    private void SubscribeInput()
    {
        _inputService.OnAbillity1Used += () => ActivateAbilityByKey(AbilityKeyCode.First);
        _inputService.OnAbillity2Used += () => ActivateAbilityByKey(AbilityKeyCode.Second);
        _inputService.OnAbillity3Used += () => ActivateAbilityByKey(AbilityKeyCode.Third);
        _inputService.OnAbillity4Used += () => ActivateAbilityByKey(AbilityKeyCode.Fourth);
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

    private void View(AbilityConfig config, IAbility ability)
    {
        gameObject.SetActive(true);
        _viewers[_count].ActivateViewer(ability, config, GetFreeKey(ability), _playerAttacker);
        _count++;
    }

    private void RemoveView(AbilityConfig config, IAbility ability)
    {
        foreach (var viewer in _viewers)
        {
            if (viewer.Ability == ability)
                viewer.DeactivateViewer();
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
