using UnityEngine;

public class PickUper : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    public void Pickup(Transform pickObject)
    {
        pickObject.SetParent(transform);
        pickObject.localPosition = Vector3.zero;
    }
}
