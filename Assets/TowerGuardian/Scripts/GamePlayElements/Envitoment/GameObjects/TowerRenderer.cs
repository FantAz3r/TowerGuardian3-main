using System.Collections.Generic;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class TowerRenderer : MonoBehaviour
    {
        [SerializeField]
        private List<Floor> _floors;
        private int _currentFloor;

        public IReadOnlyList<Floor> Floors => _floors;

        private void Start()
        {
            LoadTower();

            foreach (var floor in _floors)
            {
                if (floor.FloorNumber > _currentFloor)
                {
                    _floors[floor.FloorNumber].gameObject.SetActive(false);
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
        }

        private void HandleGoingUp(int floorNumber)
        {
            _floors[_currentFloor].Decor.SetActive(false);
            _currentFloor = floorNumber + 1;
            _floors[_currentFloor].gameObject.SetActive(true);
            _floors[_currentFloor].Decor.SetActive(true);
            SaveTower();
        }

        private void HandleGoingDown(int floorNumber)
        {
            _floors[_currentFloor].gameObject.SetActive(false);
            _currentFloor -= 1;
            _floors[_currentFloor].Decor.SetActive(true);
            SaveTower();
        }

        private void SaveTower()
        {
            YG2.saves.CurrentFloor = _currentFloor;
            YG2.SaveProgress();
        }

        private void LoadTower()
        {
            if (YG2.saves.CurrentFloor == 0)
            {
                return;
            }

            _currentFloor = YG2.saves.CurrentFloor;
        }
    }
}