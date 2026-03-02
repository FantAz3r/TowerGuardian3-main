using TMPro;
using UnityEngine;

namespace YG.Example
{
    public class DemoTextTranslateRU : MonoBehaviour
    {
        [TextArea(1, 100)]
        public string textRU;

#if RU_YG2
        void Start()
        {
            if (textRU != string.Empty)
                GetComponent<TMP_Text>().text = textRU;
        }
#endif
    }
}
