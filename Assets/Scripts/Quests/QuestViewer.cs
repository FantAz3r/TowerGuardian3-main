using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _description;

    public void Render(Sprite sprite, string description)
    {
        _image.sprite = sprite;
        _description.text = description;
    }
}
