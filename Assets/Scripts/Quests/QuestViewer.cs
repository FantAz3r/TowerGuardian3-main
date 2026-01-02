using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        _tutorial.Complited += Complite;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _tutorial.QuestSeted -= Render;
        _tutorial.QuestUpdated -= UpdateProgress;
    }

    private void Render(IQuest quest)
    {
        gameObject.SetActive(true);
        _image.sprite = quest.Config.Image;
        _description.text = quest.Config.Description;
    }

    private void UpdateProgress(string description)
    {
        _description.text = description;
    }

    private void Complite()
    {
        gameObject.SetActive(false);
    }
}
