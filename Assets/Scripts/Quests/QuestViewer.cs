using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewer : WindowBase
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _warningFrame;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private TMP_Text _timer;

    private Tween _warningTween;

    private void Awake()
    {
        Debug.Log("sdgfasrg");
    }
    public void Render(IQuest quest)
    {
        
        Open();
        _image.sprite = quest.Config.Image;
        _description.text = quest.Config.Description;

        if (quest.Config.IsProgressQuest == false)
            _progress.gameObject.SetActive(false);
        else
            _progress.gameObject.SetActive(true);

        if (quest.Config.IsTimeQuest == false)
            _timer.gameObject.SetActive(false);
        else
            _timer.gameObject.SetActive(true);
    }

    public void UpdateProgress(float currentValue, float targetValue)
    {
        if (_progress.gameObject.activeSelf == false)
            return;

        _progress.text = $"{currentValue}/{targetValue}";
    }

    public void UpdateTime(float time)
    {
        if (_timer.gameObject.activeSelf == false)
            return;

        int oneMinute = 60;
        int minutes = Mathf.FloorToInt(time / oneMinute);
        int seconds = Mathf.FloorToInt(time % oneMinute);
        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public override void Close()
    {
        base.Close();
        Destroy(gameObject);
    }

    public void ActivateWarning()
    {
        if (_warningFrame == null)
            return;

        _warningFrame.enabled = true;

        _warningTween?.Kill();

        _warningTween = _warningFrame.DOFade(0f, 0.5f) 
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void DeactivateWarning()
    {
        _warningTween?.Kill();
        _warningFrame.enabled = false;
    }
}
