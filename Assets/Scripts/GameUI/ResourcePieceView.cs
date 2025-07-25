using TMPro;
using UnityEngine;

public class ResourcePieceView : MonoBehaviour
{
    [SerializeField] private ResourceType _textType;

    private TMP_Text _text;

    public ResourceType TextType => _textType;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string amount)
    {
        _text.text = amount;
    }
}

