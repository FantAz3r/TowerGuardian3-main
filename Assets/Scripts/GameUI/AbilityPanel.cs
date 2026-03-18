using System.Collections.Generic;
using UnityEngine;
using YG;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private List<AbilityViewer> _viewers;

    private Dictionary<AbilityViewer, bool> _viewersSlots = new();
    private Dictionary<AbilityKeyCode, bool> _keyCodes = new()
    {
        { AbilityKeyCode.First, false },
        { AbilityKeyCode.Second, false },
        { AbilityKeyCode.Third, false },
        { AbilityKeyCode.Fourth, false}
    };

    private Player _player;
    private IAbilityInput _inputService;
    private void OnAbility1Used() => ActivateAbilityByKey(AbilityKeyCode.First);
    private void OnAbility2Used() => ActivateAbilityByKey(AbilityKeyCode.Second);
    private void OnAbility3Used() => ActivateAbilityByKey(AbilityKeyCode.Third);
    private void OnAbility4Used() => ActivateAbilityByKey(AbilityKeyCode.Fourth);

    private void Awake()
    {
        if (YG2.envir.isDesktop)
        {
            _inputService = ServiceLocator.Get<IAbilityInput>();
            _inputService.OnAbillity1Used += OnAbility1Used;
            _inputService.OnAbillity2Used += OnAbility2Used;
            _inputService.OnAbillity3Used += OnAbility3Used;
            _inputService.OnAbillity4Used += OnAbility4Used;
        }

        _player = ServiceLocator.Get<IGameFactory>().Player;

        foreach (var viewer in _viewers)
        {
            _viewersSlots[viewer] = false;
        }

        _player.AllAbilities.Enabled += View;
        _player.AllAbilities.Removed += RemoveView;

        gameObject.SetActive(true);
    }

    private void Start()
    {
        TryHidePanel();
    }

    private void OnDestroy()
    {
        _player.AllAbilities.Enabled -= View;
        _player.AllAbilities.Removed -= RemoveView;

        if (YG2.envir.isDesktop)
        {
            if(_inputService == null)
                return;

            _inputService.OnAbillity1Used -= OnAbility1Used;
            _inputService.OnAbillity2Used -= OnAbility2Used;
            _inputService.OnAbillity3Used -= OnAbility3Used;
            _inputService.OnAbillity4Used -= OnAbility4Used;
        }
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

        foreach (var pair in _viewersSlots)
        {
            if (pair.Value == false)
            {
                var viewer = pair.Key;
                var key = GetFreeKey(ability);
                viewer.ActivateViewer(ability, key, _player.Attacker);
                _viewersSlots[viewer] = true;
                break;
            }
        }
    }

    private void RemoveView(IAbility ability)
    {
        foreach (var pair in _viewersSlots)
        {
            var viewer = pair.Key;

            if (viewer.Ability == ability)
            {
                SetFreeKey(viewer);
                viewer.DeactivateViewer();
                _viewersSlots[viewer] = false;
                break;
            }
        }

        TryHidePanel();
    }

    private void SetFreeKey(AbilityViewer viewer)
    {
        if (viewer.AbilityKey == AbilityKeyCode.None)
            return;

        if (_keyCodes.ContainsKey(viewer.AbilityKey))
            _keyCodes[viewer.AbilityKey] = false;
    }

    private AbilityKeyCode GetFreeKey(IAbility ability)
    {
        if (ability is UsebleAbility == false)
            return AbilityKeyCode.None;

        foreach (var keyCode in _keyCodes)
        {
            if (keyCode.Value == false)
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
