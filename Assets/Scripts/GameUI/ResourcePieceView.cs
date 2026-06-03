using TMPro;
using UnityEngine;

public class ResourcePieceView : MonoBehaviour
{
    [SerializeField] private ResourceType _textType;
    [SerializeField] private TMP_Text _text;

    public ResourceType TextType => _textType;

    public void SetText(string amount)
    {
        _text.text = amount;
    }
}