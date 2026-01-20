using UnityEngine;
using YG;
public class Player : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;
    private void OnDestroy()
    {
        IsAlive = false;
        YG2.saves.PlayerPosition = transform.position;
        YG2.SaveProgress();
    }
}
