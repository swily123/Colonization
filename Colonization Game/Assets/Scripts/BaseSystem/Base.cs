using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;

namespace BaseSystem
{
    public class Base : MonoBehaviour // TODO разбить на разные ответственности скрипты
    {
        [SerializeField] private BaseDroneSpawner _droneSpawner;
        [SerializeField] private ResourceProvider _resourceProvider;
        [SerializeField] private BaseStorage _storage;
        [SerializeField] private float _delay;

        public bool HaveInheritor { get; private set; }

        private const int InheritorCost = 5;

        private readonly int _startDronesCount = 3;
        private readonly List<Drone> _drones = new();
        private BasePriority _basePriority = BasePriority.ProduceDrone;
        private Vector3 _baseInheritorPosition = Vector3.zero;

        private void OnDisable()
        {
            foreach (Drone drone in _drones)
            {
                drone.MissionCompleted -= OnMissionComplete;
            }
        }

        private void Start()
        {
            for (int i = 0; i < _startDronesCount; i++)
            {
                OnSpawnDrone();
            }

            StartCoroutine(Scanning());
        }

        public void ShowStorage()
        {
            _storage.ShowInfo();
        }

        public void SetInheritor(Vector3 inheritorPosition)
        {
            if (TryBuyStation() == false)
            {
                _baseInheritorPosition = inheritorPosition;
                _basePriority = BasePriority.SetInheritor;
                HaveInheritor = true;
            }
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

            if (TryGetDrone(out Drone drone) && melon is not null)
            {
                drone.GoToWatermelon(melon);
                _resourceProvider.MarkAsTaken(melon);
            }
        }

        private bool TryGetDrone(out Drone drone)
        {
            drone = null;
            drone = _drones.FirstOrDefault(drone => drone.IsOnMission == false);

            return drone is not null;
        }

        private void OnMissionComplete()
        {
            _storage.IncreaseCount();

            switch (_basePriority)
            {
                case BasePriority.ProduceDrone:
                {
                    if (_storage.TryDecreaseCount(_droneSpawner.DroneCost))
                    {
                        OnSpawnDrone();
                    }

                    break;
                }

                case BasePriority.SetInheritor:
                {
                    TryBuyStation();
                    break;
                }
            }
        }

        private bool TryBuyStation()
        {
            bool isBought = false;

            if (_storage.TryDecreaseCount(InheritorCost))
            {
                if (TryGetDrone(out Drone drone))
                {
                    _basePriority = BasePriority.ProduceDrone;
                    isBought = true;
                    drone.GoToPoint(_baseInheritorPosition);
                }
            }

            return isBought;
        }

        private void OnSpawnDrone()
        {
            Drone drone = _droneSpawner.SpawnDrone();
            _drones.Add(drone);
            drone.MissionCompleted += OnMissionComplete;
        }
    }
}