﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private List<Drone> _drones;
        [SerializeField] private ResourceProvider _resourceProvider;
        [SerializeField] private BaseStorage _storage;
        [SerializeField] private float _delay;

        public bool HaveInheritor { get; private set; }

        private BasePriority _basePriority;

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
        }

        public void ShowStorage()
        {
            _storage.ShowInfo();
        }
        
        public void SetInheritor()
        {
            HaveInheritor = true;
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
            Watermelon melon = _resourceProvider.GetFreeWatermelon();
            
            if (TryGetDrone(out Drone drone) && melon !=null)
            {
                drone.GoToPoint(melon);
                _resourceProvider.MarkAsTaken(melon);
            }
        }

        private bool TryGetDrone(out Drone drone)
        {
            drone = _drones.FirstOrDefault(drone => drone.IsOnMission == false);
            return drone is not null;
        }

        private void OnMissionComplete()
        {
            _storage.IncreaseCount();
        }
    }
}
