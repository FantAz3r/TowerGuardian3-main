using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private List<AbilityViewer> _viewers;

    private AllAbilities _container;
    private int _count = 0;

    public void Init(AllAbilities container)
    {
        _container = container;

        _container.AbilityActivated += View;
        _container.AbilityRemoved += RemoveView;
    }

    private void OnDestroy()
    {
        _container.AbilityActivated -= View;
    }

    private void View(AbilityConfig config, IAbility ability)
    {
        _count++;
        _viewers[_count].ActivateViewer(ability, config);
    }

    private void RemoveView(AbilityConfig config, IAbility ability)
    {
        foreach(var viewer in _viewers)
        {
            if(viewer.Ability == ability)
                viewer.DeactivateViewer();
        }

        _count--;
    }
}
