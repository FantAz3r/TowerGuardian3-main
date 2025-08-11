using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private List<AbilityViewer> _viewers;

    private AllAbilities _container;
    private int _count =0;

    public void Init(AllAbilities container)
    {
        _container = container;
        _container.AbilityActivated += View;
    }

    private void OnDestroy()
    {
        _container.AbilityActivated -= View;
    }

    private void View(AbilityConfig config, IAbility ability)
    {
        _viewers[_count].ActivateViewer(ability, config);
        _count++;
    }
}
