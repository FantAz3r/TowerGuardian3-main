using UnityEngine;

public class TransparencyTrigger : MonoBehaviour
{
    private Transform _player;

    public void Init(Transform player)
    {
        _player = player;
    }

    void Update()
    {
        Vector3 direction = _player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction.normalized, distance);

        foreach (RaycastHit hit in hits)
        {
            TransparencyObject obj = hit.collider.GetComponent<TransparencyObject>();

            if (obj != null)
            {
                obj.MakeInvisible();
            }
        }
    }
}

