using DG.Tweening;
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

    [SerializeField] private RectTransform _panelRectTransform;

    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private Vector2 _hiddenPosition = new Vector2(800, 0f); 
    private Vector2 _visiblePosition = new Vector2(-50, -70f); 
    private Tween _currentTween;

    private void Awake()
    {
        _visiblePosition = _panelRectTransform.anchoredPosition;
        _panelRectTransform.anchoredPosition = _hiddenPosition;
    }

    private void OnDestroy()
    {
        _currentTween?.Kill();
    }

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

        _progress.text = $"{currentValue:0}/{targetValue:0}";
    }

    public void UpdateTime(float time)
    {
        if (_timer != null && _timer.gameObject.activeSelf == false)
            return;

        int oneMinute = 60;
        int minutes = Mathf.FloorToInt(time / oneMinute);
        int seconds = Mathf.FloorToInt(time % oneMinute);
        _timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public override void Open()
    {
        base.Open();

        _currentTween?.Kill();
        _currentTween = _panelRectTransform.DOAnchorPos(_visiblePosition, _animationDuration).SetEase(Ease.OutCubic);
    }

    public override void Close()
    {
        _currentTween?.Kill();
        _currentTween = _panelRectTransform.DOAnchorPos(_hiddenPosition, _animationDuration)
            .SetEase(Ease.InCubic)
            .OnComplete(() => {
                base.Close();
                Destroy(gameObject);
            });
    }

    private void RenderDescription(IQuest quest)
    {
        if (YG2.envir.isDesktop || string.IsNullOrEmpty(quest.Config.MobileDescription))
            _description.text = quest.Config.Description;
        else
            _description.text = quest.Config.MobileDescription;
    }
}
