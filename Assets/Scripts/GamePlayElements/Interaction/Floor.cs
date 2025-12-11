using System;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private StairsTrigger _upTrigger;
    [SerializeField] private StairsTrigger _downTrigger;
    [SerializeField] private int _floorNumber;

    private int _startFloor = 0;
    public int FloorNumber => _floorNumber;

    public event Action<int> GoingDown;
    public event Action<int> GoingUp;   

    private void Start()
    {
        if(_floorNumber > _startFloor)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(_upTrigger != null)
            _upTrigger.Entered += Up;

        if(_downTrigger != null)
            _downTrigger.Entered += Down;
    }

    private void OnDisable()
    {
        if (_upTrigger != null)
            _upTrigger.Entered -= Up;

        if (_downTrigger != null)
            _downTrigger.Entered -= Down;
    }

    private void Up()
    {
        GoingUp?.Invoke(_floorNumber);
    }

    private void Down()
    {
        GoingDown?.Invoke(_floorNumber);
    }
}
