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
        [SerializeField] private BaseLocator _baseLocator;
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
                SendDronesToMissions(_baseLocator.Scan());
                yield return wait;
            }
        }

        private void SendDronesToMissions(List<Watermelon> watermelons)
        {
            foreach (Watermelon watermelon in watermelons)
            {
                if (TryGetDrone(out Drone drone))
                {
                    drone.GoToPoint(watermelon);
                }
            }
        }

        private bool TryGetDrone(out Drone drone)
        {
            drone = null;
            List<Drone> activeDrones = _drones.Where(drone => drone.Status == DroneMissionStatus.NoMission).ToList();
            bool haveActiveDrone = activeDrones.Count > 0;
            
            if (haveActiveDrone)
            {
                drone = activeDrones[0];
            }
            
            return haveActiveDrone;
        }

        private void OnMissionComplete()
        {
            _watermelonsCount++;
            WatermelonsChanged?.Invoke(_watermelonsCount);
        }
    }
}
