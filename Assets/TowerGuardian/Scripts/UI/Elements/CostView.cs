using TMPro;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class CostView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _cost;

        public void Render(CostInfo cost)
        {
            _image.sprite = cost.Image;
            _cost.text = cost.Value.ToString();
        }
    }
}