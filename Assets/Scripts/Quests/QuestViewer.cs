using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class QuestViewer : WindowBase
{
    [field: SerializeField] public Highlighter Highlighter { get; private set; }

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private TMP_Text _timer;

    public void Render(IQuest quest)
    {
        Open();
        _image.sprite = quest.Config.Image;
        RenderDescription(quest);

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
        if (_progress != null && _progress.gameObject.activeSelf == false)
            return;

        _progress.text = $"{currentValue}/{targetValue}";
    }

    public void UpdateTime(float time)
    {
        if (_timer != null &&  _timer.gameObject.activeSelf == false)
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

    private void RenderDescription(IQuest quest)
    {
        if (YG2.envir.isDesktop || string.IsNullOrEmpty(quest.Config.MobileDescription))
            _description.text = quest.Config.Description;
        else
            _description.text = quest.Config.MobileDescription;
    }
}
