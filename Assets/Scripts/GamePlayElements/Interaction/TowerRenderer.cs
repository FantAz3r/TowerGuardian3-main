using System.Collections.Generic;
using UnityEngine;

public class TowerRenderer : MonoBehaviour
{
    [SerializeField] private List<Floor> _floors;
    private int _currentFloor = -1;

    public IReadOnlyList<Floor> Floors => _floors;

    private void OnEnable()
    {
        foreach (var floor in _floors)
        {
            floor.GoingUp += HandleGoingUp;
            floor.GoingDown += HandleGoingDown;
        }
    }

    private void OnDisable()
    {
        foreach (var floor in _floors)
        {
            floor.GoingUp -= HandleGoingUp;
            floor.GoingDown -= HandleGoingDown;
        }
    }

    private void HandleGoingUp(int floorNumber)
    {
        ActivateFloor(floorNumber);
    }

    private void HandleGoingDown(int floorNumber)
    {
        _floors[_currentFloor].gameObject.SetActive(false);
        _currentFloor -= 1;

    }

    private void ActivateFloor(int floorNumber)
    {
        _currentFloor = floorNumber + 1;
        _floors[_currentFloor].gameObject.SetActive(true);
    }
}


