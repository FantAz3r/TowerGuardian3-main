using System.Collections.Generic;
using UnityEngine;
using YG;

public class TowerRenderer : MonoBehaviour
{
    [SerializeField] private List<Floor> _floors;
    private int _currentFloor = -1;

    public IReadOnlyList<Floor> Floors => _floors;

    private void Start()
    {
        LoadTower();

        foreach (var floor in _floors)
        {
            if(floor.FloorNumber > _currentFloor)
            {
                HandleGoingDown(floor.FloorNumber);
            }
        }
    }

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

        SaveTower();
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

    private void SaveTower()
    {
        YG2.saves.CurrentFloor = _currentFloor;
    }

    private void LoadTower()
    {
        if(YG2.saves.CurrentFloor == 0)
            return;

        _currentFloor = YG2.saves.CurrentFloor;
    }
}


