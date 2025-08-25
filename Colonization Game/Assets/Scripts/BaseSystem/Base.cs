using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private List<Drone> _drones;
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private float _delay;

        private int _watermelonsCount;
        public event Action<int> WatermelonsChanged;

        private void OnEnable()
        {
            foreach (Drone drone in _drones)
            {
                drone.MissionCompleted += OnMissionComplete;
            }
        }

        private void OnDisable()
        {
            foreach (Drone drone in _drones)
            {
                drone.MissionCompleted -= OnMissionComplete;
            }
        }

        private void Start()
        {
            StartCoroutine(Scanning());
            WatermelonsChanged?.Invoke(_watermelonsCount);
        }

        private IEnumerator Scanning()
        {
            WaitForSeconds wait = new WaitForSeconds(_delay);

            while (enabled)
            {
                SendDronesToMissions();
                yield return wait;
            }
        }

        private void SendDronesToMissions()
        {
            Watermelon melon = _resourceManager.GetFreeWatermelon();
            
            if (TryGetDrone(out Drone drone) && melon !=null)
            {
                drone.GoToPoint(melon);
                _resourceManager.MarkAsTaken(melon);
            }
        }

        private bool TryGetDrone(out Drone drone)
        {
            drone = _drones.FirstOrDefault(drone => drone.IsOnMission == false);
            return drone is not null;
        }

        private void OnMissionComplete()
        {
            _watermelonsCount++;
            WatermelonsChanged?.Invoke(_watermelonsCount);
        }
    }
}
