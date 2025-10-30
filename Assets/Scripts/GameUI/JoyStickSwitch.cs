using UnityEngine;
using YG;

public class JoyStickSwitch : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);

        if (YG2.envir.isDesktop == false)
        {
            gameObject.SetActive(true);
        }
    }
}
