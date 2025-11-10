using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Disabled(IDemageable demageable)
    {
        gameObject.SetActive(false);
    }
}
