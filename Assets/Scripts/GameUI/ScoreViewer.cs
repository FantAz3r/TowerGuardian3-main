using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreCount;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _zeroStars;
    [SerializeField] private Sprite _oneStars;
    [SerializeField] private Sprite _twoStars;
    [SerializeField] private Sprite _threeStars;

    private ScoreCounter _scoreCounter;

    public void Init(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
        _scoreCounter.LevelEnded += View;
    }

    private void OnDestroy()
    {
        if(_scoreCounter != null)
        {
            _scoreCounter.LevelEnded -= View;
        }
    }

    public void View(float score, int time, int stars)
    {
        _scoreCount.text = score.ToString();
        int minutes = time / 60;
        int seconds = time % 60;
        _time.text = $"{minutes:D2}:{seconds:D2}";

        switch (stars)
        {
            case 0:
                _image.sprite = _zeroStars;
                break;
            case 1:
                _image.sprite = _oneStars;
                break;
            case 2:
                _image.sprite = _twoStars;
                break;
            case 3:
                _image.sprite = _threeStars;
                break;
            default:
                _image.sprite = _zeroStars;
                break;
        }
    }
}
