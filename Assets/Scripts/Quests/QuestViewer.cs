using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;

    private Tutorial _tutorial;

    public void Init(Tutorial tutorial)
    {
        _tutorial = tutorial;
        _tutorial.QuestSeted += Render;
        _tutorial.QuestUpdated += UpdateProgress;
    }

    private void OnDestroy()
    {
        _tutorial.QuestSeted -= Render;
        _tutorial.QuestUpdated -= UpdateProgress;
    }

    private void Render(Sprite sprite, string description)
    {
        _image.sprite = sprite;
        _description.text = description;
    }

    private void UpdateProgress(string description)
    {
        _description.text = description;
    }
}
