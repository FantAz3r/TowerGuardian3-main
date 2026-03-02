using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderbordSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private List<LeaderboardYG> _leaderboards;

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        SwitchObjects(_dropdown.value);
    }

    private void OnDropdownValueChanged(int selectedIndex)
    {
        SwitchObjects(selectedIndex);
    }

    private void SwitchObjects(int index)
    {
        for (int i = 0; i < _leaderboards.Count; i++)
        {
            if (_leaderboards[i] != null)
            {
                _leaderboards[i].gameObject.SetActive(i == index);
            }
        }
    }

    private void OnDestroy()
    {
        if (_dropdown != null)
        {
            _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}
