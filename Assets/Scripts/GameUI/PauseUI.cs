using LayerLab;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private RectTransform _pausePanel;

    private GameStateMachine _stateMachine;

    public void Init(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

        _homeButton.onClick.AddListener(OnHomeClicked);
        _continueButton.onClick.AddListener(OnContinue);
        _pauseButton.onClick.AddListener(OnPause);
        _pausePanel.gameObject.SetActive(false);
    }

    private void OnHomeClicked()
    {
        OnContinue();
        _stateMachine.EnterIn<LoadingLevelState, LevelID>(LevelID.Tower);
    }

    private void OnPause()
    {
        YG2.PauseGameNoEditEventSystem(true);
    }

    private void OnContinue()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _pausePanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(OnHomeClicked);
        _continueButton.onClick.RemoveListener(OnContinue);
    }
}
