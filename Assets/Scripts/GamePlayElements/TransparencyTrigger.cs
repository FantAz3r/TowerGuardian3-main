using UnityEngine;

public class TransparencyTrigger : MonoBehaviour
{
    private Transform _player;
    private Vector3 _offset = new Vector3(0,1,0);
    public void Init(Transform player)
    {
        _player = player;
    }

    void Update()
    {
        
        Vector3 direction = _player.position + _offset - transform.position;
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

