using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private EntryPoint _entyPoint;

    private void Awake()
    {
        EntryPoint _entyPoint = FindObjectOfType<EntryPoint>();

        if (_entyPoint == null )
            Instantiate(_entyPoint);
    }
}
