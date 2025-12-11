using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopExitButton : MonoBehaviour
{
    private Button _exitButton;

    private void Awake()
    {
        _exitButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _exitButton.onClick.AddListener ( Exit);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(Exit);
    }

    private void Exit()
    {
        YG2.PauseGameNoEditEventSystem(false);
    }
}
